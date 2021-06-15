using System;
using System.Globalization;

namespace CalculatorShell.Expressions.Internals
{
    internal abstract class UnaryExpression : IExpression
    {
        public IVariables? Variables { get; }

        protected UnaryExpression(IExpression? child)
        {
            Child = child;
        }

        public IExpression? Child { get; }

        public INumber Evaluate()
        {
            if (Child?.Evaluate() is NumberImplementation implementation)
            {
                return Evaluate(implementation);
            }
            return Evaluate(new NumberImplementation(double.NaN));
        }

        protected abstract NumberImplementation Evaluate(NumberImplementation number);

        public abstract IExpression? Simplify();

        public abstract IExpression? Differentiate(string byVariable);

        public abstract string ToString(IFormatProvider formatProvider);

        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }
    }
}
