using CalculatorShell.Infrastructure;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace CalculatorShell
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            PrintVersion();
            var memory = new Memory();
            var loader = new CommandLoader(memory);
            using var runner = new CommandRunner(loader.Commands, CultureInfo.InvariantCulture);
            await runner.Run();
        }

        private static void PrintVersion()
        {
            var name = typeof(Program).Assembly.GetName();
            Console.WriteLine($"{name.Name} [{name.Version}]");
            Console.WriteLine();
        }
    }
}
