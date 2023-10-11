using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Kanini.InvestmentSearchEngine.FinstarRating.CustomExceptions
{
    [ExcludeFromCodeCoverage]

    [Serializable]
    public class CompanyIdAlreadyExistsException : Exception
    {
        public CompanyIdAlreadyExistsException()
        {
        }

        public CompanyIdAlreadyExistsException(string? message) : base(message)
        {
        }

        public CompanyIdAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CompanyIdAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}