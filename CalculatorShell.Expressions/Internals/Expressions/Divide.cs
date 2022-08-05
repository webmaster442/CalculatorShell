using CalculatorShell.Expressions.Properties;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal sealed class Divide : BinaryExpression
    {
        public Divide(IExpression? left, IExpression? right) : base(left, right)
        {
        }

        public override IExpression Differentiate(string byVariable)
        {
            return
                new Divide(new Subtract(new Multiply(Left?.Differentiate(byVariable), Right),
                           new Multiply(Left, Right?.Differentiate(byVariable))),
                           new Exponent(Right, new Constant(new NumberImplementation(2))));
        }

        public override IExpression Simplify()
        {
            IExpression? newLeft = Left?.Simplify();
            IExpression? newRight = Right?.Simplify();

            Constant? leftConst = newLeft as Constant;
            Constant? rightConst = newRight as Constant;
            Negate? leftNegate = newLeft as Negate;

            if (leftConst != null && rightConst != null)
            {
                // two constants
                if (rightConst.Value.Value == 0)
                {
                    throw new ExpressionEngineException(Resources.DivideByZero);
                }
                if (leftConst.Value.IsInteger
                    && rightConst.Value.IsInteger)
                {
                    return new Constant(new NumberImplementation(leftConst.Value.Value, rightConst.Value.Value));
                }
                else
                {
                    return new Constant(new NumberImplementation(leftConst.Value.Value / rightConst.Value.Value));
                }
            }
            if (leftConst?.Value.Value == 0)
            {
                // 0 / y
                if (rightConst?.Value.Value == 0)
                {
                    throw new ExpressionEngineException(Resources.DivideByZero);
                }
                return new Constant(new NumberImplementation(0));
            }
            else if (rightConst != null)
            {
                if (rightConst.Value.Value == 0)
                {
                    // x / 0
                    throw new ExpressionEngineException(Resources.DivideByZero);
                }
                if (rightConst.Value.Value == 1)
                {
                    // x / 1
                    return newLeft ?? throw new ExpressionEngineException(Resources.InternalError);
                }
                if (rightConst.Value.Value == -1)
                {
                    // x / -1
                    if (leftNegate != null)
                    {
                        // x = -u (-x = --u)
                        return leftNegate.Child ?? throw new ExpressionEngineException(Resources.InternalError);
                    }
                    return new Negate(newLeft);
                }
            }
            else if (leftNegate != null && newRight is Negate rightNegate)
            {
                // -x / -y
                return new Divide(leftNegate.Child, rightNegate.Child);
            }
            // x / y;  no simplification
            return new Divide(newLeft, newRight);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"({Left?.ToString(formatProvider)} / {Right?.ToString(formatProvider)})";
        }

        protected override NumberImplementation Evaluate(NumberImplementation number1, NumberImplementation number2)
        {
            if (number1.IsInteger
                && number2.IsInteger)
            {
                return new NumberImplementation(number1.Value, number2.Value);
            }
            else
            {
                return new NumberImplementation(number1.Value / number2.Value);
            }
        }
    }
}
