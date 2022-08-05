using CalculatorShell.Maths;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal class Vect3 : TypeFunction<Vector3>
    {
        public Vect3(params IExpression[] expressions) : base(expressions)
        {
        }

        public override string ToString(IFormatProvider formatProvider)
        {
            return $"Vect2({Parameters[0]?.ToString(formatProvider)};{Parameters[1]?.ToString(formatProvider)};{Parameters[2]?.ToString(formatProvider)})";
        }

        protected override TypeFunction<Vector3> CreateExpression(IExpression[] parameters)
        {
            return new Vect3(parameters);
        }

        protected override NumberImplementation Evaluate(params NumberImplementation[] numbers)
        {
            return new NumberImplementation(new Vector3(numbers[0].Value, numbers[1].Value, numbers[2].Value));
        }
    }
}
