using System.Collections.Generic;
using System.IO;

namespace CalculatorShell.Base
{
    /// <summary>
    /// Provides acces to File system stuff
    /// </summary>
    public interface IFsHost
    {
        /// <summary>
        /// Get or set current directory
        /// </summary>
        string CurrentDirectory { get; set; }

        /// <summary>
        /// Home directory
        /// </summary>
        string Home { get; }

        /// <summary>
        /// Get Current dirs subdirs
        /// </summary>
        /// <returns>Directories</returns>
        IEnumerable<string> GetDirectories();
        
        /// <summary>
        /// Get current dirs files
        /// </summary>
        /// <returns>Files</returns>
        IEnumerable<string> GetFiles();

        /// <summary>
        /// Returns true if a given file exists or not
        /// </summary>
        /// <param name="name">file name</param>
        /// <returns>true, if file exists</returns>
        bool FileExists(string name);

        /// <summary>
        /// Create a file for writing
        /// </summary>
        /// <param name="name">file name to create</param>
        /// <returns>Stream</returns>
        Stream CreateOrOverwrite(string name);

        /// <summary>
        /// Open a file for reading
        /// </summary>
        /// <param name="name">file name to read</param>
        /// <returns>Stream</returns>
        Stream OpenRead(string name);
    }
}
