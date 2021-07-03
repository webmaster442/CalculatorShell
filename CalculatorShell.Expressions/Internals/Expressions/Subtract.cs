using CalculatorShell.Expressions.Properties;
using System;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal sealed class Subtract : BinaryExpression
    {
        public Subtract(IExpression? left, IExpression? right) : base(left, right)
        {
        }

        public override IExpression Differentiate(string byVariable)
        {
            return new Subtract(Left?.Differentiate(byVariable), Right?.Differentiate(byVariable));
        }

        public override IExpression Simplify()
        {
            var newLeft = Left?.Simplify();
            var newRight = Right?.Simplify();

            var leftConst = newLeft as Constant;
            var rightConst = newRight as Constant;
            var rightNegate = newRight as Negate;

            if (leftConst != null && rightConst != null)
            {
                // two constants
                return new Constant(new NumberImplementation(leftConst.Value.Value - rightConst.Value.Value));
            }
            if (leftConst?.Value.Value == 0)
            {
                // 0 - y
                if (rightNegate != null)
                {
                    // y = -u (--u)
                    return rightNegate.Child ?? throw new ExpressionEngineException(Resources.InternalError);
                }
                return new Negate(newRight);
            }
            if (rightConst?.Value.Value == 0)
            {
                // x - 0
                return newLeft ?? throw new ExpressionEngineException(Resources.InternalError);
            }
            if (rightNegate != null)
            {
                // x - -y
                return new Add(newLeft, rightNegate.Child);
            }
            // x - y;  no simplification
            return new Subtract(newLeft, newRight);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"({Left?.ToString(formatProvider)} - {Right?.ToString(formatProvider)})";
        }

        protected override NumberImplementation Evaluate(NumberImplementation number1, NumberImplementation number2)
        {
            return new NumberImplementation(number1.Value - number2.Value);
        }
    }
}
