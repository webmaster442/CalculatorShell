using CalculatorShell.Base;
using CalculatorShell.Expressions;
using CalculatorShell.Infrastructure;
using System;
using System.ComponentModel.Composition;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class Eval : CommandBase, ISimpleCommand
    {
        protected INumber EvaluateExpression(Arguments arguments)
        {
            if (Memory == null)
                throw new InvalidOperationException();

            var expression = ExpressionFactory.Parse(arguments.Get<string>(0), Memory, arguments.CurrentCulture);
            return expression.Evaluate();
        }

        public virtual void Execute(Arguments arguments, ICommandConsole output)
        {
            if (Memory == null)
                throw new InvalidOperationException();

            var result = EvaluateExpression(arguments);
            output.WriteLine("{0}", result);

            Memory["ans"] = result;
        }
    }
}
