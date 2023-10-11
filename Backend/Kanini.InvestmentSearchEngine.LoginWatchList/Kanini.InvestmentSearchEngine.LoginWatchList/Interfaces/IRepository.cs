using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kanini.InvestmentSearchEngine.LoginWatchList.Interfaces
{
    /// <summary>
    /// Represents a generic repository interface for CRUD operations.
    /// </summary>
    /// <typeparam name="K">The type of the key used for CRUD operations.</typeparam>
    /// <typeparam name="T">The type of the data to be stored in the repository.</typeparam>
    public interface IRepository<K, T>
    {
        #region Add Method

        /// <summary>
        /// Adds an item to the repository.
        /// </summary>
        /// <param name="item">The item to be added.</param>
        /// <returns>The added item if the operation succeeds; otherwise, throws a custom exception.</returns>
        public Task<T?> Add(T item);

        #endregion

        #region Get Method

        /// <summary>
        /// Retrieves an item from the repository based on the provided key.
        /// </summary>
        /// <param name="Key">The key used to retrieve the item.</param>
        /// <returns>The retrieved item if found; otherwise, throws a custom exception.</returns>
        public Task<T?> Get(K Key);

        #endregion

        #region Delete Method

        /// <summary>
        /// Deletes an item from the repository based on the provided key.
        /// </summary>
        /// <param name="Key">The key used to delete the item.</param>
        /// <returns>The deleted item if the operation succeeds; otherwise, throws a custom exception.</returns>
        public Task<T?> Delete(K Key);

        #endregion

        #region GetAll Method

        /// <summary>
        /// Retrieves all items from the repository.
        /// </summary>
        /// <returns>A collection of all items in the repository if found; otherwise, throws a custom exception.</returns>
        public Task<ICollection<T>?> GetAll();

        #endregion
    }
}
