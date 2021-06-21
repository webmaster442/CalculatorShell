using CalculatorShell.Expressions.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorShell.Expressions.Internals.Expressions
{
    internal sealed class Variable : IExpression
    {
        public IVariables? Variables { get; }

        public string Identifier { get; }

        public Variable(string identifier, IVariables variables)
        {
            Variables = variables;
            Identifier = identifier.ToLower();
        }

        public IExpression Differentiate(string byVariable)
        {
            if (byVariable == Identifier)
            {
                // f(x) = x
                // d( f(x) ) = 1 * d( x )
                // d( x ) = 1
                // f'(x) = 1
                return new Constant(new NumberImplementation(1));
            }
            // f(x) = c
            // d( f(x) ) = c * d( c )
            // d( c ) = 0
            // f'(x) = 0
            return new Constant(new NumberImplementation(0));
        }

        public INumber Evaluate()
        {
            if (Variables == null)
                throw new ExpressionEngineException(Resources.NoVariableValues);

            return Variables[Identifier];
        }

        public IExpression Simplify()
        {
            if (Variables == null)
                throw new ExpressionEngineException(Resources.NoVariableValues);

            if (Variables.IsConstant(Identifier))
            {
                return new Constant(Variables[Identifier]);
            }
            return new Variable(Identifier, Variables);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return Identifier.ToString(formatProvider);
        }

        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }
    }
}
