using CalculatorShell.Base;
using CalculatorShell.Infrastructure;
using CalculatorShell.Ui;
using System.ComponentModel.Composition;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal sealed class Help : CommandBase, ISimpleCommand
    {
        private readonly TextViewer _viewwer;

        public Help()
        {
            _viewwer = new TextViewer();
            _viewwer.LoadText("CalculatorShell.Documents.Documentation.md");
            _viewwer.LoadText("CalculatorShell.Documents.CmdHelp.md");
            _viewwer.LoadText("CalculatorShell.Documents.FunctionHelp.md");
        }

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
            _viewwer.Show(output, cmd);
        }
    }
}
