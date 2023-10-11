using System.Runtime.Serialization;

namespace Kanini.InvestmentSearchEngine.SWOTAnalysis.Exceptions
{
    [Serializable]
    public class NullSWOTDetailsException : Exception
    {
        public NullSWOTDetailsException()
        {
        }

        public NullSWOTDetailsException(string? message) : base(message)
        {
        }

        public NullSWOTDetailsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NullSWOTDetailsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}