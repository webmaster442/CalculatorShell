using CalculatorShell.Base;
using CalculatorShell.Infrastructure;
using CalculatorShell.Properties;
using System.ComponentModel.Composition;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal sealed class Help : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            if (!arguments.TryGet(0, out string? cmd))
            {
                cmd = nameof(Help);
            }
            if (!string.IsNullOrEmpty(cmd))
            {
                var result = HelpTexts.ResourceManager.GetString(cmd);
                if (!string.IsNullOrEmpty(result))
                {
                    output.WriteLine(result);
                }
            }
            else
            {
                output.Error(Resources.NoHelpFound, cmd ?? string.Empty);
            }
        }
    }
}
