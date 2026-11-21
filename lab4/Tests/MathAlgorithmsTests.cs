namespace Tests;

public class MathAlgorithmsTests
{
    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(5, 120)]
    [InlineData(10, 3628800)]
    [InlineData(20, 2432902008176640000)]
    public void Factorial_ValidNumber_ReturnsExpected(
        int n,
        long expected)
    {
        long result = MathAlgorithms.Factorial(n);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Factorial_NegativeNumber_ThrowsException()
    {
        Action action = () => MathAlgorithms.Factorial(-1);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void Factorial_NumberAboveTwenty_ThrowsException()
    {
        Action action = () => MathAlgorithms.Factorial(21);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }


    [Theory]
    [InlineData(1, "0")]
    [InlineData(2, "0, 1")]
    [InlineData(5, "0, 1, 1, 2, 3")]
    [InlineData(8, "0, 1, 1, 2, 3, 5, 8, 13")]
    public void Fibonacci_ValidCount_ReturnsExpected(
        int n,
        string expected)
    {
        string result = MathAlgorithms.Fibonacci(n);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Fibonacci_Zero_ReturnsEmptyString()
    {
        string result = MathAlgorithms.Fibonacci(0);

        Assert.Equal("", result);
    }

    [Fact]
    public void Fibonacci_NegativeNumber_ThrowsException()
    {
        Action action = () => MathAlgorithms.Fibonacci(-1);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void Fibonacci_NumberAboveTwenty_ThrowsException()
    {
        Action action = () => MathAlgorithms.Fibonacci(21);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }


    [Theory]
    [InlineData(-10)]
    [InlineData(-5)]
    [InlineData(0)]
    [InlineData(5)]
    [InlineData(10)]
    public void ValueFunction_ValidX_ReturnsExpected(
        double x)
    {
        double expected = Math.Sqrt(Math.Log(4.0 / 3.0)) + (x + 9.0 / 7.0) - Math.Exp(Math.Sin(1.3 * x - 0.7));

        double result = MathAlgorithms.ValueFunction(x);

        Assert.Equal(expected, result, 10);
    }

    [Fact]
    public void ValueFunction_LowerBoundary_IsAllowed()
    {
        double result = MathAlgorithms.ValueFunction(-10);

        Assert.False(double.IsNaN(result));
    }

    [Fact]
    public void ValueFunction_UpperBoundary_IsAllowed()
    {
        double result = MathAlgorithms.ValueFunction(10);

        Assert.False(double.IsNaN(result));
    }

    [Fact]
    public void ValueFunction_BelowRange_ThrowsException()
    {
        Action action = () => MathAlgorithms.ValueFunction(-10.1);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void ValueFunction_AboveRange_ThrowsException()
    {
        Action action = () => MathAlgorithms.ValueFunction(10.1);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }


    [Theory]
    [InlineData(0)]
    [InlineData(0.5)]
    [InlineData(1)]
    [InlineData(-1)]
    [InlineData(3)]
    public void Taylor_ValidX_CloseToMathSin(
        double x)
    {
        double expected = Math.Sin(x);

        (double sum, int count) = MathAlgorithms.Taylor(x);

        Assert.Equal(expected, sum, 5);
    }

    [Fact]
    public void Taylor_Zero_ReturnsZeroAndZeroTerms()
    {
        (double sum, int count) = MathAlgorithms.Taylor(0);

        Assert.Equal(0, sum);
        Assert.Equal(0, count);
    }

    [Fact]
    public void Taylor_One_UsesAtLeastOneTerm()
    {
        (double sum, int count) = MathAlgorithms.Taylor(1);

        Assert.True(count > 0);
    }

    [Fact]
    public void Taylor_LowerBoundary_IsAllowed()
    {
        var result = MathAlgorithms.Taylor(-100);

        Assert.True(result.Count > 0);
    }

    [Fact]
    public void Taylor_UpperBoundary_IsAllowed()
    {
        var result = MathAlgorithms.Taylor(100);

        Assert.True(result.Count > 0);
    }

    [Fact]
    public void Taylor_BelowRange_ThrowsException()
    {
        Action action = () => MathAlgorithms.Taylor(-100.1);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void Taylor_AboveRange_ThrowsException()
    {
        Action action = () => MathAlgorithms.Taylor(100.1);

        Assert.Throws<ArgumentOutOfRangeException>(action);
    }
}