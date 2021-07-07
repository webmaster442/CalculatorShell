using CalculatorShell.Base;
using CalculatorShell.Infrastructure;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class Ls : CommandBase, IFsTaskCommand
    {
        public override IEnumerable<string> Aliases
        {
            get
            {
                yield return "dir";
            }
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task Execute(Arguments arguments, ICommandConsole output, IFsHost fs, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var dirs = fs.GetDirectories().ToDictionary(x => Path.GetFileName(x), _ => "[ directory ]");
            output.WriteTable<string, string>(dirs);
            var files = fs.GetFiles().ToDictionary(x => Path.GetFileName(x), x => Path.GetExtension(x));
            output.WriteTable<string, string>(files);
        }
    }
}
