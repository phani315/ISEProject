using Kanini.InvestmentSearchEngine.LoginWatchList.Interfaces;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models.DTO;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models;
using Kanini.InvestmentSearchEngine.LoginWatchList.CustomExceptions;

namespace Kanini.InvestmentSearchEngine.LoginWatchList.Services
{
    /// <summary>
    /// This class provides services related to managing user watch lists.
    /// It implements the <see cref="IWatchListService"/> interface.
    /// </summary>
    public class WatchListService : IWatchListService
    {
        #region Properties
        private readonly IRepository<int, WatchList> _watchListRepository;
        private readonly IRepository<int, User> _userRepository;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WatchListService"/> class.
        /// </summary>
        /// <param name="watchListRepository">The watch list repository for data access.</param>
        /// <param name="userRepository">The user repository for data access.</param>
        public WatchListService(IRepository<int, WatchList> watchListRepository, IRepository<int, User> userRepository)
        {
            _watchListRepository = watchListRepository;
            _userRepository = userRepository;
        }

        #endregion

        #region AddCompanyToWatchList Method

        /// <summary>
        /// Adds a company to a user's watch list.
        /// </summary>
        /// <param name="watchList">The watch list to add to the user's watch list.</param>
        /// <returns>The added watch list if the operation succeeds; otherwise, throws exceptions.</returns>
        public async Task<WatchList?> AddCompanyToWatchList(WatchList watchList)
        {
            _ = await _userRepository.Get(watchList.UserId) ?? throw new CustomException("Please register or login to add to the watchlist");

            var existingWatchList = await _watchListRepository.GetAll();

            if (existingWatchList != null)
            {
                if (existingWatchList.Any(w => w.UserId == watchList.UserId && w.CompanyId == watchList.CompanyId))
                    throw new CustomException("The company is already added to the watchlist");
            }

            var addedWatchlist = await _watchListRepository.Add(watchList);
            return addedWatchlist ?? throw new CustomException("Unable to add this movement");
        }

        #endregion

        #region RemovingFromWatchList Method

        /// <summary>
        /// Removes a company from a user's watch list based on the provided watch list ID.
        /// </summary>
        /// <param name="getWatchListIdDTO">The DTO containing the user ID and company ID to remove from the watch list.</param>
        /// <returns>The removed watch list item if the operation succeeds; otherwise, throws exceptions.</returns>
        public async Task<WatchList?> RemovingFromWatchList(GetWatchListIdDTO getWatchListIdDTO)
        {
            var watchListCollection = await _watchListRepository.GetAll() ?? throw new Exception("No data found");
            var watchLists = watchListCollection.FirstOrDefault(w =>
                w.UserId == getWatchListIdDTO.UserId && w.CompanyId == getWatchListIdDTO.CompanyId) ?? 
                throw new CustomException("Your watchlist is empty");

            var watchList = await _watchListRepository.Delete(watchLists.WatchListId);
            return watchList ?? throw new CustomException("An error occurred while deleting");
        }

        #endregion

        #region ViewWatchList Method

        /// <summary>
        /// Retrieves a user's watch list and returns a collection of watch list items.
        /// </summary>
        /// <param name="watchListUserIdDTO">The DTO containing the user ID to retrieve the watch list.</param>
        /// <returns>A collection of watch list items if found; otherwise, throws empty list.</returns>
        public async Task<ICollection<WatchListsCompanysDTO>?> ViewWatchList(WatchListUserIdDTO watchListUserIdDTO)
        {
            List<WatchListsCompanysDTO> watchListsCompanysDTO = new();
            var watchListCollection = await _watchListRepository.GetAll() ?? throw new CustomException("no items in watchlist");
            watchListsCompanysDTO = watchListCollection.Where(w => w.UserId == watchListUserIdDTO.UserId)
                        .Select(s => new WatchListsCompanysDTO
                        {
                            CompanyIds = s.CompanyId
                        })
                        .ToList();
            return watchListsCompanysDTO;
        }

        #endregion

    }
}
