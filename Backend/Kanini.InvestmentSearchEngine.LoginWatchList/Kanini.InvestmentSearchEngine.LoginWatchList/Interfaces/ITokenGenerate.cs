using Kanini.InvestmentSearchEngine.LoginWatchList.Models.DTO;

namespace Kanini.InvestmentSearchEngine.LoginWatchList.Interfaces
{
    /// <summary>
    /// Represents an interface for generating authentication tokens.
    /// </summary>
    public interface ITokenGenerate
    {
        #region GenerateToken Method

        /// <summary>
        /// Generates an authentication token for a user based on the provided user login DTO.
        /// </summary>
        /// <param name="user">The user login DTO for which the token is generated.</param>
        /// <returns>The generated authentication token.</returns>
        public string GenerateToken(UserLoginDTO user);

        #endregion
    }
}
