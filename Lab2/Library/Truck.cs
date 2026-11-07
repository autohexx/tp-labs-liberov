namespace Library
{
    public class Truck : Vehicle
    {
        private decimal maxLoadTons;

        public decimal MaxLoadTons => maxLoadTons;

        public Truck(
            string brand,
            string model,
            string registrationNumber,
            decimal ratePerKm,
            decimal maxLoadTons)
            : base(brand, model, registrationNumber, ratePerKm)
        {
            if (maxLoadTons <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(maxLoadTons),
                    "Грузоподъёмность должна быть больше нуля.");

            this.maxLoadTons = maxLoadTons;
        }

        public override string CapacityDescription =>
            $"{maxLoadTons} тонн груза";

        public override decimal CalculateTripCost(decimal distance)
        {
            ValidateDistance(distance);

            return distance * (RatePerKm + maxLoadTons * 5);
        }

        public override string ToString()
        {
            return $"Грузовой автомобиль: {base.ToString()}, " +
                   $"грузоподъёмность: {CapacityDescription}";
        }
    }
}