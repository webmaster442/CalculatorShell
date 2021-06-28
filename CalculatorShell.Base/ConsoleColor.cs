using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorShell.Base
{
    public sealed record ConsoleColor
    {
        public byte R { get; init; }
        public byte G { get; init; }
        public byte B { get; init; }
    }
}
