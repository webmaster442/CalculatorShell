using CalculatorShell.Expressions.Properties;
using System;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal sealed class Function2 : BinaryExpression
    {
        private readonly Func<double, double, double> _function;
        private readonly string _name;

        public Function2(
            IExpression? left,
            IExpression? right,
            Func<double, double, double> function,
            string name) : base(left, right)
        {
            _function = function;
            _name = name;
        }

        public override IExpression Differentiate(string byVariable)
        {
            throw new ExpressionEngineException(Resources.CanotDifferentiate);
        }

        public override IExpression Simplify()
        {
            var newLeft = Left?.Simplify();
            var newRight = Right?.Simplify();

            if (newLeft is Constant leftConst
                && newRight is Constant rightConst)
            {
                // two constants
                return new Constant(Evaluate(leftConst.Value.Value, rightConst.Value.Value));
            }
            else
            {
                return new Function2(newLeft, newRight, _function, _name);
            }
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"{_name}({Left?.ToString(formatProvider)}; {Right?.ToString(formatProvider)})";
        }

        protected override NumberImplementation Evaluate(NumberImplementation number1, NumberImplementation number2)
        {
            return new NumberImplementation(_function.Invoke(number1.Value, number2.Value));
        }
    }
}
