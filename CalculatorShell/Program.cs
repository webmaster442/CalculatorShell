using CalculatorShell.Properties;
using System.Globalization;

namespace CalculatorShell
{
    //TODO: Add command for debugger wait
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            PrintVersion();
            Memory? memory = new();
            HostEnvironment? host = new();
            FsHost? fshost = new();
            CommandLoader? loader = new(memory, host);
            using CommandRunner? runner = new(loader.Commands, host, fshost, CultureInfo.InvariantCulture);
            await runner.RunShell();
        }

        private static void PrintVersion()
        {
            System.Reflection.AssemblyName? name = typeof(Program).Assembly.GetName();
            Console.WriteLine($"{name.Name} [{name.Version}] {Resources.InitText}\r\n");
        }
    }
}
