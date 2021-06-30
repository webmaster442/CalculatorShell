using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorShell.Base
{
    /// <summary>
    /// Represents an entry in the storage system
    /// </summary>
    public interface IStorageEntry
    {
        /// <summary>
        /// Last write time
        /// </summary>
        DateTimeOffset LastWrite { get; }
        /// <summary>
        /// Name of entry
        /// </summary>
        string Name { get; }
    }
}
