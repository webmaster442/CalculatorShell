using System.Runtime.Serialization;

namespace CalculatorShell.Expressions
{
    /// <summary>
    /// Exception type thrown by the Expression parser.
    /// </summary>
    [Serializable]
    public class ExpressionEngineException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ExpressionEngineException class.
        /// </summary>
        public ExpressionEngineException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ExpressionEngineException class with a specified error message
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public ExpressionEngineException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ExpressionEngineException class with a specified error
        /// message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception</param>
        /// <param name="innerException">The exception that is the cause of the current exception</param>
        public ExpressionEngineException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ExpressionEngineException class with a specified error format
        /// </summary>
        /// <param name="message">message with format</param>
        /// <param name="arguments">arguments for formatting</param>
        public ExpressionEngineException(string message, params object[] arguments) : this(string.Format(message, arguments))
        {
        }

        /// <summary>
        /// Initializes a new instance of the ExpressionEngineException class with serialized data.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination</param>
        protected ExpressionEngineException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
