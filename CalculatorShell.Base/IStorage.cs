using System;
using System.Collections.Generic;

namespace CalculatorShell.Base
{
    /// <summary>
    /// Provides methods to store and read data between sessions
    /// </summary>
    public interface IStorage : IEnumerable<IStorageEntry>, IDisposable
    {
        /// <summary>
        /// Check if an entry exists in the storage system
        /// </summary>
        /// <param name="name">Entry name</param>
        /// <returns>true, if it exists</returns>
        bool Exists(string name);
        /// <summary>
        /// Delete an entry by name if it exists.
        /// </summary>
        /// <param name="name">name of entry</param>
        void Delete(string name);
        /// <summary>
        /// Deserialize a storage entry content to an object
        /// </summary>
        /// <typeparam name="T">Type of deserialized data</typeparam>
        /// <param name="name">storage entry to use</param>
        /// <returns>Deserialized entry content</returns>
        T? Deserialize<T>(string name) where T : class, new();
        /// <summary>
        /// Serialize an object to a storage entry
        /// </summary>
        /// <typeparam name="T">Type of data</typeparam>
        /// <param name="name">storage entry to use</param>
        /// <param name="entryData">object that will be placed into the storage</param>
        void CreateOrUpdateEntry<T>(string name, T entryData) where T : class, new();
    }
}
