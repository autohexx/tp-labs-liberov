public abstract class Vehicle
{
    private string _brand;
    private string _model;
    private double _fuelConsumption;
    private double _mileage;

    public string Brand
    {
        get { return _brand; }
    }

    public string Model
    {
        get { return _model; }
    }

    public double FuelConsumption
    {
        get { return _fuelConsumption; }
    }

    public double Mileage
    {
        get { return _mileage; }
    }

    protected Vehicle(
        string brand,
        string model,
        double fuelConsumption)
    {
        if (string.IsNullOrWhiteSpace(brand))
            throw new ArgumentException(
                "Марка не может быть пустой.",
                nameof(brand));

        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException(
                "Модель не может быть пустой.",
                nameof(model));

        if (fuelConsumption <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(fuelConsumption),
                "Расход топлива должен быть больше нуля.");

        _brand = brand;
        _model = model;
        _fuelConsumption = fuelConsumption;
        _mileage = 0;
    }

    public double CalculateTripCost(
        double distanceKm,
        double fuelPrice)
    {
        if (distanceKm <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(distanceKm));

        if (fuelPrice <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(fuelPrice));

        double usedFuel =
            distanceKm / 100 * FuelConsumption;

        return usedFuel * fuelPrice;
    }

    public void Drive(double distanceKm)
    {
        if (distanceKm <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(distanceKm));

        _mileage += distanceKm;
    }

    public abstract string GetCapacityInfo();

    public override string ToString()
    {
        return $"{Brand} {Model}, расход: {FuelConsumption} л/100 км, пробег: {Mileage} км";
    }
}