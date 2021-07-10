using System;
using System.Globalization;

namespace CalculatorShell.Expressions.Internals
{
    internal abstract class BinaryExpression : IExpression
    {
        public IVariables? Variables
        {
            get
            {
                if (Left?.Variables != null)
                    return Left.Variables;
                else if (Right?.Variables != null)
                    return Right.Variables;
                else
                    return null;
            }
        }

        protected BinaryExpression(IExpression? left, IExpression? right)
        {
            Left = left;
            Right = right;
        }

        public IExpression? Left { get; }
        public IExpression? Right { get; }

        public INumber Evaluate()
        {
            if (Left?.Evaluate() is NumberImplementation implementation1
                && Right?.Evaluate() is NumberImplementation implementation2)
            {
                return Evaluate(implementation1, implementation2);
            }
            return Evaluate(new NumberImplementation(double.NaN), new NumberImplementation(double.NaN));
        }

        protected abstract NumberImplementation Evaluate(NumberImplementation number1, NumberImplementation number2);

        public abstract IExpression Simplify();

        public abstract IExpression Differentiate(string byVariable);

        public abstract string ToString(IFormatProvider formatProvider);

        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }
    }
}
