using CalculatorShell.Base;
using CalculatorShell.Infrastructure;
using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal sealed class CmdList : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            if (Host == null)
                throw new InvalidOperationException();

            output.WriteLine("Known commands:");
            output.WriteTable(Host.Commands.OrderBy(x => x), 6);
            output.WriteLine("Known functions:");
            output.WriteTable(Host.Functions.OrderBy(x => x), 6);
        }
    }
}
