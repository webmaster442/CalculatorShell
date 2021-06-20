using CalculatorShell.Infrastructure;
using System;
using System.Threading.Tasks;

namespace CalculatorShell
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            PrintVersion();
            var memory = new Memory();
            CommandLoader loader = new CommandLoader(memory);
            using (var runner = new CommandRunner(loader.Commands))
            {
                await runner.Run();
            }
        }

        private static void PrintVersion()
        {
            var name = typeof(Program).Assembly.GetName();
            Console.WriteLine($"{name.Name} [{name.Version}]");
            Console.WriteLine();
        }
    }
}
