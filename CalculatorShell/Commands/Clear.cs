using CalculatorShell.Base;
using CalculatorShell.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal sealed class Clear : CommandBase, ISimpleCommand
    {
        public override IEnumerable<string> Aliases
        {
            get
            {
                yield return "cls";
            }
        }

        public void Execute(Arguments arguments, ICommandConsole output)
        {
            arguments.CheckArgumentCount(0);

            output.Clear();
        }
    }
}
