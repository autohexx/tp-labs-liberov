public class MathAlgorithms
{
    public static long Factorial(int n)
    {
        if (n < 0 || n > 20)
            throw new ArgumentOutOfRangeException(
                nameof(n),
                "n должен быть от 0 до 20.");

        long result = 1;

        for (int i = 2; i <= n; i++)
        {
            result *= i;
        }

        return result;
    }

    public static string Fibonacci(int n)
    {
        if (n < 0 || n > 20)
            throw new ArgumentOutOfRangeException(
                nameof(n),
                "n должен быть от 0 до 20.");

        long a = 0;
        long b = 1;

        string result = "";

        for (int i = 0; i < n; i++)
        {
            if (i > 0)
            {
                result += ", ";
            }

            result += a;

            long temp = a;
            a = b;
            b = temp + b;
        }

        return result;
    }

    public static double ValueFunction(double x)
    {
        if (x < -10 || x > 10)
            throw new ArgumentOutOfRangeException(
                nameof(x),
                "x должен быть от -10 до 10.");

        double value =
            Math.Sqrt(Math.Log(4.0 / 3.0))
            + (x + 9.0 / 7.0)
            - Math.Exp(Math.Sin(1.3 * x - 0.7));

        return value;
    }

    public static (double Sum, int Count) Taylor(double x)
    {
        if (x < -100 || x > 100)
            throw new ArgumentOutOfRangeException(
                nameof(x),
                "x должен быть от -100 до 100.");

        double sum = 0.0;
        double term = x;

        int n = 0;
        int count = 0;

        while (Math.Abs(term) > 1e-6)
        {
            sum += term;

            n++;
            count++;

            term *=
                -x * x /
                ((2 * n) * (2 * n + 1));
        }

        return (sum, count);
    }
}