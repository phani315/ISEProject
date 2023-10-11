using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.LoginWatchList.Error
{
    [ExcludeFromCodeCoverage]
    public class Error
    {
        #region Properties

        /// <summary>
        /// Gets or sets the error number.
        /// </summary>
        public int ErrorNumber { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string ErrorMessage { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the Error class with the specified error number and error message.
        /// </summary>
        /// <param name="errorNumber">The error number.</param>
        /// <param name="errorMessage">The error message.</param>
        public Error(int errorNumber, string errorMessage)
        {
            ErrorNumber = errorNumber;
            ErrorMessage = errorMessage;
        }

        #endregion
    }
}
