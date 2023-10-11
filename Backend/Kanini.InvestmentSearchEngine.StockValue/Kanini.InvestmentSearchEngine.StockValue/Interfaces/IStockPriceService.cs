using Kanini.InvestmentSearchEngine.StockValue.Models;
using Kanini.InvestmentSearchEngine.StockValue.Models.DTOs;

namespace Kanini.InvestmentSearchEngine.StockValue.Interfaces
{
    public interface IStockPriceService
    {
        #region Method for performing stock calculations and operations
        public Task<StockPrice> AddStockPriceService(StockPrice stockPrice);
        public Task<StockPrice> UpdateStockPriceService(StockPrice stockPrice);
        public Task<StockPrice> DeleteStockPriceService(int Id);
        public Task<StockPrice> GetStockPriceService(int Id);
        public Task<ICollection<StockPrice>> GetAllStockPriceService();
        Task<StockPriceAveragesDTO> CalculateStockAveragesByCompanyID(int companyID);
        Task<Double> GetYearHigh(int stockId);
        Task<Double> GetYearLow(int stockId);
        Task<Double> GetTodayHigh(int stockId);
        Task<Double> GetTodayLow(int stockId);
        Task<StockPrice> UpdateCurrentStockValue(StockPriceUpdateDTO stockPriceChangeDTO);
        Task<double> GetInitialValueOfTheStock(int stockId);
        Task<CurrentStockPriceDTO> GetCurrentStockPriceDetails(int companyId);
        Task<StockPrice> GetStockPriceByCompanyID(int companyId);
        #endregion
    }
}
