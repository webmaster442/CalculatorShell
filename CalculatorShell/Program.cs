using CalculatorShell.Infrastructure;
using CalculatorShell.Properties;
using System.Globalization;

namespace CalculatorShell
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            PrintVersion();
            Memory? memory = new Memory();
            HostEnvironment? host = new HostEnvironment();
            FsHost? fshost = new FsHost();
            CommandLoader? loader = new CommandLoader(memory, host);
            using CommandRunner? runner = new CommandRunner(loader.Commands, host, fshost, CultureInfo.InvariantCulture);
            await runner.RunShell();
        }

        private static void PrintVersion()
        {
            System.Reflection.AssemblyName? name = typeof(Program).Assembly.GetName();
            Console.WriteLine($"{name.Name} [{name.Version}] {Resources.InitText}\r\n");
        }
    }
}
