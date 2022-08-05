using CalculatorShell.Base;
using CalculatorShell.Infrastructure;
using System.ComponentModel.Composition;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class MemLoad : MemorySerializeCommandBase, IFsTaskCommand
    {
        public async Task Execute(Arguments arguments, ICommandConsole output, IFsHost fs, CancellationToken cancellationToken)
        {
            arguments.CheckArgumentCount(1);
            string fileName = arguments.Get<string>(0);

            using (Stream? fss = fs.OpenRead(fileName))
            {
                await Deserialize(fss, cancellationToken);
            }
        }
    }
}
