using CalculatorShell.Maths;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal sealed class Sin : UnaryExpression
    {
        public Sin(IExpression? child) : base(child)
        {
        }

        public override IExpression Differentiate(string byVariable)
        {
            return new Multiply(new Cos(Child), Child?.Differentiate(byVariable));
        }

        public override IExpression Simplify()
        {
            IExpression? newChild = Child?.Simplify();
            if (newChild is Constant childConst)
            {
                // child is constant
                return new Constant(Evaluate(childConst.Value));
            }
            return new Sin(newChild);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"sin({Child?.ToString(formatProvider)})";
        }

        protected override NumberImplementation Evaluate(NumberImplementation number)
        {
            return new NumberImplementation(Trigonometry.Sin(number.Value));
        }
    }
}
