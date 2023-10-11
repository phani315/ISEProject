using Kanini.InvestmentSearchEngine.LoginWatchList.Models;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models.DTO;
using Kanini.InvestmentSearchEngine.LoginWatchList.CustomExceptions;

namespace Kanini.InvestmentSearchEngine.LoginWatchList.Interfaces
{
    /// <summary>
    /// Represents an interface for managing user watch lists.
    /// </summary>
    public interface IWatchListService
    {
        #region ViewWatchList Method

        /// <summary>
        /// Retrieves a user's watch list and returns a collection of watch list items.
        /// </summary>
        /// <param name="watchListUserIdDTO">The DTO containing the user ID to retrieve the watch list.</param>
        /// <returns>A collection of watch list items if found; otherwise, throws a custom exception.</returns>
        /// <exception cref="CustomException">Thrown if the operation fails for any reason.</exception>
        public Task<ICollection<WatchListsCompanysDTO>?> ViewWatchList(WatchListUserIdDTO watchListUserIdDTO);

        #endregion

        #region RemovingFromWatchList Method

        /// <summary>
        /// Removes a company from a user's watch list based on the provided watch list ID.
        /// </summary>
        /// <param name="getWatchListIdDTO">The DTO containing the user ID and company ID to remove from the watch list.</param>
        /// <returns>The removed watch list item if the operation succeeds; otherwise, throws a custom exception.</returns>
        /// <exception cref="CustomException">Thrown if the operation fails for any reason.</exception>
        public Task<WatchList?> RemovingFromWatchList(GetWatchListIdDTO getWatchListIdDTO);

        #endregion

        #region AddCompanyToWatchList Method

        /// <summary>
        /// Adds a company to a user's watch list.
        /// </summary>
        /// <param name="watchList">The watch list to add to the user's watch list.</param>
        /// <returns>The added watch list item if the operation succeeds; otherwise, throws a custom exception.</returns>
        /// <exception cref="CustomException">Thrown if the operation fails for any reason.</exception>
        public Task<WatchList?> AddCompanyToWatchList(WatchList watchList);

        #endregion
    }
}
