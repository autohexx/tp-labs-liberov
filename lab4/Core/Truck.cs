public class Truck : Vehicle
{
    private double _loadCapacity;

    public double LoadCapacity
    {
        get { return _loadCapacity; }
    }

    public Truck(
        string brand,
        string model,
        double fuelConsumption,
        double loadCapacity)
        : base(brand, model, fuelConsumption)
    {
        if (loadCapacity <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(loadCapacity));

        _loadCapacity = loadCapacity;
    }

    public override string GetCapacityInfo()
    {
        return $"Грузоподъёмность {LoadCapacity} т";
    }

    public override string ToString()
    {
        return $"Грузовик: {base.ToString()}";
    }
}