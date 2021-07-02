﻿using System;
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
            foreach (var argument in arguments)
            {
                if (_arguments.Length == argument)
                {
                    checkpassed = true;
                    break;
                }
            }
            if (!checkpassed)
            {
                var countString = string.Join(" or ", arguments);
                throw new InvalidOperationException($"Command expected {countString} arguments. {_arguments.Length} was given");
            }
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
        /// <returns>true, if conversion was successfull, othwerwise false</returns>
        public bool TryGet<T>(int index, out T? result) where T : IConvertible
        {
            try
            {
                result = (T)Convert.ChangeType(_arguments[index], typeof(T), CurrentCulture);
                return true;
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
        /// <param name="failMsg">Message of the Exception, when the conversion failed</param>
        /// <returns>Converted value</returns>
        public T Get<T>(int index, string? failMsg = null) where T : IConvertible
        {
            try
            {
                return (T)Convert.ChangeType(_arguments[index], typeof(T), CurrentCulture);
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(failMsg))
                    throw new InvalidOperationException(ex.Message, ex);
                else
                    throw new InvalidOperationException(failMsg, ex);
            }
        }

        /// <summary>
        /// Number of available arguments
        /// </summary>
        public int Count => _arguments.Length;
    }
}
