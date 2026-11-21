using System.Diagnostics;

namespace Core
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const long Terms = 100_000_000;

            Console.Write("Введите количества потоков через пробел: ");

            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ошибка: ничего не введено.");
                return;
            }

            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            List<int> threadCounts = [];

            foreach (string part in parts)
            {
                if (!int.TryParse(part, out int count) || count <= 0)
                {
                    Console.WriteLine($"Ошибка: '{part}' не является корректным количеством потоков.");
                    return;
                }

                if (!threadCounts.Contains(count))
                {
                    threadCounts.Add(count);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Последовательное вычисление:");

            Stopwatch stopwatch = Stopwatch.StartNew();
            double sequentialPi = PiCalculator.CalculateSequential(Terms);
            stopwatch.Stop();

            long sequentialTime = stopwatch.ElapsedMilliseconds;
            double sequentialError = Math.Abs(Math.PI - sequentialPi);

            Console.WriteLine($"Полученное PI: {sequentialPi:F15}");
            Console.WriteLine($"Math.PI: {Math.PI:F15}");
            Console.WriteLine($"Погрешность: {sequentialError:E6}");
            Console.WriteLine($"Время: {sequentialTime} мс");

            Console.WriteLine();
            Console.WriteLine("Многопоточные вычисления:");

            Console.WriteLine($"{"Потоков",-10}" + $"{"Время, мс",-15}" + $"{"Ускорение",-15}" + $"{"Совпадение",-15}");
            Console.WriteLine(new string('-', 55));

            foreach (int threadCount in threadCounts)
            {
                stopwatch.Restart();

                double parallelPi = PiCalculator.CalculateParallel(Terms, threadCount);

                stopwatch.Stop();

                long parallelTime = stopwatch.ElapsedMilliseconds;

                double speedup = parallelTime == 0 ? 0 : (double)sequentialTime / parallelTime;

                double difference = Math.Abs(sequentialPi - parallelPi);

                bool resultsMatch = difference < 1e-10;

                Console.WriteLine($"{threadCount,-10}" + $"{parallelTime,-15}" + $"{speedup,-15:F2}" + $"{(resultsMatch ? "Да" : "Нет"),-15}");
            }
        }
    }
}
