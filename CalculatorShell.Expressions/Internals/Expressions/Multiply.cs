using CalculatorShell.Expressions.Properties;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal sealed class Multiply : BinaryExpression
    {
        public Multiply(IExpression? left, IExpression? right) : base(left, right)
        {
        }

        public override IExpression Differentiate(string byVariable)
        {
            return new Add(new Multiply(Left, Right?.Differentiate(byVariable)),
                      new Multiply(Left?.Differentiate(byVariable), Right));
        }

        public override IExpression Simplify()
        {
            IExpression? newLeft = Left?.Simplify();
            IExpression? newRight = Right?.Simplify();

            Constant? leftConst = newLeft as Constant;
            Constant? rightConst = newRight as Constant;
            Negate? leftNegate = newLeft as Negate;
            Negate? rightNegate = newRight as Negate;

            if (leftConst != null && rightConst != null)
            {
                // two constants
                return new Constant(new NumberImplementation(leftConst.Value.Value * rightConst.Value.Value));
            }
            if (leftConst != null)
            {
                if (leftConst.Value.Value == 0)
                {
                    // 0 * y
                    return new Constant(new NumberImplementation(0));
                }
                if (leftConst.Value.Value == 1)
                {
                    // 1 * y
                    return newRight ?? throw new ExpressionEngineException(Resources.InternalError);
                }
                if (leftConst.Value.Value == -1)
                {
                    // -1 * y
                    if (rightNegate != null)
                    {
                        // y = -u (-y = --u)
                        return rightNegate.Child ?? throw new ExpressionEngineException(Resources.InternalError);
                    }
                    return new Negate(newRight);
                }
            }
            else if (rightConst != null)
            {
                if (rightConst.Value.Value == 0)
                {
                    // x * 0
                    return new Constant(new NumberImplementation(0));
                }
                if (rightConst.Value.Value == 1)
                {
                    // x * 1
                    return newLeft ?? throw new ExpressionEngineException(Resources.InternalError);
                }
                if (rightConst.Value.Value == -1)
                {
                    // x * -1
                    if (leftNegate != null)
                    {
                        // x = -u (-x = --u)
                        return leftNegate.Child ?? throw new ExpressionEngineException(Resources.InternalError);
                    }
                    return new Negate(newLeft);
                }
            }
            else if (leftNegate != null && rightNegate != null)
            {
                // -x * -y
                return new Multiply(leftNegate.Child, rightNegate.Child);
            }
            // x * y
            return new Multiply(newLeft, newRight);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"({Left?.ToString(formatProvider)} * {Right?.ToString(formatProvider)})";
        }

        protected override NumberImplementation Evaluate(NumberImplementation number1, NumberImplementation number2)
        {
            return new NumberImplementation(number1.Value * number2.Value);
        }
    }
}
