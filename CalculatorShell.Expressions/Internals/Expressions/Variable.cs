using CalculatorShell.Expressions.Properties;
using System.Globalization;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal sealed class Variable : IExpression
    {
        public IVariables? Variables { get; }

        public string Name { get; }
        public string PropertyName { get; }

        private readonly string _identifier;

        public Variable(string identifier, IVariables variables)
        {
            _identifier = identifier;
            Variables = variables;
            if (identifier.Contains('[') &&
                identifier.Contains(']'))
            {
                string[]? parts = identifier.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2)
                    throw new ExpressionEngineException(Resources.InvalidToken);

                Name = parts[0];
                PropertyName = parts[1];

            }
            else
            {
                Name = identifier.ToLower();
                PropertyName = string.Empty;
            }
        }

        public IExpression Differentiate(string byVariable)
        {
            if (byVariable == _identifier)
            {
                // f(x) = x
                // d( f(x) ) = 1 * d( x )
                // d( x ) = 1
                // f'(x) = 1
                return new Constant(new NumberImplementation(1.0d));
            }
            // f(x) = c
            // d( f(x) ) = c * d( c )
            // d( c ) = 0
            // f'(x) = 0
            return new Constant(new NumberImplementation(0.0d));
        }

        public INumber Evaluate()
        {
            if (Variables == null)
                throw new ExpressionEngineException(Resources.NoVariableValues);

            if (!string.IsNullOrEmpty(PropertyName))
            {
                return Variables[Name, PropertyName];
            }

            return Variables[Name];
        }

        public IExpression Simplify()
        {
            if (Variables == null)
                throw new ExpressionEngineException(Resources.NoVariableValues);

            if (Variables.IsConstant(Name)
                && string.IsNullOrEmpty(PropertyName))
            {
                return new Constant((NumberImplementation)Variables[Name]);
            }
            return new Variable(_identifier, Variables);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return Name.ToString(formatProvider);
        }

        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }
    }
}
