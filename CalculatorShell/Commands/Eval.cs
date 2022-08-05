using CalculatorShell.Base;
using CalculatorShell.Expressions;
using CalculatorShell.Infrastructure;
using System.ComponentModel.Composition;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class Eval : CommandBase, ISimpleCommand
    {
        protected INumber EvaluateExpression(Arguments arguments, out IExpression expression)
        {
            expression = ParseExpression(arguments, 0);
            return expression.Evaluate();
        }

        public virtual void Execute(Arguments arguments, ICommandConsole output)
        {
            if (Memory == null)
                throw new InvalidOperationException();

            arguments.CheckArgumentCount(1);

            INumber? result = EvaluateExpression(arguments, out IExpression parsed);

            output.WriteLine("{0}", result);
            Memory.SetExpression("$ans", parsed);
            Memory["ans"] = result;
        }
    }
}
