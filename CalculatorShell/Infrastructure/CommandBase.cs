using CalculatorShell.Base;
using System.ComponentModel.Composition;

namespace CalculatorShell.Infrastructure
{
    internal abstract class CommandBase : ICommand
    {
        [Import(typeof(IMemory))]
        public IMemory? Memory { get; set; }

        [Import(typeof(IHost))]
        public IHost? Host { get; set; }
    }
}
