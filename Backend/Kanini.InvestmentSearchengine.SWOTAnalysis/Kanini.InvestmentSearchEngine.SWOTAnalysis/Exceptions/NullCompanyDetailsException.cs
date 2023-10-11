using System.Runtime.Serialization;

namespace Kanini.InvestmentSearchEngine.SWOTAnalysis.Exceptions
{
    [Serializable]
    public class NullCompanyDetailsException : Exception
    {
        public NullCompanyDetailsException()
        {
        }

        public NullCompanyDetailsException(string? message) : base(message)
        {
        }

        public NullCompanyDetailsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NullCompanyDetailsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}