using System.ComponentModel.Composition.Hosting;

namespace CalculatorShell.Infrastructure
{
    internal sealed class CommandLoader
    {
        [ImportMany(typeof(ICommand))]
        public IEnumerable<ICommand> Commands { get; private set; }

        [Export(typeof(IMemory))]
        public IMemory _memory;

        [Export(typeof(IHost))]
        public IHost _host;

        public CommandLoader(IMemory memory, IHost host)
        {
            Commands = Enumerable.Empty<ICommand>();
            _memory = memory;
            _host = host;
            using AggregateCatalog? catalog = new AggregateCatalog();
            using AssemblyCatalog? ac = new AssemblyCatalog(typeof(CommandLoader).Assembly);
            catalog.Catalogs.Add(ac);
            using CompositionContainer? container = new CompositionContainer(catalog);
            container.ComposeParts(this);
            container.SatisfyImportsOnce(this);

        }
    }
}
