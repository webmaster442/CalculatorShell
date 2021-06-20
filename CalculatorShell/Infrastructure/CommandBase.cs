using System.Threading;
using System.Threading.Tasks;

namespace CalculatorShell.Infrastructure
{
    internal abstract class CommandBase : ICommand
    {
        public abstract void Execute(string[] arguments, ICommandConsole output);

        public virtual bool ValidateArguments(string[] arguments)
        {
            return true;
        }
    }
}
