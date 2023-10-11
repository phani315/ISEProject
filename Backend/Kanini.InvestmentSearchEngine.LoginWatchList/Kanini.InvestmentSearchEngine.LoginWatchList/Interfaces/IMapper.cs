using Kanini.InvestmentSearchEngine.LoginWatchList.Models;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models.DTO;

namespace Kanini.InvestmentSearchEngine.LoginWatchList.Interfaces
{
    /// <summary>
    /// Represents an interface for mapping user-related objects.
    /// </summary>
    public interface IMapper
    {
        #region UserToUserLoginDTO Method

        /// <summary>
        /// Maps a User object to a UserLoginDTO object.
        /// </summary>
        /// <param name="user">The User object to be mapped.</param>
        /// <returns>The mapped UserLoginDTO object.</returns>
        public UserLoginDTO? UserToUserLoginDTO(User user);

        #endregion
    }
}
