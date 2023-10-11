using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Kanini.InvestmentSearchEngine.FinstarRating.CustomExceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]

    public class InvalidIdException : Exception
    {
        public InvalidIdException()
        {
        }

        public InvalidIdException(string? message) : base(message)
        {
        }

        public InvalidIdException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public InvalidIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}