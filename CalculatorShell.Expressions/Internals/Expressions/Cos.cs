using System;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal sealed class Cos : UnaryExpression
    {
        public Cos(IExpression? child) : base(child)
        {
        }

        public override IExpression? Differentiate(string byVariable)
        {
            return new Multiply(new Negate(new Sin(Child)), Child?.Differentiate(byVariable));
        }

        public override IExpression? Simplify()
        {
            var newChild = Child?.Simplify();
            if (newChild is Constant childConst)
            {
                // child is constant
                return new Constant(Evaluate(childConst.Value));
            }
            return new Cos(newChild);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"cos({Child?.ToString(formatProvider)})";
        }

        protected override NumberImplementation Evaluate(NumberImplementation number)
        {
            //TODO: Proper system switcher
            return Math.Cos(number.Value);
        }
    }
}
