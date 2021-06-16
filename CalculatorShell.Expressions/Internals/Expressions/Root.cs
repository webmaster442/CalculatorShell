using CalculatorShell.Expressions.Properties;
using System;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal sealed class Root : BinaryExpression
    {
        public Root(IExpression? left, IExpression? right) : base(left, right)
        {
        }

        public override IExpression? Differentiate(string byVariable)
        {
            if (Right is Constant)
            {
                // f(x) = g(x)^n
                // f'(x) = n * g'(x) * g(x)^(n-1)

                var newRight = new Divide(new Constant(new NumberImplementation(1)), Right);

                return
                    new Multiply(new Multiply(newRight, Left?.Differentiate(byVariable)),
                                           new Exponent(Left, new Subtract(newRight, new Constant(new NumberImplementation(1)))));
            }
            throw new ExpressionEngineException(Resources.CanotDifferentiate);
        }

        public override IExpression? Simplify()
        {
            var newLeft = Left?.Simplify();
            var newRight = Right?.Simplify();

            var leftConst = newLeft as Constant;
            var rightConst = newRight as Constant;

            if (leftConst != null && rightConst != null)
            {
                // two constants
                return new Constant(Math.Pow(leftConst.Value.Value,  1.0 / rightConst.Value.Value));
            }
            if (rightConst != null)
            {
                if (rightConst.Value.Value == 0)
                {
                    // x ^ 0
                    return new Constant(new NumberImplementation(1));
                }
                if (rightConst.Value.Value == 1)
                {
                    // x ^ 1
                    return newLeft;
                }
            }
            else if (leftConst?.Value.Value == 0)
            {
                // 0 ^ y
                return new Constant(new NumberImplementation(0));
            }
            // x ^ y;  no simplification
            return new Exponent(newLeft, new Divide(new Constant(new NumberImplementation(1)), newRight));
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"Root({Left?.ToString(formatProvider)}, {Right?.ToString(formatProvider)})";
        }

        protected override NumberImplementation Evaluate(NumberImplementation number1, NumberImplementation number2)
        {
            return Math.Pow(number1.Value, 1 / number2.Value);
        }
    }
}
