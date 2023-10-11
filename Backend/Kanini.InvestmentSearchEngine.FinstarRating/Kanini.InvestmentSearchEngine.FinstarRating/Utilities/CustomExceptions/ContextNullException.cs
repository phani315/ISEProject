using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Kanini.InvestmentSearchEngine.FinstarRating.CustomExceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]

    internal class ContextNullException : Exception
    {
        public ContextNullException()
        {
        }

        public ContextNullException(string? message) : base(message)
        {
        }

        public ContextNullException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ContextNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}