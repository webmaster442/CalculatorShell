namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal sealed class Negate : UnaryExpression
    {
        public Negate(IExpression? child) : base(child)
        {
        }

        public override IExpression Differentiate(string byVariable)
        {
            return new Negate(Child?.Differentiate(byVariable));
        }

        public override IExpression Simplify()
        {
            IExpression? newChild = Child?.Simplify();
            if (newChild is Constant childConst)
            {
                // child is constant
                return new Constant(new NumberImplementation(-childConst.Value.Value));
            }
            return new Negate(newChild);
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"(-{Child?.ToString(formatProvider)})";
        }

        protected override NumberImplementation Evaluate(NumberImplementation number)
        {
            return new NumberImplementation(-number.Value);
        }
    }
}
