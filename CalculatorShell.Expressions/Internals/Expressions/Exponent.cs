﻿using CalculatorShell.Expressions.Properties;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal sealed class Exponent : BinaryExpression
    {
        public Exponent(IExpression? left, IExpression? right) : base(left, right)
        {
        }

        public override IExpression Differentiate(string byVariable)
        {
            if (Right is Constant)
            {
                // f(x) = g(x)^n
                // f'(x) = n * g'(x) * g(x)^(n-1)
                return
                    new Multiply(new Multiply(Right, Left?.Differentiate(byVariable)),
                                 new Exponent(Left, new Subtract(Right, new Constant(new NumberImplementation(1)))));
            }
            IExpression? simple = Left?.Simplify();
            if (simple is Constant constant)
            {
                // f(x) = a^g(x)
                // f'(x) = (ln a) * g'(x) * a^g(x)
                NumberImplementation? a = constant.Value;
                Multiply? newleft = new Multiply(new Constant(new NumberImplementation(Math.Log(a.Value))), Right?.Differentiate(byVariable));
                return new Multiply(newleft, new Exponent(simple, Right));
            }
            throw new ExpressionEngineException(Resources.CanotDifferentiate);
        }

        public override IExpression Simplify()
        {
            IExpression? newLeft = Left?.Simplify();
            IExpression? newRight = Right?.Simplify();

            Constant? leftConst = newLeft as Constant;
            Constant? rightConst = newRight as Constant;

            if (leftConst != null && rightConst != null)
            {
                // two constants
                return new Constant(new NumberImplementation(Math.Pow(leftConst.Value.Value, rightConst.Value.Value)));
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
                    return newLeft ?? throw new ExpressionEngineException(Resources.InternalError);
                }
            }
            else if (leftConst?.Value.Value == 0)
            {
                // 0 ^ y
                return new Constant(new NumberImplementation(0));
            }
            // x ^ y;  no simplification
            return new Exponent(newLeft, newRight);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"({Left?.ToString(formatProvider)} ^ {Right?.ToString(formatProvider)})";
        }

        protected override NumberImplementation Evaluate(NumberImplementation number1, NumberImplementation number2)
        {
            return new NumberImplementation(Math.Pow(number1.Value, number2.Value));
        }
    }
}
