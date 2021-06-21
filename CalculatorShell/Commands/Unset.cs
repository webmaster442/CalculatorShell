using CalculatorShell.Base;
using CalculatorShell.Expressions;
using CalculatorShell.Infrastructure;
using System;
using System.ComponentModel.Composition;

namespace CalculatorShell.Commands
{

    [Export(typeof(ICommand))]
    internal class Unset : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            if (Memory == null)
                throw new InvalidOperationException();

            if (arguments.Count == 1)
            {
                var name = arguments.Get<string>(0);
                Memory.Delete(name);
            }
            else
            {
                Memory.Clear();
            }
        }
    }
}
