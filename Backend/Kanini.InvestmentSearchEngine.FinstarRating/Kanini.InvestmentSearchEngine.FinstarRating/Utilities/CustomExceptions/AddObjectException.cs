using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Kanini.InvestmentSearchEngine.FinstarRating.CustomExceptions
{
    [ExcludeFromCodeCoverage]

    [Serializable]
    public class AddObjectException : Exception
    {
        public AddObjectException()
        {
        }

        public AddObjectException(string? message) : base(message)
        {
        }

        public AddObjectException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AddObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}