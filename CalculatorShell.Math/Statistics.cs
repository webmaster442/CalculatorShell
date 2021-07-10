using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorShell.Maths
{
    public static class Statistics
    {
        public static StatisticResult Calculate(params double[] numbers)
        {
            StatisticResult ret = new();

            if (numbers.Length < 1)
                return ret;

            ret.Maximum = numbers[0];
            ret.Minimum = numbers[0];
            ret.Sum = 0.0;
            ret.Count = numbers.Length;

            for (int i=0; i<numbers.Length; i++)
            {
                //Maximum
                if (numbers[i] > ret.Maximum)
                    ret.Maximum = numbers[i];

                //Minimum
                if (numbers[i] < ret.Minimum)
                    ret.Minimum = numbers[i];

                //Sum
                ret.Sum += numbers[i];
            }

            ret.Median = ComputeMedian(numbers);

            return ret;
        }

        private static double ComputeMedian(double[] numbers)
        {
            int halfindex = numbers.Length / 2;
            var sorted = numbers.OrderBy(x => x);
            if ((numbers.Length %2) == 0)
                return (sorted.ElementAt(halfindex) + sorted.ElementAt(halfindex - 1)) / 2;
            else
                return sorted.ElementAt(halfindex);
        }
    }
}