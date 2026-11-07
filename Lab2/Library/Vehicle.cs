namespace Library
{
    public abstract class Vehicle
    {
        private string brand;
        private string model;
        private string registrationNumber;
        private decimal ratePerKm;

        public string Brand => brand;
        public string Model => model;
        public string RegistrationNumber => registrationNumber;
        public decimal RatePerKm => ratePerKm;

        protected Vehicle(
            string brand,
            string model,
            string registrationNumber,
            decimal ratePerKm)
        {
            if (string.IsNullOrWhiteSpace(brand))
                throw new ArgumentException("Марка не может быть пустой.");

            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Модель не может быть пустой.");

            if (string.IsNullOrWhiteSpace(registrationNumber))
                throw new ArgumentException("Номер не может быть пустым.");

            if (ratePerKm <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(ratePerKm),
                    "Стоимость километра должна быть больше нуля.");

            this.brand = brand;
            this.model = model;
            this.registrationNumber = registrationNumber;
            this.ratePerKm = ratePerKm;
        }

        public abstract string CapacityDescription { get; }

        public abstract decimal CalculateTripCost(decimal distance);

        protected static void ValidateDistance(decimal distance)
        {
            if (distance <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(distance),
                    "Расстояние должно быть больше нуля.");
        }

        public override string ToString()
        {
            return $"{brand} {model}, номер: {registrationNumber}, " +
                   $"стоимость за км: {ratePerKm} руб.";
        }
    }
}