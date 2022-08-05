using CalculatorShell.Maths;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal class Vect2 : TypeFunction<Vector2>
    {
        public Vect2(params IExpression[] expressions) : base(expressions)
        {
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"Vect2({Parameters[0]?.ToString(formatProvider)};{Parameters[1]?.ToString(formatProvider)})";
        }

        protected override TypeFunction<Vector2> CreateExpression(IExpression[] parameters)
        {
            return new Vect2(parameters);
        }

        protected override NumberImplementation Evaluate(params NumberImplementation[] numbers)
        {
            return new NumberImplementation(new Vector2(numbers[0].Value, numbers[1].Value));
        }
    }
}
