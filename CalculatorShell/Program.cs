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
                var fshost = new FsHost();
                var loader = new CommandLoader(memory, host);
                using var runner = new CommandRunner(loader.Commands, host, fshost, CultureInfo.InvariantCulture);
                await runner.RunShell();
            }
        }

        private static void PrintVersion()
        {
            var name = typeof(Program).Assembly.GetName();
            Console.WriteLine($"{name.Name} [{name.Version}] {Resources.InitText}\r\n");
        }

        private static string GetFileName()
        {
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return System.IO.Path.Combine(appdata, "calcshell.storage");
        }
    }
}
