using CalculatorShell.Expressions.Properties;
using System;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal sealed class And : BinaryExpression
    {
        public And(IExpression? left, IExpression? right) : base(left, right)
        {
        }

        public override IExpression Differentiate(string byVariable)
        {
            throw new ExpressionEngineException(Resources.CanotDifferentiate);
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
                return new Constant(new NumberImplementation(leftConst.Value.Value & rightConst.Value.Value));
            }
            if (leftConst?.Value.Value == true)
            {
                return newRight ?? throw new ExpressionEngineException(Resources.InternalError);
            }
            else if (leftConst?.Value.Value == false)
            {
                return new Constant(new NumberImplementation(false));
            }
            else if (rightConst?.Value.Value == true)
            {
                return newLeft ?? throw new ExpressionEngineException(Resources.InternalError);
            }
            else if (rightConst?.Value.Value == false)
            {
                return new Constant(new NumberImplementation(false));
            }

            return new And(newLeft, newRight);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"({Left?.ToString(formatProvider)} & {Right?.ToString(formatProvider)})";
        }

        protected override NumberImplementation Evaluate(NumberImplementation number1, NumberImplementation number2)
        {
            return new NumberImplementation(number1.Value && number2.Value);
        }
    }
}
