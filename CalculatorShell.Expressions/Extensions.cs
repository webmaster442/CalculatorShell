using CalculatorShell.Expressions.Internals;
using CalculatorShell.Expressions.Internals.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorShell.Expressions
{
    /// <summary>
    /// Various extension methodts for expressions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Flatten the expression tree into an enumerable collection
        /// </summary>
        /// <param name="expression">Expression to flaten</param>
        /// <returns>enumerable collection</returns>
        public static IEnumerable<IExpression> Flatten(this IExpression expression)
        {
            Stack<IExpression> expressions = new Stack<IExpression>();

            expressions.Push(expression);

            while (expressions.Count > 0)
            {
                var n = expressions.Pop();

                if (n != null)
                {
                    yield return n;
                }

                if (n is BinaryExpression bin)
                {
                    if (bin.Left != null)
                        expressions.Push(bin.Left);
                    if (bin.Right != null)
                        expressions.Push(bin.Right);
                }
                else if (n is UnaryExpression un 
                    && un.Child != null)
                {
                    expressions.Push(un.Child);
                }
            }
        }

        /// <summary>
        /// Returns true if the given expression is a logical expression.
        /// </summary>
        /// <param name="expression">Expression to check</param>
        /// <returns>true if the given expression is a logical expression.</returns>
        public static bool IsLogicExpression(this IExpression expression)
        {
            return expression
                    .Flatten()
                    .All(n => IsLogicExpressionNode(n));
        }

        private static bool IsLogicExpressionNode(IExpression expression)
        {
            return expression is And
                || expression is Or
                || expression is Not
                || (expression is Constant constant && constant.Value.NumberType == NumberType.Boolean)
                || expression is Variable;
        }

        /// <summary>
        /// Create a list of minterms from the given expression
        /// </summary>
        /// <param name="expression">Expression to check</param>
        /// <returns>Minterms of the expression</returns>
        public static IEnumerable<int> GetLogicTable(this IExpression expression)
        {
            if (!expression.IsLogicExpression())
                throw new ExpressionEngineException("Only logical expressions");

            if (expression.Variables == null)
                throw new ExpressionEngineException();

            var varialbes = expression.Flatten().OfType<Variable>().ToArray();
            checked
            {
                int combinations = 1 << varialbes.Length;
                for (int i=0; i<combinations; i++)
                {
                    bool[] values = GetValues(i, varialbes.Length);
                    for (int j=0; j<values.Length; j++)
                    {
                        expression.Variables[varialbes[j].Name] = new NumberImplementation(values[j]);
                    }
                    var result = expression.Evaluate().GetBooean();
                    if (result)
                    {
                        yield return i;
                    }
                }
            }
        }

        private static bool[] GetValues(int currentIndex, int length)
        {
            bool[] result = new bool[length];
            var str = Convert.ToString(currentIndex, 2).PadLeft(length, '0');
            for (int j=0; j<str.Length; j++)
            {
                if (str[j] == '1')
                    result[j] = true;
                else
                    result[j] = false;
            }
            return result;
        }
    }
}
