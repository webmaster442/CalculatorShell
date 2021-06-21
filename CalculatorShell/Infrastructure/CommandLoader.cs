using CalculatorShell.Base;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;

namespace CalculatorShell.Infrastructure
{
    internal sealed class CommandLoader
    {
        [ImportMany(typeof(ICommand))]
        public IEnumerable<ICommand> Commands { get; private set; }

        [Export(typeof(IMemory))]
        public IMemory _memory;

        public CommandLoader(IMemory memory)
        {
            Commands = Enumerable.Empty<ICommand>();
            _memory = memory;
            using var catalog = new AggregateCatalog();
            using var ac = new AssemblyCatalog(typeof(CommandLoader).Assembly);
            catalog.Catalogs.Add(ac);
            using var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
            container.SatisfyImportsOnce(this);
        }
    }
}
