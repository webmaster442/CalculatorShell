using System;
using System.Globalization;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal class Constant : IExpression
    {
        public IVariables? Variables { get; }

        public NumberImplementation Value { get; }

        public Constant(NumberImplementation value)
        {
            Value = value;
        }

        public IExpression? Differentiate(string byVariable)
        {
            return new Constant(new NumberImplementation(0.0));
        }

        public INumber Evaluate()
        {
            return Value;
        }

        public IExpression? Simplify()
        {
            return new Constant(Value);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return Value.ToString(formatProvider);
        }

        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }
    }
}
