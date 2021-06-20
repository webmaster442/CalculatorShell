using System.Threading;
using System.Threading.Tasks;

namespace CalculatorShell.Infrastructure
{
    internal interface ICommand
    {
        bool ValidateArguments(string[] arguments);
        void Execute(string[] arguments, ICommandConsole output);
    }
}
