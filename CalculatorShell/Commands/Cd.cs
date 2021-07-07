using CalculatorShell.Base;
using CalculatorShell.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class Cd : CommandBase, IFsTaskCommand
    {
        public override IEnumerable<string> Aliases
        {
            get { yield return "cwd"; }
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task Execute(Arguments arguments, ICommandConsole output, IFsHost fs, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            arguments.CheckArgumentCount(1);
            var target = arguments.Get<string>(0);
            if (target == "~"
                && Directory.Exists(fs.Home))
            {
                fs.CurrentDirectory = fs.Home;
            }
            else if (target == ".." && TryGetParent(fs.CurrentDirectory, out string parent))
            {
                fs.CurrentDirectory = parent;
            }
            else if (target.Contains(":")
                && Directory.Exists(target))
            {
                if (target.EndsWith("\\"))
                    fs.CurrentDirectory = target;
                else
                    fs.CurrentDirectory = target + "\\";
            }
            else if (target.Contains("\\")
                && TryGetSubir(fs.CurrentDirectory, target, out string subdir))
            {
                fs.CurrentDirectory = subdir;
            }
            else
                throw new IOException("Can't change folder");

        }

        private bool TryGetSubir(string currentDirectory, string target, out string subdir)
        {
            subdir = Path.Combine(currentDirectory, target);
            if (!subdir.EndsWith("\\")) 
                subdir += "\\";
            return Directory.Exists(subdir);
        }

        private bool TryGetParent(string currentDirectory, out string parent)
        {
            parent = Directory.GetParent(currentDirectory)?.FullName ?? string.Empty;
            return Directory.Exists(parent);
        }
    }
}
