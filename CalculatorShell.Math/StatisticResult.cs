namespace CalculatorShell.Maths
{
    public sealed record StatisticResult
    {
        public double Minimum { get; internal set; }
        public double Maximum { get; internal set; }
        public double Sum { get; internal set; }
        public double Average => Sum / Count;
        public double Count { get; internal set; }
        public double Range => Maximum - Minimum;

        public StatisticResult()
        {
            Minimum = double.NaN;
            Maximum = double.NaN;
            Sum = double.NaN;
            Count = 0;
        }
    }
}
