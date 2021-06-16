using CalculatorShell.Expressions.Internals;
using System;
using System.Runtime.Serialization;

namespace CalculatorShell.Expressions
{
    [Serializable]
    public class ExpressionEngineException : Exception
    {
        private TokenType type;

        public ExpressionEngineException() : base()
        {
        }

        public ExpressionEngineException(string message) : base(message)
        {
        }

        public ExpressionEngineException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ExpressionEngineException(string message, params object[] arguments) : this(string.Format(message, arguments))
        {
        }

        protected ExpressionEngineException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
