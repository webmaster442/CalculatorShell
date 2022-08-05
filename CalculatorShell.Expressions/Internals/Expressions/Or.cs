using CalculatorShell.Expressions.Properties;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal sealed class Or : BinaryExpression
    {
        public Or(IExpression? left, IExpression? right) : base(left, right)
        {
        }

        public override IExpression Differentiate(string byVariable)
        {
            throw new ExpressionEngineException(Resources.CanotDifferentiate);
        }

        public override IExpression Simplify()
        {
#pragma warning disable S1125 // Boolean literals should not be redundant
            IExpression? newLeft = Left?.Simplify();
            IExpression? newRight = Right?.Simplify();

            Constant? leftConst = newLeft as Constant;
            Constant? rightConst = newRight as Constant;

            if (leftConst != null && rightConst != null)
            {
                // two constants
                return new Constant(new NumberImplementation(leftConst.Value.Value | rightConst.Value.Value));
            }

            if (leftConst?.Value.Value == true)
            {
                return new Constant(new NumberImplementation(true));
            }
            else if (leftConst?.Value.Value == false)
            {
                return newRight ?? throw new ExpressionEngineException(Resources.InternalError);
            }
            else if (rightConst?.Value.Value == true)
            {
                return new Constant(new NumberImplementation(true));
            }
            else if (rightConst?.Value.Value == false)
            {
                return newLeft ?? throw new ExpressionEngineException(Resources.InternalError);
            }
#pragma warning restore S1125 // Boolean literals should not be redundant

            return new Or(newLeft, newRight);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"({Left?.ToString(formatProvider)} | {Right?.ToString(formatProvider)})";
        }

        protected override NumberImplementation Evaluate(NumberImplementation number1, NumberImplementation number2)
        {
            return new NumberImplementation(number1.Value || number2.Value);
        }
    }
}
