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

            return ret;
        }
    }
}
