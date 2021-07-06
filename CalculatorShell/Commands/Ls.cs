using CalculatorShell.Base;
using CalculatorShell.Infrastructure;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class Ls : CommandBase, IFsTaskCommand
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task Execute(Arguments arguments, ICommandConsole output, IFsHost fs, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            output.WriteLines(fs.GetDirectories());
            output.WriteLines(fs.GetFiles());
        }
    }
}
