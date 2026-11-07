using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Truck : Vehicle
    {
        private readonly double _maxCargoKg;
        private readonly double _cargoCoefficient;
        private readonly bool _requiresSpecialPermit;

        public Truck(string plateNumber, string brand, int year,
                     double fuelConsumption, double fuelPrice,
                     double maxCargoKg, double cargoCoefficient, bool requiresSpecialPermit)
            : base(plateNumber, brand, year, fuelConsumption, fuelPrice)
        {
            if (maxCargoKg <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxCargoKg), "Грузоподъёмность должна быть > 0.");
            if (cargoCoefficient < 0.0 || cargoCoefficient > 1.5)
                throw new ArgumentOutOfRangeException(nameof(cargoCoefficient), "Коэффициент груза: 0.0..1.5.");

            _maxCargoKg = maxCargoKg;
            _cargoCoefficient = cargoCoefficient;
            _requiresSpecialPermit = requiresSpecialPermit;
        }

        public override int Capacity => (int)(_maxCargoKg / 1000.0);
        public override string VehicleType => "Грузовой";

        public override double CalculateTripCost(double distanceKm)
        {
            double cost = FuelCost(distanceKm) * (1.0 + _cargoCoefficient);
            if (_requiresSpecialPermit)
                cost *= 1.2;
            return cost;
        }

        public override string ToString()
        {
            return base.ToString() +
                   $", груз до {_maxCargoKg:F0} кг, загрузка ×{_cargoCoefficient:F1}" +
                   (_requiresSpecialPermit ? ", спецразрешение" : "");
        }
    }
}
