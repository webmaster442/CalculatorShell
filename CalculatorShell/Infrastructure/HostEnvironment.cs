using CalculatorShell.Base;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorShell.Infrastructure
{
    public class HostEnvironment : IHostEx
    {
        public HostEnvironment(IStorage storage)
        {
            Commands = Enumerable.Empty<string>();
            Functions = Enumerable.Empty<string>();
            Storage = storage;
            CanRun = true;
        }

        public IEnumerable<string> Commands { get; private set; }

        public IEnumerable<string> Functions { get; private set; }

        public IStorage Storage { get; }

        public bool CanRun { get; private set; }

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
