using Kanini.InvestmentSearchEngine.StockValue.Models;

namespace Kanini.InvestmentSearchEngine.StockValue.Interfaces
{
    public interface IMapperService
    {
        public Task<StockPrice> StockPriceMapper(StockPrice source, StockPrice destination);
    }
}
