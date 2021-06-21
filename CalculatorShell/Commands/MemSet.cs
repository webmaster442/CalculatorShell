using CalculatorShell.Base;
using CalculatorShell.Expressions;
using CalculatorShell.Infrastructure;
using System;
using System.ComponentModel.Composition;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class MemSet : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            if (Memory == null)
                throw new InvalidOperationException();

            var name = arguments.Get<string>(0);
            Memory[name] = ExpressionFactory.Parse(arguments.Get<string>(1), Memory, arguments.CurrentCulture).Evaluate();
        }
    }
}
