using CalculatorShell.Base;
using System;

namespace CalculatorShell.Infrastructure
{
    internal sealed record StorageEntry : IStorageEntry
    {
        public StorageEntry()
        {
            Name = string.Empty;
        }


        public DateTimeOffset LastWrite { get; init; }

        public string Name { get; init; }
    }
}
