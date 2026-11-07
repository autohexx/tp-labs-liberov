namespace Library
{
    public class PassengerCar : Vehicle
    {
        private int passengerSeats;

        public int PassengerSeats => passengerSeats;

        public PassengerCar(
            string brand,
            string model,
            string registrationNumber,
            decimal ratePerKm,
            int passengerSeats)
            : base(brand, model, registrationNumber, ratePerKm)
        {
            if (passengerSeats <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(passengerSeats),
                    "Количество мест должно быть больше нуля.");

            this.passengerSeats = passengerSeats;
        }

        public override string CapacityDescription =>
            $"{passengerSeats} пассажирских мест";

        public override decimal CalculateTripCost(decimal distance)
        {
            ValidateDistance(distance);

            return distance * RatePerKm;
        }

        public override string ToString()
        {
            return $"Легковой автомобиль: {base.ToString()}, " +
                   $"вместимость: {CapacityDescription}";
        }
    }
}