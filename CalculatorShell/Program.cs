using CalculatorShell.Infrastructure;
using System;

namespace CalculatorShell
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            PrintVersion();
            var memory = new Memory();
            CommandLoader loader = new CommandLoader(memory);
            var runner = new CommandRunner(loader.Commands);
            runner.Run();
        }

        private static void PrintVersion()
        {
            var name = typeof(Program).Assembly.GetName();
            Console.WriteLine($"{name.Name} [{name.Version}]");
            Console.WriteLine();
        }
    }
}
