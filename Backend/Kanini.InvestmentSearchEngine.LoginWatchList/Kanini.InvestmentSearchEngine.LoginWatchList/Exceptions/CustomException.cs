using System;
using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.LoginWatchList.CustomExceptions
{
    [ExcludeFromCodeCoverage]
    public class CustomException : Exception
    {
        #region Default Constructor

        /// <summary>
        /// Initializes a new instance of the CustomException class with a default error message.
        /// </summary>
        public CustomException() : base("User Exception raised") { }

        #endregion

        #region Constructor with Message Parameter

        /// <summary>
        /// Initializes a new instance of the CustomException class with a custom error message.
        /// </summary>
        /// <param name="message">The custom error message.</param>
        public CustomException(string message) : base(message) { }

        #endregion
    }
}
