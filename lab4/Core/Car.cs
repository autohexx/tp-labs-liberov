public class Car : Vehicle
{
    private int _passengerCapacity;
    private int _trunkVolume;

    public int PassengerCapacity
    {
        get { return _passengerCapacity; }
    }

    public int TrunkVolume
    {
        get { return _trunkVolume; }
    }

    public Car(
        string brand,
        string model,
        double fuelConsumption,
        int passengerCapacity,
        int trunkVolume)
        : base(brand, model, fuelConsumption)
    {
        if (passengerCapacity <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(passengerCapacity));

        if (trunkVolume < 0)
            throw new ArgumentOutOfRangeException(
                nameof(trunkVolume));

        _passengerCapacity = passengerCapacity;
        _trunkVolume = trunkVolume;
    }

    public override string GetCapacityInfo()
    {
        return $"{PassengerCapacity} человек, багажник {TrunkVolume} л";
    }

    public override string ToString()
    {
        return $"Легковой автомобиль: {base.ToString()}";
    }
}