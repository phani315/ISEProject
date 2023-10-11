using Kanini.InvestmentSearchEngine.StockValue.Interfaces;
using Kanini.InvestmentSearchEngine.StockValue.Models;
using Kanini.InvestmentSearchEngine.StockValue.Models.DTOs;
using Kanini.InvestmentSearchEngine.StockValue.Utilities.Exceptions;
using Kanini.InvestmentSearchEngine.StockValue.Utilities.Mapper;
using Kanini.InvestmentSearchEngine.StockValue.Utilities.Message;

namespace Kanini.InvestmentSearchEngine.StockValue.Services
{
    public class StockPriceService : IStockPriceService
    {
        #region Private field
        private readonly IRepository<int, StockPrice> _stockPriceRepository;
        private readonly IRepository<int, StockTransaction> _stockTransactionRepository;
        private readonly IMapperService _mapper;
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="stockPriceRepository"></param>
        /// <param name="stockTransactionRepository"></param>
        public StockPriceService(IRepository<int, StockPrice> stockPriceRepository, 
                                 IRepository<int, StockTransaction> stockTransactionRepository,
                                 IMapperService mapper)
        {
            _stockPriceRepository = stockPriceRepository;
            _stockTransactionRepository = stockTransactionRepository;
            _mapper = mapper;
        }
        #endregion

        #region BL Method for Adding StockPrice
        /// <summary>
        /// BL Method for Adding StockPrice
        /// </summary>
        /// <param name="stockPrice"></param>
        /// <returns></returns>
        /// <exception cref="UserException"></exception>
        /// <exception cref="AddObjectException"></exception>
        public async Task<StockPrice> AddStockPriceService(StockPrice stockPrice)
        {
            var stockPrices = await _stockPriceRepository.GetAll();
            var existingPrices = new HashSet<StockPrice>(stockPrices);

            if (existingPrices.Contains(stockPrice))
            {
                throw new UserException(ResponseMessage.Messages[20]);
            }

            return await _stockPriceRepository.Add(stockPrice);
        }
        #endregion

        #region BL Method for Deleting StockPrice
        /// <summary>
        /// BL Method for Deleting StockPrice
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<StockPrice> DeleteStockPriceService(int Id)
        {
            return await _stockPriceRepository.Delete(Id);
        }
        #endregion

        #region BL Method for Getting All StockPrices By CompanyID
        /// <summary>
        /// BL Method for Getting All StockPrices By CompanyID
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<StockPrice> GetStockPriceByCompanyID(int companyId)
        {
            var stockPrices = await _stockPriceRepository.GetAll();
            var stockPrice = stockPrices.FirstOrDefault(s => s.CompanyId == companyId)
                                        ?? throw new NullReferenceException(ResponseMessage.Messages[19]);
            return stockPrice;
        }
        #endregion

        #region BL Method for Calculating StockAverages By CompanyID
        /// <summary>
        /// BL Method for Calculating StockAverages By CompanyID
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<StockPriceAveragesDTO> CalculateStockAveragesByCompanyID(int companyID)
        {
            var stockPrices = await _stockPriceRepository.GetAll();
            var stockInfoByCompanyID = stockPrices.FirstOrDefault(s => s.CompanyId == companyID)
                                      ?? throw new NullReferenceException(ResponseMessage.Messages[11]);

            var stockId = stockInfoByCompanyID.Id;
            var yearHigh = await GetYearHigh(stockId);
            var yearLow = await GetYearLow(stockId);
            var todayHigh = await GetTodayHigh(stockId);
            var todayLow = await GetTodayLow(stockId);

            return new StockPriceAveragesDTO
            {
                CompanyID = companyID,
                YearHigh = yearHigh,
                YearLow = yearLow,
                TodayHigh = todayHigh,
                TodayLow = todayLow
            };
        }

        #endregion

        #region BL Method for Getting All StockPrices
        /// <summary>
        /// BL Method for Getting All StockPrices
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<StockPrice>> GetAllStockPriceService()
        {
            return await _stockPriceRepository.GetAll();
        }
        #endregion

        #region BL Method for Getting Current StockPrice Details
        /// <summary>
        /// Getting Current StockPrice Details
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<CurrentStockPriceDTO> GetCurrentStockPriceDetails(int companyId)
        {
            var stockDetails = await _stockPriceRepository.GetAll();
            var stockInfoByCompanyID = stockDetails.FirstOrDefault(s => s.CompanyId == companyId)
                                      ?? throw new NullReferenceException(ResponseMessage.Messages[19]);

            return new CurrentStockPriceDTO
            {
                CurrentStockPrice = Math.Round(stockInfoByCompanyID.CurrentStockPrice, 2),
                UpdatedStockPercent = Math.Round(stockInfoByCompanyID.UpdatedStockPercent, 2),
                UpdatedStockPrice = stockInfoByCompanyID.UpdatedStockPrice,
                Date = stockInfoByCompanyID.Date
            };
        }

        #endregion

        #region  BL Method for Getting Initial Value Of The Stock
        /// <summary>
        /// Getting Initial Value Of The Stock
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        public async Task<double> GetInitialValueOfTheStock(int stockId)
        {
            DateTime yesterday = DateTime.Today.AddDays(-1);
            DateTime today = DateTime.Today;

            var stockPrices = await _stockPriceRepository.GetAll();

            var initialValue = stockPrices?.FirstOrDefault(s => s.Id == stockId)
                ?.StockTransactions?
                .Where(t => t.Date.Date <= yesterday.Date)
                .OrderByDescending(t => t.Date)
                .FirstOrDefault()?.StockValue ?? 0.0;

            if (initialValue == 0.0)
            {
                initialValue = stockPrices?.FirstOrDefault(s => s.Id == stockId)
                    ?.StockTransactions?
                    .Where(t => t.Date.Date == today.Date)
                    .OrderByDescending(t => t.Date)
                    .FirstOrDefault()?.StockValue ?? 0.0;
            }

            return initialValue;
        }
        #endregion

        #region BL Method for Getting StockPrice By Id
        /// <summary>
        /// BL Method for Getting StockPrice By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<StockPrice> GetStockPriceService(int Id)
        {
            return await _stockPriceRepository.Get(Id);
        }
        #endregion

        #region BL Method for Calculating Today's High By Id
        /// <summary>
        /// BL Method for Calculating Today's High By Id
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        public async Task<double> GetTodayHigh(int stockId)
        {
            DateTime currentDate = DateTime.Today;

            var stockPrices = await _stockPriceRepository.GetAll();

            var todayHigh = stockPrices?.FirstOrDefault(s => s.Id == stockId)?.StockTransactions
                ?.Where(s => s.Date.Date == currentDate.Date)
                ?.Select(s => s.StockValue)
                ?.DefaultIfEmpty(0)
                ?.Max() ?? 0;

            return Math.Round(todayHigh, 2);
        }
        #endregion

        #region BL Method for Calculating Today's Low By Id
        /// <summary>
        /// BL Method for Calculating Today's Low By Id
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        public async Task<double> GetTodayLow(int stockId)
        {
            DateTime currentDate = DateTime.Today;

            var stockPrices = await _stockPriceRepository.GetAll();

            var todayLow = stockPrices?.FirstOrDefault(s => s.Id == stockId)?.StockTransactions
                ?.Where(s => s.Date.Date == currentDate.Date)
                ?.Select(s => s.StockValue)
                ?.DefaultIfEmpty(0)
                ?.Min() ?? 0;

            return Math.Round(todayLow, 2);
        }
        #endregion

        #region BL Method for Calculating Year's High By Id
        /// <summary>
        /// BL Method for Calculating Year's High By Id
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        public async Task<double> GetYearHigh(int stockId)
        {
            var stockDetails = await _stockPriceRepository.GetAll();

            var yearHigh = stockDetails?.FirstOrDefault(s => s.Id == stockId)
                ?.StockTransactions
                ?.OrderByDescending(s => s.Date)
                ?.Take(365)
                ?.Max(s => s.StockValue) ?? 0.0;

            return Math.Round(yearHigh, 2);
        }
        #endregion

        #region BL Method for Calculating Year's Low By Id
        /// <summary>
        /// BL Method for Calculating Year's Low By Id
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        public async Task<double> GetYearLow(int stockId)
        {
            var stockDetails = await _stockPriceRepository.GetAll();

            var yearLow = stockDetails?.FirstOrDefault(s => s.Id == stockId)
                ?.StockTransactions
                ?.OrderByDescending(s => s.Date)
                ?.Take(365)
                ?.Min(s => s.StockValue) ?? 0.0;

            return Math.Round(yearLow, 2);
        }
        #endregion

        #region BL Method for Updating Current Stock Price
        /// <summary>
        /// BL Method for Updating Current Stock Price
        /// </summary>
        /// <param name="stockPriceChangeDTO"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="UnableToUpdateCurrentStockPrice"></exception>
        public async Task<StockPrice> UpdateCurrentStockValue(StockPriceUpdateDTO stockPriceChangeDTO)
        {
            var stockPrices = await _stockPriceRepository.GetAll();
            var stockPrice = stockPrices?.FirstOrDefault(s => s.CompanyId == stockPriceChangeDTO.companyId) ?? throw new NullReferenceException(ResponseMessage.Messages[19]);
            var initialValue = await GetInitialValueOfTheStock(stockPrice.Id);
            stockPrice.CurrentStockPrice += stockPriceChangeDTO.UpdatedStockPrice;
            stockPrice.UpdatedStockPercent = ((stockPrice.CurrentStockPrice - initialValue) / initialValue) * 100;
            stockPrice.UpdatedStockPrice = stockPriceChangeDTO.UpdatedStockPrice;
            stockPrice.Date = stockPriceChangeDTO.Date;
            var updatedStockPrice = await _stockPriceRepository.Update(stockPrice);
            var stockTransaction = new StockTransaction { StockId = stockPrice.Id, StockValue = stockPrice.CurrentStockPrice, Date = stockPrice.Date };
            var updatedStockTransaction = await _stockTransactionRepository.Add(stockTransaction);
            return updatedStockPrice;
        }
        #endregion

        #region BL Method for Updating StockPrice
        /// <summary>
        /// BL Method for Updating StockPrice
        /// </summary>
        /// <param name="stockPrice"></param>
        /// <returns></returns>
        /// <exception cref="UpdateValueException"></exception>
        public async Task<StockPrice> UpdateStockPriceService(StockPrice stockPrice)
        {
            var existingStockPrice = await _stockPriceRepository.Get(stockPrice.Id);
            await _mapper.StockPriceMapper(stockPrice, existingStockPrice);
            await _stockPriceRepository.Update(existingStockPrice);
            return existingStockPrice;
        }
        #endregion
    }
}
