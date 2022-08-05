namespace CalculatorShell.Base
{
    /// <summary>
    /// Provides API to the console
    /// </summary>
    public interface ICommandConsole : IProgress<float>, IProgress<TimeSpan>
    {
        /// <summary>
        /// Display an exception as an error message.
        /// </summary>
        /// <param name="ex">Exception to display</param>
        void Error(Exception ex);

        /// <summary>
        /// Display an error message.
        /// </summary>
        /// <param name="format">A composite format string</param>
        /// <param name="args">An array of objects to write using format</param>
        /// <exception cref="System.ArgumentNullException">format or arg is null</exception>
        /// <exception cref="System.FormatException">The format specification in format is invalid</exception>
        void Error(string format, params object[] args);
        /// <summary>
        /// Write a string
        /// </summary>
        /// <param name="format">A composite format string</param>
        /// <param name="args">An array of objects to write using format</param>
        /// <exception cref="System.ArgumentNullException">format or arg is null</exception>
        /// <exception cref="System.FormatException">The format specification in format is invalid</exception>
        void Write(string format, params object[] args);
        /// <summary>
        /// Write a line
        /// </summary>
        /// <param name="format">A composite format string</param>
        /// <param name="args">An array of objects to write using format</param>
        /// <exception cref="System.ArgumentNullException">format or arg is null</exception>
        /// <exception cref="System.FormatException">The format specification in format is invalid</exception>
        void WriteLine(string format, params object[] args);
        /// <summary>
        /// Write multiple lines to the console
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="items">collection of items</param>
        /// <param name="itemFormatter">Single item formatter function, that convert the item to a line of string</param>
        void WriteLines<T>(IEnumerable<T> items, Func<T, string> itemFormatter);
        /// <summary>
        /// Write an objects properties and values as a table to the console
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="item">object instance</param>
        void WriteTable<T>(T item);
        /// <summary>
        /// Write a dictionary's contents as a talbe to the console
        /// </summary>
        /// <typeparam name="Tkey">Dictionary key type</typeparam>
        /// <typeparam name="TValue">Dictionary value type</typeparam>
        /// <param name="dictionary">Dictionary to write</param>
        void WriteTable<Tkey, TValue>(IDictionary<Tkey, TValue> dictionary);
        /// <summary>
        /// Wite a Collection of items as a table to the console
        /// </summary>
        /// <typeparam name="T">Type of objects</typeparam>
        /// <param name="items">collection of items</param>
        /// <param name="columns">number of columns to use</param>
        void WriteTable<T>(IEnumerable<T> items, int columns = 4);
        /// <summary>
        /// Clear the console
        /// </summary>
        void Clear();
        /// <summary>
        /// Get or set the current format. If null, then all formating is cleared.
        /// </summary>
        ConsoleFormat? CurrentFormat { get; set; }

        /// <summary>
        /// Read a key
        /// </summary>
        /// <returns>Read key</returns>
        ConsoleKey ReadKey();
    }
}