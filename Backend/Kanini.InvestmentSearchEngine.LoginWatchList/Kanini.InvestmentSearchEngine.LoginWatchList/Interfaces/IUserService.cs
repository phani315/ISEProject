using Kanini.InvestmentSearchEngine.LoginWatchList.Models.DTO;
using Kanini.InvestmentSearchEngine.LoginWatchList.CustomExceptions;

namespace Kanini.InvestmentSearchEngine.LoginWatchList.Interfaces
{
    /// <summary>
    /// Represents an interface for user registration and authentication services.
    /// </summary>
    public interface IUserService
    {
        #region Register Method

        /// <summary>
        /// Registers a new user based on the provided user registration DTO.
        /// </summary>
        /// <param name="userDTO">The user registration DTO containing registration data.</param>
        /// <returns>The user login DTO if registration is successful; otherwise, throws a custom exception.</returns>
        /// <exception cref="CustomException">Thrown if registration fails for any reason.</exception>
        public Task<UserLoginDTO?> Register(UserRegisterDTO userDTO);

        #endregion

        #region Login Method

        /// <summary>
        /// Logs in a user with the provided login credentials.
        /// </summary>
        /// <param name="userDTO">The user login DTO containing login credentials.</param>
        /// <returns>The user login DTO if authentication is successful; otherwise, throws a custom exception.</returns>
        /// <exception cref="CustomException">Thrown if login fails for any reason.</exception>
        public Task<UserLoginDTO?> Login(UserLoginDTO userDTO);

        #endregion
    }
}
