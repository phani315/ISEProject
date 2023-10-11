namespace KaniniInvestmentSearchEngineCompanyMarketEssentials.Interfaces
{
    public interface ICompanyEssentialsServices<K,T>
    {
        #region Company Services Interfaces
        public Task<T?> AddEssentials(T item);
        public Task<ICollection<T>?> GetAllEssentials();
        public Task<T?> GetEssential(K key);
        public Task<T?> UpdateEssential(T item);
        public Task<T?> DeleteEssential(K key);
        public Task<ICollection<T?>?> FilterCompanies();
        #endregion

    }
}
