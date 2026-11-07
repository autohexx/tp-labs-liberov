using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Car : Vehicle
    {
        private readonly int _passengerSeats;
        private readonly double _comfortCoefficient;

        public Car(string plateNumber, string brand, int year,
                   double fuelConsumption, double fuelPrice,
                   int passengerSeats, double comfortCoefficient)
            : base(plateNumber, brand, year, fuelConsumption, fuelPrice)
        {
            if (passengerSeats < 1 || passengerSeats > 8)
                throw new ArgumentOutOfRangeException(nameof(passengerSeats), "Мест в легковом авто: 1..8.");
            if (comfortCoefficient < 1.0 || comfortCoefficient > 3.0)
                throw new ArgumentOutOfRangeException(nameof(comfortCoefficient), "Коэффициент комфорта: 1.0..3.0.");

            _passengerSeats = passengerSeats;
            _comfortCoefficient = comfortCoefficient;
        }

        public override int Capacity => _passengerSeats;
        public override string VehicleType => "Легковой";

        public override double CalculateTripCost(double distanceKm)
        {
            return FuelCost(distanceKm) * _comfortCoefficient;
        }

        public override string ToString()
        {
            return base.ToString() + $", комфорт ×{_comfortCoefficient:F1}";
        }
    }
}
