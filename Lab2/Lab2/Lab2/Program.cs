using System;
using System.Collections.Generic;

namespace Lab2
{
    abstract class Vehicle
    {
        private string brand;
        private string model;
        private int year;
        private decimal pricePerKm;

        public Vehicle(string brand, string model, int year, decimal pricePerKm)
        {
            if (string.IsNullOrWhiteSpace(brand))
                throw new ArgumentException("Марка не может быть пустой.");

            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Модель не может быть пустой.");

            if (year < 1886 || year > DateTime.Now.Year)
                throw new ArgumentOutOfRangeException("Некорректный год выпуска.");

            if (pricePerKm <= 0)
                throw new ArgumentOutOfRangeException("Стоимость километра должна быть положительной.");

            this.brand = brand;
            this.model = model;
            this.year = year;
            this.pricePerKm = pricePerKm;
        }

        // Абстрактный метод определения вместимости
        public abstract decimal GetCapacity();


        public abstract string GetCapacityDescription();


        public decimal CalculateTripCost(decimal distance)
        {
            if (distance < 0)
                throw new ArgumentOutOfRangeException("Расстояние не может быть отрицательным.");

            return distance * pricePerKm;
        }

       
        public bool CanAccommodate(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException("Количество не может быть отрицательным.");

            return amount <= GetCapacity();
        }

        public override string ToString()
        {
            return $"{brand} {model}, год выпуска: {year}, " +
                   $"стоимость за км: {pricePerKm} руб.";
        }
    }

    
    class PassengerCar : Vehicle
    {
        private int passengerCapacity;

        public PassengerCar(string brand, string model, int year,
            decimal pricePerKm, int passengerCapacity)
            : base(brand, model, year, pricePerKm)
        {
            if (passengerCapacity <= 0)
                throw new ArgumentOutOfRangeException("Количество мест должно быть положительным.");

            this.passengerCapacity = passengerCapacity;
        }

        public override decimal GetCapacity()
        {
            return passengerCapacity;
        }

        public override string GetCapacityDescription()
        {
            return $"Количество пассажирских мест: {passengerCapacity}";
        }

        public override string ToString()
        {
            return "[Легковой автомобиль] " + base.ToString() +
                   $", мест: {passengerCapacity}";
        }
    }

    class Truck : Vehicle
    {
        private decimal loadCapacity;

        public Truck(string brand, string model, int year,
            decimal pricePerKm, decimal loadCapacity)
            : base(brand, model, year, pricePerKm)
        {
            if (loadCapacity <= 0)
                throw new ArgumentOutOfRangeException("Грузоподъемность должна быть положительной.");

            this.loadCapacity = loadCapacity;
        }

        public override decimal GetCapacity()
        {
            return loadCapacity;
        }

        public override string GetCapacityDescription()
        {
            return $"Грузоподъемность: {loadCapacity} тонн";
        }

        public override string ToString()
        {
            return "[Грузовой автомобиль] " + base.ToString() +
                   $", грузоподъемность: {loadCapacity} тонн";
        }
    }


    class Bus : Vehicle
    {
        private int passengerCapacity;
        public Bus(string brand, string model, int year,
            decimal pricePerKm, int passengerCapacity)
            : base(brand, model, year, pricePerKm)
        {
            if (passengerCapacity <= 0)
                throw new ArgumentOutOfRangeException("Количество мест должно быть положительным.");

            this.passengerCapacity = passengerCapacity;
        }

        public override decimal GetCapacity()
        {
            return passengerCapacity;
        }

        public override string GetCapacityDescription()
        {
            return $"Количество пассажирских мест: {passengerCapacity}";
        }

        public override string ToString()
        {
            return "[Автобус] " + base.ToString() +
                   $", мест: {passengerCapacity}";
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("СИСТЕМА УПРАВЛЕНИЯ АВТОПАРКОМ");
  


            List<Vehicle> fleet = new List<Vehicle>();


            fleet.Add(new PassengerCar("Toyota", "Camry", 2022, 25, 4));
            fleet.Add(new PassengerCar("Hyundai", "Solaris", 2021, 20, 4));
            fleet.Add(new Truck("MAN", "TGX", 2020, 45, 20));
            fleet.Add(new Truck("Volvo", "FH", 2023, 50, 15));
            fleet.Add(new Bus("Mercedes-Benz", "Sprinter", 2022, 35, 20));

 
            Console.WriteLine("1. СПИСОК ТРАНСПОРТНЫХ СРЕДСТВ\n");

            foreach (Vehicle vehicle in fleet)
            {
                Console.WriteLine(vehicle);
            }


            Console.WriteLine("\n2. РАСЧЕТ СТОИМОСТИ ПОЕЗДКИ\n");

            decimal distance = 100;

            foreach (Vehicle vehicle in fleet)
            {
                Console.WriteLine(vehicle);
                Console.WriteLine($"Расстояние: {distance} км");
                Console.WriteLine(
                    $"Стоимость поездки: {vehicle.CalculateTripCost(distance)} руб.");
                Console.WriteLine();
            }

   
            Console.WriteLine("3. ВМЕСТИМОСТЬ ТРАНСПОРТНЫХ СРЕДСТВ\n");

            foreach (Vehicle vehicle in fleet)
            {
                Console.WriteLine(vehicle);
                Console.WriteLine(vehicle.GetCapacityDescription());
                Console.WriteLine();
            }

           
            Console.WriteLine("4. ПРОВЕРКА ВОЗМОЖНОСТИ ПЕРЕВОЗКИ\n");

            foreach (Vehicle vehicle in fleet)
            {
                decimal requiredCapacity = 3;

                Console.WriteLine(vehicle);
                Console.WriteLine(
                    $"Необходимая вместимость: {requiredCapacity}");

                if (vehicle.CanAccommodate(requiredCapacity))
                {
                    Console.WriteLine("Перевозка возможна.");
                }
                else
                {
                    Console.WriteLine("Перевозка невозможна.");
                }

                Console.WriteLine();
            }

          
            Console.WriteLine("5. ПРОВЕРКА КОРРЕКТНОСТИ ДАННЫХ\n");

            try
            {
                PassengerCar invalidCar = new PassengerCar(
                    "Toyota", "Test", 2022, 25, -5);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(
                    "Ошибка при создании автомобиля: " + ex.Message);
            }
            try
            {
                fleet[0].CalculateTripCost(-100);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(
                    "Ошибка при расчете поездки: " + ex.Message);
            }

            Console.WriteLine("\n");
            Console.WriteLine("РАБОТА ПРОГРАММЫ ЗАВЕРШЕНА.");


            Console.ReadKey();
        }
    }
}