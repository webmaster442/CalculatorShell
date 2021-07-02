using CalculatorShell.Infrastructure;
using CalculatorShell.Properties;
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
            using (var storage = new Storage(GetFileName()))
            {
                var host = new HostEnvironment(storage);
                var loader = new CommandLoader(memory, host);
                using var runner = new CommandRunner(loader.Commands, host, CultureInfo.InvariantCulture);
                await runner.Run();
            }
        }

        private static void PrintVersion()
        {
            var name = typeof(Program).Assembly.GetName();
            Console.WriteLine($"{name.Name} [{name.Version}] {Resources.InitText}");
        }

        private static string GetFileName()
        {
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return System.IO.Path.Combine(appdata, "calcshell.storage");
        }
    }
}
