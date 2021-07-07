using CalculatorShell.Base;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace CalculatorShell.Infrastructure
{
    internal abstract class CommandBase : ICommand
    {
        [Import(typeof(IMemory))]
        public IMemory? Memory { get; set; }

        [Import(typeof(IHost))]
        public IHost? Host { get; set; }

        public virtual IEnumerable<string> Aliases => Enumerable.Empty<string>();
    }
}
