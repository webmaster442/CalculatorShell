namespace CalculatorShell.Maths
{
    [Serializable]
    public class TypeException : Exception
    {
        public TypeException() : base()
        {
        }

        public TypeException(string message) : base(message)
        {
        }

        public TypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TypeException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
