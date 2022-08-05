using System.Globalization;

namespace CalculatorShell.Base
{
    /// <summary>
    /// Provides acces to command arguments
    /// </summary>
    public sealed class Arguments
    {
        private readonly string[] _arguments;

        /// <summary>
        /// Current culture that was and will be used to parse arguments.
        /// </summary>
        public CultureInfo CurrentCulture { get; }


        /// <summary>
        /// Throw an exception if argument count is below or over limit.
        /// </summary>
        /// <param name="count">Number of arguments to expect</param>
        public void CheckArgumentCount(int count)
        {
            if (_arguments.Length < count || _arguments.Length > count)
                throw new InvalidOperationException($"Command expected {count} arguments. {_arguments.Length} was given");
        }

        /// <summary>
        /// Throw an exception if argument count is not in the given range
        /// </summary>
        /// <param name="arguments">Arguments to check</param>
        public void CheckArgumentCount(params int[] arguments)
        {
            bool checkpassed = false;
            foreach (int argument in arguments)
            {
                if (_arguments.Length == argument)
                {
                    checkpassed = true;
                    break;
                }
            }
            if (!checkpassed)
            {
                string? countString = string.Join(" or ", arguments);
                throw new InvalidOperationException($"Command expected {countString} arguments. {_arguments.Length} was given");
            }
        }

        /// <summary>
        /// Throw an exception if argument count is below a limit.
        /// </summary>
        /// <param name="count">Number of arguments to expect</param>
        public void CheckMinimumArgumentCount(int count)
        {
            if (_arguments.Length < count)
                throw new InvalidOperationException($"Command expected at least {count} arguments.");
        }

        /// <summary>
        /// Create a new instance of arguments
        /// </summary>
        /// <param name="args">raw tokenized sting arguments</param>
        /// <param name="culture">Culture to use for parsing</param>
        public Arguments(string[] args, CultureInfo culture)
        {
            _arguments = args;
            CurrentCulture = culture;
        }

        /// <summary>
        /// Try to get a converted value of the given argument index
        /// </summary>
        /// <typeparam name="T">Target type</typeparam>
        /// <param name="index">argument index</param>
        /// <param name="result">converted ouptut</param>
        /// <param name="ignore">string to ignore at the begining of the argument</param>
        /// <returns>true, if conversion was successfull, othwerwise false</returns>
        public bool TryGet<T>(int index, out T? result, string? ignore = "") where T : IConvertible
        {
            string? preprocessed = _arguments[index];
            if (!string.IsNullOrEmpty(ignore)
                && preprocessed.StartsWith(ignore))
            {
                preprocessed = preprocessed[ignore.Length..];
            }

            try
            {
                if (typeof(T).IsEnum
                    && Enum.TryParse(typeof(T), preprocessed, true, out object? enumObject)
                    && enumObject != null)
                {
                    result = (T)enumObject;
                    return true;
                }
                else
                {
                    result = (T)Convert.ChangeType(preprocessed, typeof(T), CurrentCulture);
                    return true;
                }
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        /// <summary>
        /// Get a converted value of the given argument index. If conversion fails, an InvalidOperationException is thrown
        /// </summary>
        /// <typeparam name="T">Target type</typeparam>
        /// <param name="index">argument index</param>
        /// <param name="ignore">string to ignore at the begining of the argument</param>
        /// <returns>Converted value</returns>
        public T Get<T>(int index, string? ignore = null) where T : IConvertible
        {
            string? preprocessed = _arguments[index];
            if (!string.IsNullOrEmpty(ignore)
                && preprocessed.StartsWith(ignore))
            {
                preprocessed = preprocessed[ignore.Length..];
            }

            try
            {
                if (typeof(T).IsEnum
                    && Enum.TryParse(typeof(T), preprocessed, true, out object? enumObject)
                    && enumObject != null)
                {
                    return (T)enumObject;
                }

                return (T)Convert.ChangeType(preprocessed, typeof(T), CurrentCulture);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get a converted value of the given indexes. If conversion fails, an InvalidOperationException is thrown
        /// </summary>
        /// <typeparam name="T">Target type</typeparam>
        /// <param name="start">start index</param>
        /// <param name="end">end index</param>
        /// <returns>an enumerable of converted values</returns>
        public IEnumerable<T> GetRange<T>(int start = 0, int end = -1) where T : IConvertible
        {
            if (end == -1)
                end = _arguments.Length;
            for (int i = start; i < end; i++)
            {
                yield return Get<T>(i);
            }
        }

        /// <summary>
        /// Number of available arguments
        /// </summary>
        public int Count => _arguments.Length;
    }
}
