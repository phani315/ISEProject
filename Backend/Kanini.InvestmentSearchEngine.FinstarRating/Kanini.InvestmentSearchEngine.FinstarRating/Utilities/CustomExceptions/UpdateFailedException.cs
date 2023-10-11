using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Kanini.InvestmentSearchEngine.FinstarRating.CustomExceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]

    public class UpdateFailedException : Exception
    {
        public UpdateFailedException()
        {
        }

        public UpdateFailedException(string? message) : base(message)
        {

        }

        public UpdateFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UpdateFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}