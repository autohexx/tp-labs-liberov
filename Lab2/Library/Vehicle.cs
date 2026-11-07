namespace Library
{
    public abstract class Vehicle
    {
        private readonly string _plateNumber;
        private readonly string _brand;
        private readonly int _year;
        private readonly double _fuelConsumption; // л / 100 км
        private readonly double _fuelPrice;       // руб / л

        protected Vehicle(string plateNumber, string brand, int year,
                          double fuelConsumption, double fuelPrice)
        {
            if (string.IsNullOrWhiteSpace(plateNumber))
                throw new ArgumentException("Госномер не может быть пустым.", nameof(plateNumber));
            if (string.IsNullOrWhiteSpace(brand))
                throw new ArgumentException("Марка не может быть пустой.", nameof(brand));
            if (year < 1900 || year > DateTime.Now.Year + 1)
                throw new ArgumentOutOfRangeException(nameof(year), "Некорректный год выпуска.");
            if (fuelConsumption <= 0)
                throw new ArgumentOutOfRangeException(nameof(fuelConsumption), "Расход топлива должен быть > 0.");
            if (fuelPrice <= 0)
                throw new ArgumentOutOfRangeException(nameof(fuelPrice), "Цена топлива должна быть > 0.");

            _plateNumber = plateNumber;
            _brand = brand;
            _year = year;
            _fuelConsumption = fuelConsumption;
            _fuelPrice = fuelPrice;
        }

        public string PlateNumber => _plateNumber;
        public string Brand => _brand;
        public int Year => _year;
        public double FuelConsumption => _fuelConsumption;
        public double FuelPrice => _fuelPrice;

        public abstract int Capacity { get; }
        public abstract string VehicleType { get; }

        public abstract double CalculateTripCost(double distanceKm);

        protected double FuelCost(double distanceKm)
        {
            if (distanceKm <= 0)
                throw new ArgumentOutOfRangeException(nameof(distanceKm), "Расстояние должно быть > 0.");
            return distanceKm / 100.0 * _fuelConsumption * _fuelPrice;
        }

        public override string ToString()
        {
            return $"[{VehicleType}] {_brand} ({_plateNumber}, {_year} г.) " +
                   $"расход {_fuelConsumption:F1} л/100км, " +
                   $"топливо {_fuelPrice:F2} руб/л, вместимость {Capacity}";
        }
    }
}
