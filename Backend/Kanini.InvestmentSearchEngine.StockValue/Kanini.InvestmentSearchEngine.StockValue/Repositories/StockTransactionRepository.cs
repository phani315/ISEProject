using Kanini.InvestmentSearchEngine.StockValue.Models.Context;
using Kanini.InvestmentSearchEngine.StockValue.Models;
using Microsoft.EntityFrameworkCore;
using Kanini.InvestmentSearchEngine.StockValue.Interfaces;
using System.Diagnostics.CodeAnalysis;
using Kanini.InvestmentSearchEngine.StockValue.Utilities.Exceptions;
using Kanini.InvestmentSearchEngine.StockValue.Utilities.Message;

namespace Kanini.InvestmentSearchEngine.StockValue.Repositories
{
    [ExcludeFromCodeCoverage]
    public class StockTransactionRepository:IRepository<int,StockTransaction>
    {
        #region Private field
        private readonly StockPriceContext _stockPriceContext;
        private readonly ILogger<StockTransactionRepository> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="stockPriceContext"></param>
        public StockTransactionRepository(StockPriceContext stockPriceContext, ILogger<StockTransactionRepository> logger)
        {
            _stockPriceContext = stockPriceContext;
            _logger = logger;
        }
        #endregion

        #region Repo Method for Add StockTransaction
        /// <summary>
        /// Repo Method for Add StockTransaction
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ContextException"></exception>
        /// <exception cref="AddObjectException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<StockTransaction> Add(StockTransaction item)
        {
            var transaction = _stockPriceContext.Database.BeginTransaction();
            try
            {
                if (_stockPriceContext.StockTransactions == null)
                    throw new ContextException(ResponseMessage.Messages[10]);
                _ = (await _stockPriceContext.StockTransactions.AddAsync(item)) ?? throw new AddObjectException(ResponseMessage.Messages[12] + item.Id);
                await _stockPriceContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return item;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region Repo Method for Delete StockTransaction
        /// <summary>
        /// Repo Method for Delete StockTransaction
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ContextException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<StockTransaction> Delete(int key)
        {
            var transaction = _stockPriceContext.Database.BeginTransaction();
            try
            {
                if (_stockPriceContext.StockTransactions == null)
                    throw new ContextException(ResponseMessage.Messages[10]);
                var stockTransaction = (await Get(key)) ?? throw new NullReferenceException(ResponseMessage.Messages[11]);
                _stockPriceContext.StockTransactions.Remove(stockTransaction);
                await _stockPriceContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return stockTransaction;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region Repo Method for Get StockTransaction
        /// <summary>
        /// Repo Method for Get StockTransaction
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ContextException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<StockTransaction> Get(int key)
        {
            try
            {
                if (_stockPriceContext.StockTransactions == null)
                    throw new ContextException(ResponseMessage.Messages[10]);
                var stockTransaction = (await _stockPriceContext.StockTransactions.FirstOrDefaultAsync(s => s.Id == key)) ?? throw new NullReferenceException(ResponseMessage.Messages[11]);
                return stockTransaction;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region Repo Method for Get All StockTransaction
        /// <summary>
        /// Repo Method for GetAll StockTransaction
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ContextException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<ICollection<StockTransaction>> GetAll()
        {
            try
            {
                if (_stockPriceContext.StockTransactions == null)
                    throw new ContextException(ResponseMessage.Messages[10]);
                var stockTransactions = (await _stockPriceContext.StockTransactions.ToListAsync()) ?? throw new NullReferenceException(ResponseMessage.Messages[16]);
                return stockTransactions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region Repo Method for Update StockTransaction
        /// <summary>
        /// Repo Method for Update StockTransaction
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ContextException"></exception>
        /// <exception cref="UpdateValueException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<StockTransaction> Update(StockTransaction item)
        {
            var transaction = _stockPriceContext.Database.BeginTransaction();
            try
            {
                if (_stockPriceContext.StockTransactions == null)
                    throw new ContextException(ResponseMessage.Messages[14]);
                _ = _stockPriceContext.StockTransactions.Update(item) ?? throw new UpdateObjectException(ResponseMessage.Messages[15]);
                await _stockPriceContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return item;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion
    }
}
