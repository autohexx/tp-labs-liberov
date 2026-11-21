public class Bus : Vehicle
{
    private int _passengerCapacity;

    public int PassengerCapacity
    {
        get { return _passengerCapacity; }
    }

    public Bus(
        string brand,
        string model,
        double fuelConsumption,
        int passengerCapacity)
        : base(brand, model, fuelConsumption)
    {
        if (passengerCapacity <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(passengerCapacity));

        _passengerCapacity = passengerCapacity;
    }

    public override string GetCapacityInfo()
    {
        return $"{PassengerCapacity} пассажиров";
    }

    public override string ToString()
    {
        return $"Автобус: {base.ToString()}";
    }
}