using System.Collections.Generic;

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
    }
}
