namespace CalculatorShell.Expressions.Internals
{
    internal sealed class ConstantBasedConversion
    {
        public string UnitName { get; init; }
        public decimal Value { get; init; }

        public bool IsBaseUnit => Value == 1M;

        public Func<decimal, decimal, decimal, decimal> BaseRelationShip { get; init; }
        public Func<decimal, decimal, decimal, decimal> TargetRelationShip { get; init; }

        public ConstantBasedConversion()
        {
            UnitName = string.Empty;
            BaseRelationShip = NoConversion;
            TargetRelationShip = NoConversion;
        }

        private decimal NoConversion(decimal rawValue, decimal constantMultiplier, decimal prefix)
        {
            if (!IsBaseUnit)
                throw new InvalidOperationException("Provide valid conversion");

            if (prefix < 1M)
                return 1M / prefix;
            else
                return 1M * prefix;
        }
    }
}
