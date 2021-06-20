using CalculatorShell.Infrastructure;

namespace CalculatorShell.Commands
{
    internal sealed class Clear : CommandBase
    {
        public override void Execute(string[] arguments, IConsole output)
        {
            output.Clear();
        }
    }
}
