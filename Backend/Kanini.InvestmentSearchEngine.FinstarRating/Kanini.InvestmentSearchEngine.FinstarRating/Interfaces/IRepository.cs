namespace Kanini.InvestmentSearchEngine.FinstarRating.Interfaces
{
    public interface IRepo<k, T>
    {
        #region Finstar Repository
        public Task<ICollection<T>?> GetAll();

        public Task<T?> Get(int id);

        public Task<T?> Update(T item);
        public Task<T?> Delete(k key);

        public Task<T?> Add(T item);
        #endregion
    }

}
