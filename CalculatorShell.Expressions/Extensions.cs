using CalculatorShell.Expressions.Internals;
using CalculatorShell.Expressions.Internals.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorShell.Expressions
{
    public static class Extensions
    {
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
                || expression is Constant
                || expression is Variable;
        }

    }
}
