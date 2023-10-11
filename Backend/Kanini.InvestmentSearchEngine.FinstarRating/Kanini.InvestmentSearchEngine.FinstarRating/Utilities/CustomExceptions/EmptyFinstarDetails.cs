using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Kanini.InvestmentSearchEngine.FinstarRating.CustomExceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]

    public class EmptyFinstarDetails : Exception
    {
        public EmptyFinstarDetails()
        {
        }

        public EmptyFinstarDetails(string? message) : base(message)
        {
        }

        public EmptyFinstarDetails(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EmptyFinstarDetails(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}