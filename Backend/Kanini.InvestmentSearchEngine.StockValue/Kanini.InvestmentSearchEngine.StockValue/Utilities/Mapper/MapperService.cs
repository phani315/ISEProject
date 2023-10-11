using Kanini.InvestmentSearchEngine.StockValue.Interfaces;
using Kanini.InvestmentSearchEngine.StockValue.Models;
using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.StockValue.Utilities.Mapper
{
    [ExcludeFromCodeCoverage]
    public class MapperService:IMapperService
    {
        public Task<StockPrice> StockPriceMapper(StockPrice source, StockPrice destination)
        {
            destination.Id = source.Id;
            destination.CompanyId = source.CompanyId;
            destination.CurrentStockPrice = source.CurrentStockPrice;
            destination.UpdatedStockPrice = source.UpdatedStockPrice;
            destination.UpdatedStockPercent = source.UpdatedStockPercent;
            destination.Date = source.Date;
            return Task.FromResult(destination);
        }
    }
}
