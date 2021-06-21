using CalculatorShell.Base;
using CalculatorShell.Expressions;
using CalculatorShell.Infrastructure;
using System;
using System.ComponentModel.Composition;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal sealed class Exit : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            Environment.Exit(0);
        }
    }
}
