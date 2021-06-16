using System;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal sealed class Log : BinaryExpression
    {
        public Log(IExpression? left, IExpression? right) : base(left, right)
        {
        }

        public override IExpression? Differentiate(string byVariable)
        {
            return new Multiply(new Divide(new Constant(new NumberImplementation(1)), new Ln(Right)),
                                           new Divide(new Constant(new NumberImplementation(1)), Left));
        }

        public override IExpression? Simplify()
        {
            var newLeft = Left?.Simplify();
            var newRight = Right?.Simplify();

            if (newLeft is Constant leftConst
                && newRight is Constant rightConst)
            {
                // two constants
                return new Constant(Math.Log(leftConst.Value.Value, rightConst.Value.Value));
            }
            else
            {
                return new Log(newLeft, newRight);
            }
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"log({Left?.ToString(formatProvider)}, {Right?.ToString(formatProvider)})";
        }

        protected override NumberImplementation Evaluate(NumberImplementation number1, NumberImplementation number2)
        {
            return new NumberImplementation(Math.Log(number1.Value, number2.Value));
        }
    }
}
