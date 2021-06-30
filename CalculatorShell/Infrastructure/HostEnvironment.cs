using CalculatorShell.Base;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorShell.Infrastructure
{
    public class HostEnvironment : IHost
    {
        public HostEnvironment(IStorage storage)
        {
            Commands = Enumerable.Empty<string>();
            Functions = Enumerable.Empty<string>();
            Storage = storage;
            CanRun = true;
        }

        public IEnumerable<string> Commands { get; set; }

        public IEnumerable<string> Functions { get; set; }

        public IStorage Storage { get; }

        public bool CanRun { get; private set; }

        public void Shutdown()
        {
            CanRun = false;
        }
    }
}
