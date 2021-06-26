using CalculatorShell.Expressions.Properties;
using System;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal class Mod : BinaryExpression
    {
        public Mod(IExpression? left, IExpression? right) : base(left, right)
        {
        }

        public override IExpression Differentiate(string byVariable)
        {
            var newLeft = Left?.Simplify();
            var newRight = Right?.Simplify();

            var leftConst = newLeft as Constant;
            var rightConst = newRight as Constant;

            if (leftConst != null && rightConst != null)
            {
                if (Math.Abs(leftConst.Value.Value % rightConst.Value.Value) < 1E-9)
                    throw new ExpressionEngineException(Resources.CanotDifferentiate);
                else
                    return new Constant(new NumberImplementation(1));
            }

            throw new ExpressionEngineException(Resources.CanotDifferentiate);
        }

        public override IExpression Simplify()
        {
            var newLeft = Left?.Simplify();
            var newRight = Right?.Simplify();

            var leftConst = newLeft as Constant;
            var rightConst = newRight as Constant;
            var leftNegate = newLeft as Negate;
            var rightNegate = newRight as Negate;

            if (leftConst != null && rightConst != null)
            {
                // two constants
                if (rightConst.Value.Value == 0)
                {
                    throw new ExpressionEngineException(Resources.DivideByZero);
                }
                else
                {
                    return new Constant(new NumberImplementation(leftConst.Value.Value % rightConst.Value.Value));
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
            else if (leftNegate != null && rightNegate != null)
            {
                // -x / -y
                return new Mod(leftNegate.Child, rightNegate.Child);
            }
            // x / y;  no simplification
            return new Mod(newLeft, newRight);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"({Left?.ToString(formatProvider)} % {Right?.ToString(formatProvider)})";
        }

        protected override NumberImplementation Evaluate(NumberImplementation number1, NumberImplementation number2)
        {
            return new NumberImplementation(number1.Value % number2.Value);
        }
    }
}
