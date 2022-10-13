using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorShell.Base
{
    /// <summary>
    /// Represents global constants in the application
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Home directrory shortcut in paths
        /// </summary>
        public const string HomeSymbol = "~";

        /// <summary>
        /// Path value representing the directory above the current one
        /// </summary>
        public const string UpOneDir = "..";

        /// <summary>
        /// Path seperator in Windows drives
        /// </summary>
        public const string WindowsDriveSeparator = ":";

        /// <summary>
        /// Directory separator. Now only supports Windows
        /// </summary>
        public const string DirectorySeparator = "\\";
    }
}
