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
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            if (Memory == null)
                throw new InvalidOperationException();

            var expression = ExpressionFactory.Parse(arguments.Get<string>(0), Memory, arguments.CurrentCulture);
            if (expression != null)
            {
                var result = expression.Evaluate();
                output.WriteLine("{0}", result);

                Memory["Ans"] = result;
            }
        }
    }
}
