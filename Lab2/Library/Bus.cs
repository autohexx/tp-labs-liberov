namespace Library
{
    public class Bus : Vehicle
    {
        private int passengerSeats;

        public int PassengerSeats => passengerSeats;

        public Bus(
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

            return distance * (RatePerKm + passengerSeats * 0.5m);
        }

        public override string ToString()
        {
            return $"Автобус: {base.ToString()}, " +
                   $"вместимость: {CapacityDescription}";
        }
    }
}