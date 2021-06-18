using CalculatorShell.Expressions.Properties;
using System;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal sealed class Function : UnaryExpression
    {
        private readonly Func<double, double> _function;
        private readonly string _name;

        public Function(IExpression? child, Func<double, double> function, string name) : base(child)
        {
            _function = function;
            _name = name;
        }

        public override IExpression? Differentiate(string byVariable)
        {
            throw new ExpressionEngineException(Resources.CanotDifferentiate);
        }

        public override IExpression? Simplify()
        {
            var newChild = Child?.Simplify();
            if (newChild is Constant childConst)
            {
                // child is constant
                return new Constant(Evaluate(childConst.Value));
            }
            return new Function(newChild, _function, _name);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"{_name}({Child?.ToString(formatProvider)})";
        }

        protected override NumberImplementation Evaluate(NumberImplementation number)
        {
            return new NumberImplementation(_function.Invoke(number.Value));
        }
    }
}
