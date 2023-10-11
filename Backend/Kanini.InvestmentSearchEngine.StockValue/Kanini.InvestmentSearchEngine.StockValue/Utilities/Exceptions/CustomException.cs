using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.StockValue.Utilities.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class CustomException : Exception
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public CustomException() : base("Context is Empty") { }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="message"></param>
        public CustomException(string message) : base(message) { }
        #endregion
    }

    [ExcludeFromCodeCoverage]
    public class UserException : Exception
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public UserException() : base("User exception raised") { }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="message"></param>
        public UserException(string message) : base(message) { }
        #endregion
    }

    [ExcludeFromCodeCoverage]
    public class ContextException : Exception
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public ContextException() : base("Context exception raised") { }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="message"></param>
        public ContextException(string message) : base(message) { }
        #endregion
    }

    [ExcludeFromCodeCoverage]
    public class AddObjectException : Exception
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public AddObjectException() : base("Add object exception raised")
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="message"></param>
        public AddObjectException(string? message) : base(message)
        {
        }
        #endregion
    }

    [ExcludeFromCodeCoverage]
    public class UpdateObjectException : Exception
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public UpdateObjectException() : base("Update Object exception raised")
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="message"></param>
        public UpdateObjectException(string? message) : base(message)
        {
        }
        #endregion
    }

    [ExcludeFromCodeCoverage]
    public class UnableToUpdateCurrentStockPrice : Exception
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public UnableToUpdateCurrentStockPrice() : base("Unable to update current stock price exception raised")
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="message"></param>
        public UnableToUpdateCurrentStockPrice(string? message) : base(message)
        {
        }
        #endregion
    }
}

