using System.ComponentModel.Composition;

namespace CalculatorShell.Infrastructure
{
    internal abstract class CommandBase : ICommand
    {
        public abstract void Execute(string[] arguments, ICommandConsole output);

        [Import(typeof(IMemory))]
        public IMemory Memory { get; set; }

        public virtual bool ValidateArguments(string[] arguments)
        {
            return true;
        }
    }
}
