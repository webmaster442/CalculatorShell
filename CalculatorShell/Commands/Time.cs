using CalculatorShell.Base;
using CalculatorShell.Infrastructure;
using System;
using System.ComponentModel.Composition;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class Time : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            output.WriteLine("{0}:{1}", DateTime.Now.Hour, DateTime.Now.Minute);
        }
    }
}
