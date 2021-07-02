using CalculatorShell.Base;
using CalculatorShell.Infrastructure;
using System;
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
            arguments.CheckArgumentCount(1);

            long number = arguments.Get<long>(0);
            var numbers = new ConcurrentBag<long>();
            var options = new ParallelOptions
            {
                CancellationToken = cancellationToken,
            };

            Parallel.For(1L, Convert.ToInt64(Math.Sqrt(number)),  options, i =>
            {
                if (number % i == 0)
                {
                    if (number / i == 0)
                    {
                        numbers.Add(i);
                    }
                    else
                    {
                        numbers.Add(i);
                        numbers.Add(number / i);
                    }
                }
            });

            var str = string.Join(';', numbers.OrderBy(i => i));
            output.WriteLine(str);
        }
    }
}
