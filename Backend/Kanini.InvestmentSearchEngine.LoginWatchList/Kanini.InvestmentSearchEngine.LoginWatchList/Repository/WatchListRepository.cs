using Kanini.InvestmentSearchEngine.LoginWatchList.Context;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models;
using Kanini.InvestmentSearchEngine.LoginWatchList.CustomExceptions;
using Microsoft.EntityFrameworkCore;
using Kanini.InvestmentSearchEngine.LoginWatchList.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Kanini.InvestmentSearchEngine.LoginWatchList.Repository
{
    /// <summary>
    /// This class represents a repository for managing watch list data within the application.
    /// It implements the generic repository interface where K is of type int and T is of type WatchList.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class WatchListRepository : IRepository<int, WatchList>
    {
        private readonly AuthenticationAndWatchListContext _context;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WatchListRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used for data access.</param>
        public WatchListRepository(AuthenticationAndWatchListContext context)
        {
            _context = context;
        }

        #endregion

        #region Add Method

        /// <summary>
        /// Adds a watch list to the repository and returns the added watch list if successful.
        /// </summary>
        /// <param name="item">The watch list to be added.</param>
        /// <returns>The added watch list if the operation succeeds; otherwise, returns null.</returns>
        public async Task<WatchList?> Add(WatchList item)
        {
            _context.WatchLists.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        #endregion

        #region Delete Method

        /// <summary>
        /// Deletes a watch list from the repository based on its unique identifier and returns the deleted watch list if successful.
        /// </summary>
        /// <param name="Key">The unique identifier of the watch list to be deleted.</param>
        /// <returns>The deleted watch list if the operation succeeds; otherwise, returns null.</returns>
        public async Task<WatchList?> Delete(int Key)
        {
            var item = await Get(Key);
            if (item != null)
            {
                _context.WatchLists.Remove(item);
                await _context.SaveChangesAsync();
                return item;
            }
            throw new CustomException("Unable to delete");
        }

        #endregion

        #region Get Method

        /// <summary>
        /// Retrieves a watch list from the repository based on its unique identifier and returns the watch list if found.
        /// Throws an Exception if the watch list is not found.
        /// </summary>
        /// <param name="Key">The unique identifier of the watch list to retrieve.</param>
        /// <returns>The retrieved watch list if found; otherwise, throws an Exception.</returns>
        public async Task<WatchList?> Get(int Key)
        {
            var item = await _context.WatchLists.FirstOrDefaultAsync(w => w.WatchListId == Key);
            return item ?? throw new Exception("Unable to find");
        }

        #endregion

        #region GetAll Method

        /// <summary>
        /// Retrieves all watch lists from the repository and returns them as a collection if found.
        /// </summary>
        /// <returns>A collection of all watch lists in the repository if found; otherwise, returns null.</returns>
        public async Task<ICollection<WatchList>?> GetAll()
        {
            var watchList = await _context.WatchLists.ToListAsync();
            return watchList;
        }

        #endregion
    }
}
