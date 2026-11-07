using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Library
{
    public class Bus : Vehicle
    {
        private readonly int _passengerCapacity;
        private readonly decimal _ticketPrice;

        public Bus(string plateNumber, string brand, int year,
                   double fuelConsumption, double fuelPrice,
                   int passengerCapacity, decimal ticketPrice)
            : base(plateNumber, brand, year, fuelConsumption, fuelPrice)
        {
            if (passengerCapacity < 8 || passengerCapacity > 200)
                throw new ArgumentOutOfRangeException(nameof(passengerCapacity), "Мест в автобусе: 8..200.");
            if (ticketPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(ticketPrice), "Цена билета не может быть отрицательной.");

            _passengerCapacity = passengerCapacity;
            _ticketPrice = ticketPrice;
        }

        public override int Capacity => _passengerCapacity;
        public override string VehicleType => "Автобус";

        public override double CalculateTripCost(double distanceKm)
        {
            return FuelCost(distanceKm) + (double)_ticketPrice * _passengerCapacity * 0.1;
        }

        public override string ToString()
        {
            return base.ToString() + $", билет {_ticketPrice:F2} руб.";
        }
    }
}
