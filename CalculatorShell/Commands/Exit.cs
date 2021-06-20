using CalculatorShell.Infrastructure;
using System;
using System.ComponentModel.Composition;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class Exit : CommandBase
    {
        public override void Execute(string[] arguments, ICommandConsole output)
        {
            Environment.Exit(0);
        }
    }
}
