using System.Threading;
using System.Threading.Tasks;

namespace CalculatorShell.Base
{
    public interface ITaskCommand : ICommand
    {
        Task Execute(Arguments arguments, ICommandConsole output, CancellationToken cancellationToken);
    }
}
