using CalculatorShell.Base;
using CalculatorShell.Infrastructure;
using CalculatorShell.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class Mem : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            if (Memory == null)
                throw new InvalidOperationException();

            arguments.CheckArgumentCount(0, 1);

            if (arguments.Count == 0)
            {
                Dictionary<string, string> vars = new();
                foreach (var name in Memory.VariableNames)
                {
                    vars.Add(name, Memory[name].ToString());
                }
                output.WriteLine(Resources.SetVariables);
                if (Memory.VariableNames.Any())
                    output.WriteTable<string, string>(vars);
            }
            else if (arguments.Count == 1)
            {
                var name = arguments.Get<string>(0);
                output.WriteLine("{0}", Memory[name]);
            }
        }
    }
}
