using CalculatorShell.Infrastructure;

namespace CalculatorShell
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CommandLoader loader = new CommandLoader();
            var runner = new CommandRunner(loader.Commands);
            runner.Run();
        }
    }
}
