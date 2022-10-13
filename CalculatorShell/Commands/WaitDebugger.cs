using System.Diagnostics;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal class WaitDebugger : CommandBase, ISimpleCommand
    {
        public void Execute(Arguments arguments, ICommandConsole output)
        {
            output.WriteLine("Waiting for debugger to be attached...");
            output.WriteLine("Press Escape to cancel, or any key to continue, If debugger is attached.");
            while (!Debugger.IsAttached)
            {
                if (output.ReadKey(false) == ConsoleKey.Escape)
                {
                    break;
                }
                //Blocks execution. Intentionally left empty
            }

            if (Debugger.IsAttached)
            {
                //Happy debugging 😀
                Debugger.Break();
            }

        }
    }
}
