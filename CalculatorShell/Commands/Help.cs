using CalculatorShell.Base;
using CalculatorShell.Infrastructure;
using CalculatorShell.Ui;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal sealed class Help : CommandBase, ISimpleCommand
    {
        private static HelpSystem help = new ();

        public override IEnumerable<string> Aliases
        {
            get
            {
                yield return "man";
            }
        }

        public void Execute(Arguments arguments, ICommandConsole output)
        {
            arguments.CheckArgumentCount(0, 1);
            string cmd = "help";
            if (arguments.Count == 1)
            {
                cmd = arguments.Get<string>(0);
            }

            help.WriteGetFormattetdHelp(cmd, output);
        }
    }
}
