namespace Kanini.InvestmentSearchEngine.SWOTAnalysis.Interfaces
{
    public interface IRepository<K, T>
    {
        #region SWOTRepository Interface
        public Task<T?> Add(T item);
        public Task<T?> Update(T item);
        public Task<T?> Delete(K key);
        public Task<T?> Get(K key);
        public Task<ICollection<T>?> GetAll();
        #endregion
    }

}
