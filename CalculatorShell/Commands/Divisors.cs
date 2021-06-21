using CalculatorShell.Base;
using CalculatorShell.Infrastructure;
using System.Collections.Concurrent;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CalculatorShell.Commands
{
    [Export(typeof(ICommand))]
    internal sealed class Divisors : CommandBase, ITaskCommand
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task Execute(Arguments arguments, ICommandConsole output, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            long number = arguments.Get<long>(0);
            var numbers = new ConcurrentBag<long>();
            var options = new ParallelOptions
            {
                CancellationToken = cancellationToken,
            };

            Parallel.For(1, number / 2,  options, i =>
            {
                if (number % i == 0)
                    numbers.Add(i);
            });

            var str = string.Join(';', numbers.OrderBy(i => i));
            output.WriteLine(str);
        }
    }
}
