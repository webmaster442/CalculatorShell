using CalculatorShell.Base;
using CalculatorShell.Infrastructure;
using System;
using System.ComponentModel.Composition;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal sealed class CmdList : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            if (Host == null)
                throw new InvalidOperationException();

            var commands = string.Join(' ', Host.Commands);
            var functions = string.Join(' ', Host.Functions);

            Console.WriteLine("Known commands: {0}", commands);
            Console.WriteLine("Known functions: {0}", functions);

        }
    }
}
