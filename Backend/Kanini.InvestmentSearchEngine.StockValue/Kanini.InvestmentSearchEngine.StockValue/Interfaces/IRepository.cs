namespace Kanini.InvestmentSearchEngine.StockValue.Interfaces
{
    public interface IRepository<K, T>
    {
        #region Method for CURD Operations
        public Task<T> Add(T item);
        public Task<T> Update(T item);
        public Task<T> Delete(K key);
        public Task<T> Get(K key);
        public Task<ICollection<T>> GetAll();
        #endregion
    }
}
