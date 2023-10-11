using Kanini.InvestmentSearchEngine.StockValue.Interfaces;
using Kanini.InvestmentSearchEngine.StockValue.Models.Context;
using Kanini.InvestmentSearchEngine.StockValue.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Kanini.InvestmentSearchEngine.StockValue.Utilities.Exceptions;
using Kanini.InvestmentSearchEngine.StockValue.Utilities.Message;

namespace Kanini.InvestmentSearchEngine.StockValue.Repositories
{
    [ExcludeFromCodeCoverage]
    public class StockPriceRepository : IRepository<int, StockPrice>
    {
        #region Private field
        private readonly StockPriceContext _stockPriceContext;
        private readonly ILogger<StockPriceRepository> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="stockPriceContext"></param>
        public StockPriceRepository(StockPriceContext stockPriceContext,ILogger<StockPriceRepository> logger)
        {
            _stockPriceContext = stockPriceContext;
            _logger = logger;
        }
        #endregion

        #region Repo Method for Add StockPrice
        /// <summary>
        /// Repo Method for Add StockPrice
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ContextException"></exception>
        /// <exception cref="AddObjectException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<StockPrice> Add(StockPrice item)
        {
            var transaction = _stockPriceContext.Database.BeginTransaction();
            try
            {
                if (_stockPriceContext.StockPrices == null)
                    throw new ContextException(ResponseMessage.Messages[10]);
                _ = (await _stockPriceContext.StockPrices.AddAsync(item)) ?? throw new AddObjectException(ResponseMessage.Messages[12] + item.Id);
                await _stockPriceContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return item;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message,ex);
            }
        }
        #endregion

        #region Repo Method for Delete StockPrice
        /// <summary>
        /// Repo Method for Delete StockPrice
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ContextException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<StockPrice> Delete(int key)
        {
            var transaction = _stockPriceContext.Database.BeginTransaction();
            try
            {
                if (_stockPriceContext.StockPrices == null)
                    throw new ContextException(ResponseMessage.Messages[10]);
                var stockPrice = (await Get(key)) ?? throw new NullReferenceException(ResponseMessage.Messages[11]);
                _stockPriceContext.StockPrices.Remove(stockPrice);
                await _stockPriceContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return stockPrice;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message,ex);
            }
        }
        #endregion

        #region Repo Method for Get StockPrice
        /// <summary>
        /// Repo Method for Get StockPrice
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ContextException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<StockPrice> Get(int key)
        {
            try
            {
                if (_stockPriceContext.StockPrices == null)
                    throw new ContextException(ResponseMessage.Messages[10]);
                var stockPrice = ((await _stockPriceContext.StockPrices.Include(s => s.StockTransactions).
                                                                        FirstOrDefaultAsync(s => s.Id == key))) 
                                                                        ?? throw new NullReferenceException(ResponseMessage.Messages[11]);
                return stockPrice;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message,ex);
            } 
        }
        #endregion

        #region  Repo Method for Get All StockPrices
        /// <summary>
        /// Repo Method for GetAll StockPrices
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ContextException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<ICollection<StockPrice>> GetAll()
        {
            try
            {
                if (_stockPriceContext.StockPrices == null)
                    throw new ContextException(ResponseMessage.Messages[10]);

                var stockPrices = await _stockPriceContext.StockPrices
                    .Include(s => s.StockTransactions)
                    .ToListAsync() ?? throw new NullReferenceException(ResponseMessage.Messages[7]);

                foreach (var stock in stockPrices)
                {
                    stock.StockTransactions = stock.StockTransactions?.OrderBy(sp => sp.Date).ToList();
                }

                return stockPrices;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region  Repo Method for Update StockPrice
        /// <summary>
        /// Repo Method for Update StockPrice
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ContextException"></exception>
        /// <exception cref="UpdateValueException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<StockPrice> Update(StockPrice item)
        {
            var transaction = _stockPriceContext.Database.BeginTransaction();
            try
            {
                if (_stockPriceContext.StockPrices == null)
                    throw new ContextException(ResponseMessage.Messages[10]);
                _ = _stockPriceContext.StockPrices.Update(item) ?? throw new UpdateObjectException(ResponseMessage.Messages[13]);
                await _stockPriceContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return item;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message,ex);
            }
        }
        #endregion
    }
}
