using CalculatorShell.Base;
using CalculatorShell.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class MemSave : MemorySerializeCommandBase, IFsTaskCommand
    {
        public async Task Execute(Arguments arguments, ICommandConsole output, IFsHost fs, CancellationToken cancellationToken)
        {
            arguments.CheckArgumentCount(1);
            string fileName = arguments.Get<string>(0);

            using (var fss = fs.CreateOrOverwrite(fileName))
            {
                await Serialize(fss, cancellationToken);
            }
        }
    }
}