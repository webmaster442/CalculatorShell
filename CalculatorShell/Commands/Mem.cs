using CalculatorShell.Base;
using CalculatorShell.Infrastructure;
using CalculatorShell.Properties;
using System;
using System.ComponentModel.Composition;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class Mem : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            if (Memory == null)
                throw new InvalidOperationException();

            if (arguments.Count == 0)
            {
                var list = string.Join('\n', Memory.VariableNames);
                output.WriteLine(Resources.SetVariables);
                if (!string.IsNullOrEmpty(list))
                    output.WriteLine(list);
            }
            else if (arguments.Count == 1)
            {
                var name = arguments.Get<string>(0);
                output.WriteLine("{0}", Memory[name]);
            }
        }
    }
}
