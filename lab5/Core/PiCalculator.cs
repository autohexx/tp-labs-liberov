public static class PiCalculator
{
    public static double CalculateSequential(long terms)
    {
        if (terms <= 0)
            throw new ArgumentOutOfRangeException(nameof(terms), "Количество членов должно быть больше нуля.");

        double sum = SumRange(0, terms);

        return 4.0 * sum;
    }

    public static double CalculateParallel(
        long terms,
        int threadCount)
    {
        if (terms <= 0)
            throw new ArgumentOutOfRangeException(nameof(terms), "Количество членов должно быть больше нуля.");

        if (threadCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(threadCount), "Количество потоков должно быть больше нуля.");

        if (threadCount > terms)
            throw new ArgumentException("Количество потоков не может быть больше количества членов.");

        Thread[] threads = new Thread[threadCount];

        double[] partialSums = new double[threadCount];

        long partSize = terms / threadCount;
        long remainder = terms % threadCount;

        long start = 0;

        for (int i = 0; i < threadCount; i++)
        {
            long size = partSize;

            if (i < remainder)
            {
                size++;
            }

            long rangeStart = start;
            long rangeEnd = rangeStart + size;
            int index = i;

            threads[i] = new Thread(() =>
            {
                partialSums[index] = SumRange(rangeStart, rangeEnd);
            });

            start = rangeEnd;
        }

        foreach (Thread thread in threads)
        {
            thread.Start();
        }

        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        double sum = 0;

        foreach (double partialSum in partialSums)
        {
            sum += partialSum;
        }

        return 4.0 * sum;
    }

    private static double SumRange(
        long start,
        long end)
    {
        double sum = 0;

        double sign = start % 2 == 0 ? 1.0 : -1.0;

        for (long i = start; i < end; i++)
        {
            double denominator = 2.0 * i + 1.0;

            sum += sign / denominator;

            sign = -sign;
        }

        return sum;
    }
}