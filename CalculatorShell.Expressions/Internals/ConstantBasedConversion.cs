using System;

namespace CalculatorShell.Expressions.Internals
{
    internal sealed class ConstantBasedConversion
    {
        public string UnitName { get; init; }
        public decimal Value { get; init; }

        public bool IsBaseUnit => Value == 1M;

        public Func<decimal, decimal, decimal> ToBaseUnit { get; init; }
        public Func<decimal, decimal, decimal> ToTargetUnit { get; init; }

        public ConstantBasedConversion()
        {
            UnitName = string.Empty;
            ToBaseUnit = NoConversion;
            ToTargetUnit = NoConversion;
        }

        private decimal NoConversion(decimal arg1, decimal arg2)
        {
            if (!IsBaseUnit)
                throw new InvalidOperationException("Provide valid conversion");

            return 1M;
        }
    }
}
