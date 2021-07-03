using CalculatorShell.Expressions.Properties;
using System;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal sealed class Add : BinaryExpression
    {
        public Add(IExpression? left, IExpression? right) : base(left, right)
        {
        }

        public override IExpression Differentiate(string byVariable)
        {
            return new Add(Left?.Differentiate(byVariable), Right?.Differentiate(byVariable));
        }

        public override IExpression Simplify()
        {
            var newLeft = Left?.Simplify();
            var newRight = Right?.Simplify();

            var leftConst = newLeft as Constant;
            var rightConst = newRight as Constant;

            if (leftConst != null && rightConst != null)
            {
                // two constants
                return new Constant(new NumberImplementation(leftConst.Value.Value + rightConst.Value.Value));
            }
            if (leftConst?.Value.Value == 0)
            {
                // 0 + y
                return newRight ?? throw new ExpressionEngineException(Resources.InternalError);
            }
            if (rightConst?.Value.Value == 0)
            {
                // x + 0
                return newLeft ?? throw new ExpressionEngineException(Resources.InternalError);
            }
            if (newRight is Negate rightNegate)
            {
                // x + -y;  return x - y;  (this covers -x + -y case too)
                return new Subtract(newLeft, rightNegate.Child);
            }
            if (newLeft is Negate leftNegate)
            {
                // -x + y
                return new Subtract(newRight, leftNegate.Child);
            }
            // x + y;  no simplification
            return new Add(newLeft, newRight);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"({Left?.ToString(formatProvider)} + {Right?.ToString(formatProvider)})";
        }

        protected override NumberImplementation Evaluate(NumberImplementation number1, NumberImplementation number2)
        {
            return new NumberImplementation(number1.Value + number2.Value);
        }
    }
}
