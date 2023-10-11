namespace KaniniInvestmentSearchEngineCompanyMarketEssentials.Interfaces
{
    public interface IRepository<K,T>
    {
        #region Company Essentials Repository Interface
        public Task<ICollection<T>?> GetAll();
        public Task<T?> Get(K key);
        public Task<T?> Add(T item);
        public Task<T?> Update(T item); 
        public Task<T?> Delete(K key);
        #endregion
    }
}
