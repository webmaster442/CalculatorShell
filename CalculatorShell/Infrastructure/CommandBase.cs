using System.Threading;
using System.Threading.Tasks;

namespace CalculatorShell.Infrastructure
{
    internal abstract class CommandBase : ICommand
    {
        public abstract void Execute(string[] arguments, IConsole output);

        public virtual bool ValidateArguments(string[] arguments)
        {
            return true;
        }
    }
}
