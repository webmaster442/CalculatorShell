using CalculatorShell.Base;
using CalculatorShell.Infrastructure;
using System.ComponentModel.Composition;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))] //TODO: Make it support Unix
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
            string? target = arguments.Get<string>(0);
            if (target == Constants.HomeSymbol
                && Directory.Exists(fs.Home))
            {
                fs.CurrentDirectory = fs.Home;
            }
            else if (target == Constants.UpOneDir && TryGetParent(fs.CurrentDirectory, out string parent))
            {
                fs.CurrentDirectory = parent;
            }
            else if (target.Contains(Constants.WindowsDriveSeparator)
                && Directory.Exists(target))
            {
                if (target.EndsWith(Constants.DirectorySeparator))
                    fs.CurrentDirectory = target;
                else
                    fs.CurrentDirectory = target + Constants.DirectorySeparator;
            }
            else if (target.Contains(Constants.DirectorySeparator)
                && TryGetSubir(fs.CurrentDirectory, target, out string subdir))
            {
                fs.CurrentDirectory = subdir;
            }
            else
                throw new IOException("Can't change folder");
        }

        private static bool TryGetSubir(string currentDirectory, string target, out string subdir)
        {
            subdir = Path.Combine(currentDirectory, target);
            if (!subdir.EndsWith(Constants.DirectorySeparator))
                subdir += Constants.DirectorySeparator;
            return Directory.Exists(subdir);
        }

        private static bool TryGetParent(string currentDirectory, out string parent)
        {
            parent = Directory.GetParent(currentDirectory)?.FullName ?? string.Empty;
            return Directory.Exists(parent);
        }
    }
}
