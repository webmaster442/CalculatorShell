using CalculatorShell.Expressions.Properties;
using System;
using System.Globalization;
using System.Linq;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal abstract class TypeFunction<T> : IExpression where T : class
    {
        public TypeFunction(params IExpression[] expressions)
        {
            Parameters = expressions;
        }

        public IExpression[] Parameters { get; }

        public IVariables? Variables => Parameters.FirstOrDefault(x => x.Variables != null)?.Variables;

        public IExpression Differentiate(string byVariable)
        {
            throw new ExpressionEngineException(Resources.CanotDifferentiate);
        }

        public INumber Evaluate()
        {
            var evaluated = Parameters.Select(x => x.Evaluate() as NumberImplementation);
            NumberImplementation?[] notnull = evaluated?.Where(x => x != null)?.ToArray() ?? Array.Empty<NumberImplementation>();

            return Evaluate(notnull!);
        }

        protected abstract NumberImplementation Evaluate(params NumberImplementation[] numbers);

        protected abstract TypeFunction<T> CreateExpression(IExpression[] parameters);

        public IExpression Simplify()
        {
            if (Parameters.All(x => x is Constant))
            {
                var values = Parameters.OfType<Constant>().Select(x => x.Value);
                return new Constant(Evaluate(values.ToArray()));
            }
            else
            {
                return CreateExpression(Parameters);
            }
        }


        public abstract string ToString(IFormatProvider formatProvider);

        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }
    }
}
