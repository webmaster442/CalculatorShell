using CalculatorShell.Base;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorShell.Infrastructure
{
    public class HostEnvironment : IHost
    {
        public HostEnvironment()
        {
            Commands = Enumerable.Empty<string>();
            Functions = Enumerable.Empty<string>();
        }

        public IEnumerable<string> Commands { get; set; }

        public IEnumerable<string> Functions { get; set; }
    }
}
