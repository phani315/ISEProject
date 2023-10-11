using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Kanini.InvestmentSearchEngine.FinstarRating.CustomExceptions
{
    [ExcludeFromCodeCoverage]

    [Serializable]
    public class CompanyNotFound : Exception
    {
        public CompanyNotFound()
        {
        }

        public CompanyNotFound(string? message) : base(message)
        {
        }

        public CompanyNotFound(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CompanyNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}