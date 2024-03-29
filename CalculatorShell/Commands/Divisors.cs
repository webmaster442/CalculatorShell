﻿using System.Collections.Concurrent;

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
            ConcurrentBag<long>? numbers = new ConcurrentBag<long>();
            ParallelOptions? options = new ParallelOptions
            {
                CancellationToken = cancellationToken,
            };

            Parallel.For(1L, Convert.ToInt64(Math.Sqrt(number)), options, i =>
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

            string? str = string.Join(';', numbers.OrderBy(i => i));
            output.WriteLine(str);
        }
    }
}
