using Kanini.InvestmentSearchEngine.LoginWatchList.Context;
using Kanini.InvestmentSearchEngine.LoginWatchList.Interfaces;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models;
using Kanini.InvestmentSearchEngine.LoginWatchList.CustomExceptions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.LoginWatchList.Repository
{
    /// <summary>
    /// This class represents a repository for managing user data within the application.
    /// It implements the generic repository interface where K is of type int and T is of type User.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class UserRepository : IRepository<int, User>
    {
        #region Properties
        private readonly AuthenticationAndWatchListContext _context;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used for data access.</param>
        public UserRepository(AuthenticationAndWatchListContext context)
        {
            _context = context;
        }

        #endregion

        #region Add Method

        /// <summary>
        /// Adds a user to the repository and returns the added user if successful.
        /// </summary>
        /// <param name="item">The user to be added.</param>
        /// <returns>The added user if the operation succeeds; otherwise, returns null.</returns>
        public async Task<User?> Add(User item)
        {
            _context.Users.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        #endregion

        #region Delete Method

        /// <summary>
        /// Deletes a user from the repository based on their unique identifier and returns the deleted user if successful.
        /// </summary>
        /// <param name="Key">The unique identifier of the user to be deleted.</param>
        /// <returns>The deleted user if the operation succeeds; otherwise, returns null.</returns>
        public async Task<User?> Delete(int Key)
        {
            var item = await Get(Key);
            if (item != null)
            {
                _context.Remove(item);
                await _context.SaveChangesAsync();
                return item;
            }
            throw new CustomException("Unable to delete");
        }

        #endregion

        #region Get Method

        /// <summary>
        /// Retrieves a user from the repository based on their unique identifier and returns the user if found.
        /// Throws a CustomException if the user is not found.
        /// </summary>
        /// <param name="Key">The unique identifier of the user to retrieve.</param>
        /// <returns>The retrieved user if found; otherwise, throws a CustomException.</returns>
        public async Task<User?> Get(int Key)
        {
            var item = await _context.Users.FirstOrDefaultAsync(u => u.Id == Key);
            return item ?? throw new CustomException("Please register, you are not registered.");
        }

        #endregion

        #region GetAll Method

        /// <summary>
        /// Retrieves all users from the repository and returns them as a collection if found.
        /// Throws an Exception if no users are found.
        /// </summary>
        /// <returns>A collection of all users in the repository if found; otherwise, throws an Exception.</returns>
        public async Task<ICollection<User>?> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return users == null ? throw new Exception("No users found.") : (ICollection<User>)users;
        }

        #endregion
    }
}
