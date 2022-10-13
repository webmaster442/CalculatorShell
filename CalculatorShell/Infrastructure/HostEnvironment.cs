namespace CalculatorShell.Infrastructure
{
    public class HostEnvironment : IHostEx
    {
        public HostEnvironment()
        {
            Commands = Enumerable.Empty<string>();
            Functions = Enumerable.Empty<string>();
            CanRun = true;
        }

        public IEnumerable<string> Commands { get; private set; }

        public IEnumerable<string> Functions { get; private set; }

        public bool CanRun { get; private set; }

        public Version HostVersion
        {
            get
            {
                System.Reflection.AssemblyName? name = typeof(Program).Assembly.GetName();
                return name?.Version ?? new Version(99, 99, 99);
            }
        }

        public void SetCommands(IEnumerable<string> commands)
        {
            Commands = commands;
        }

        public void SetFunctions(IEnumerable<string> functions)
        {
            Functions = functions;
        }

        public void Shutdown()
        {
            CanRun = false;
        }
    }
}
