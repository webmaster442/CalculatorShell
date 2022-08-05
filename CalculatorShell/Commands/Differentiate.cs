using CalculatorShell.Base;
using CalculatorShell.Expressions;
using CalculatorShell.Infrastructure;
using System.ComponentModel.Composition;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class Differentiate : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            if (Memory == null)
                throw new InvalidOperationException();

            arguments.CheckArgumentCount(2);

            string expression = arguments.Get<string>(0);
            string byVariable = arguments.Get<string>(1);

            if (expression.StartsWith('$'))
            {
                IExpression? exp = Memory.GetExpression(expression);
                Memory.SetExpression("$ans", exp.Differentiate(byVariable));
            }
            else
            {
                IExpression? exp = ExpressionFactory.Parse(expression, Memory, arguments.CurrentCulture);
                Memory.SetExpression("$ans", exp.Differentiate(byVariable));
            }
        }
    }
}
