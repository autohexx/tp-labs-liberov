namespace Tests;

public class VehicleTests
{
    [Theory]
    [InlineData(100, 60, 420)]
    [InlineData(200, 60, 840)]
    [InlineData(500, 50, 1750)]
    public void CalculateTripCost_ValidData_ReturnsExpected(
        double distance,
        double fuelPrice,
        double expected)
    {
        Car car = new Car("Lada", "Vesta", 7, 5, 480);

        double result = car.CalculateTripCost(distance,fuelPrice);

        Assert.Equal(expected, result, 2);
    }

    [Fact]
    public void CalculateTripCost_ZeroDistance_ThrowsException()
    {
        Car car = new Car("Lada", "Vesta", 7, 5, 480);

        Action action = () => car.CalculateTripCost(0, 60);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void CalculateTripCost_ZeroFuelPrice_ThrowsException()
    {
        Car car = new Car("Lada", "Vesta", 7, 5, 480);

        Action action = () => car.CalculateTripCost(100, 0);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void Drive_NewVehicle_IncreasesMileage()
    {
        Car car = new Car("Lada", "Vesta", 7.1, 5, 480);

        car.Drive(150);

        Assert.Equal(150, car.Mileage);
    }

    [Fact]
    public void Drive_TwoTrips_AccumulatesMileage()
    {
        Car car = new Car("Lada", "Vesta", 7.1, 5, 480);

        car.Drive(100);
        car.Drive(250);

        Assert.Equal(350, car.Mileage);
    }

    [Fact]
    public void Drive_ZeroDistance_ThrowsException()
    {
        Car car = new Car("Lada", "Vesta", 7.1, 5, 480);

        Action action = () => car.Drive(0);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void Car_ValidArguments_StoresProperties()
    {
        string brand = "Toyota";
        string model = "Camry";
        double fuelConsumption = 8.5;
        int passenger = 5;
        int trunkVolume = 493;

        Car car = new Car(brand, model, fuelConsumption, passenger, trunkVolume);

        Assert.Equal(brand, car.Brand);
        Assert.Equal(model, car.Model);
        Assert.Equal(fuelConsumption, car.FuelConsumption);
        Assert.Equal(passenger, car.PassengerCapacity);
        Assert.Equal(trunkVolume, car.TrunkVolume);
    }

    [Fact]
    public void Car_ZeroPassengerCapacity_ThrowsException()
    {
        Action action = () => new Car("Lada", "Vesta", 7.1, 0, 480);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void Car_NegativeTrunkVolume_ThrowsException()
    {
        Action action = () => new Car("Lada", "Vesta", 7.1, 5, -1);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void Truck_ValidArguments_ReturnsCapacityInformation()
    {
        Truck truck = new Truck("KamAZ", "54901", 25, 20);

        string result = truck.GetCapacityInfo();

        Assert.Equal("Грузоподъёмность 20 т", result);
    }

    [Fact]
    public void Truck_ZeroLoadCapacity_ThrowsException()
    {
        Action action = () => new Truck("KamAZ", "54901", 25, 0);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void Bus_ValidArguments_ReturnsCapacityInformation()
    {
        Bus bus = new Bus("PAZ", "3205", 18, 42);

        string result = bus.GetCapacityInfo();

        Assert.Equal("42 пассажиров", result);
    }

    [Fact]
    public void Bus_ZeroPassengerCapacity_ThrowsException()
    {
        Action action = () => new Bus("PAZ", "3205", 18, 0);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void Vehicle_EmptyBrand_ThrowsException()
    {
        Action action = () => new Car("", "Vesta", 7.1, 5, 480);

        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Vehicle_ZeroFuelConsumption_ThrowsException()
    {
        Action action = () => new Car("Lada", "Vesta", 0, 5, 480);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void Car_ToString_ContainsBrandAndModel()
    {
        Car car = new Car("Lada", "Vesta", 7.1, 5, 480);

        string result = car.ToString();

        Assert.Contains("Lada", result);
        Assert.Contains("Vesta", result);
    }

    [Fact]
    public void Polymorphism_DifferentVehicles_ReturnCapacityInfo()
    {
        List<Vehicle> vehicles =
        [
            new Car("Lada", "Vesta", 7.1, 5, 480),
            new Truck("KamAZ", "54901", 25, 20),
            new Bus("PAZ", "3205", 18, 42)
        ];

        List<string> result = vehicles.Select(vehicle => vehicle.GetCapacityInfo()).ToList();

        Assert.Equal(3, result.Count);
        Assert.Contains("5 человек, багажник 480 л", result);
        Assert.Contains("Грузоподъёмность 20 т", result);
        Assert.Contains("42 пассажиров", result);
    }
}