using Kanini.InvestmentSearchEngine.CompanyEssentials.CustomExceptions;
using Kanini.InvestmentSearchEngine.CompanyMarketEssentials.Messages;
using KaniniInvestmentSearchEngineCompanyMarketEssentials.Context;
using KaniniInvestmentSearchEngineCompanyMarketEssentials.Interfaces;
using KaniniInvestmentSearchEngineCompanyMarketEssentials.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace KaniniInvestmentSearchEngineCompanyMarketEssentials.Repositary
{
    [ExcludeFromCodeCoverage] 
    public class CompanyEssentialsRepository : IRepository<int, CompanyEssentials>
    {
        #region Private Field
        private readonly CompanyEssentialsContext _companyEssentialsContext;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the CompanyEssentialsRepository class.
        /// </summary>
        /// <param name="companyEssentialsContext">The CompanyEssentialsContext to use for database operations.</param>
        public CompanyEssentialsRepository(CompanyEssentialsContext companyEssentialsContext)
        {
            _companyEssentialsContext = companyEssentialsContext;  
        }
        #endregion

        #region Repo Method for Add Essentials 
        /// <summary>
        /// Adds a CompanyEssentials item to the database.
        /// </summary>
        /// <param name="item">The CompanyEssentials item to be added.</param>
        public async Task<CompanyEssentials?> Add(CompanyEssentials item)
        {
            if (_companyEssentialsContext.CompanyEssentials == null) 
                throw new NullReferenceException(Messages.messages[1]);
            var essentials=_companyEssentialsContext.CompanyEssentials.Add(item);
            return essentials != null &&  await _companyEssentialsContext.SaveChangesAsync()>0?item: 
                   throw new GroupExceptions(Messages.messages[4]);
        }
        #endregion

        #region Repo Method for Delete Essentials
        /// <summary>
        /// Deletes a CompanyEssentials item from the database by its key (EssenID).
        /// </summary>
        /// <param name="key">The key (EssenID) of the CompanyEssentials item to delete.</param>
        public async Task<CompanyEssentials?> Delete(int key)
        {
            if(_companyEssentialsContext.CompanyEssentials == null) 
                throw new NullReferenceException("The Company Essentials context is null");
            var Essentials = await Get(key);
            if (Essentials != null)
            {
                 _companyEssentialsContext.CompanyEssentials.Remove(Essentials);
                await _companyEssentialsContext.SaveChangesAsync();
                return Essentials;
            }
            throw new GroupExceptions(Messages.messages[2]);
        }
        #endregion

        #region Repo Method for Get Essentials
        /// <summary>
        /// Gets a CompanyEssentials item from the database by its key (EssenID).
        /// </summary>
        /// <param name="key">The key (EssenID) of the CompanyEssentials item to retrieve.</param>
        public async Task<CompanyEssentials?> Get(int key)
        {
            if (_companyEssentialsContext.CompanyEssentials == null) 
                throw new NullReferenceException("The Company Essentials context is null");
            var essentials = await _companyEssentialsContext.CompanyEssentials.ToListAsync();
            if (essentials.Count == 0) throw new NullReferenceException("No records are availbale in the context");
            return await _companyEssentialsContext.CompanyEssentials.SingleOrDefaultAsync(h => h.CompanyID == key);
         }
        #endregion

        #region  Repo Method for GetAll Essentials
        /// <summary>
        /// Gets all CompanyEssentials items from the database.
        /// </summary>
        public async Task<ICollection<CompanyEssentials>?> GetAll()
        {
            if(_companyEssentialsContext.CompanyEssentials == null) throw new NullReferenceException();
            var companies = await _companyEssentialsContext.CompanyEssentials.ToListAsync();
            if (companies.Count == 0) throw new NullReferenceException("The Company Essentials context is null");
            return companies;
        }
        #endregion

        #region Repo Method for Update Essentials
        /// <summary>
        /// Updates a CompanyEssentials item in the database.
        /// </summary>
        /// <param name="item">The updated CompanyEssentials item.</param>
        public async Task<CompanyEssentials?> Update(CompanyEssentials item)
        {
            if(_companyEssentialsContext.CompanyEssentials == null) throw new NullReferenceException();
            var essentials = _companyEssentialsContext.CompanyEssentials.SingleOrDefault(h => h.EssenID == item.EssenID);
            if (essentials == null)
            {
                throw new NullReferenceException("The Company Essentials context is null");
            }
                essentials.EnterpriceValue = item.EnterpriceValue;
                essentials.MarketCap = item.MarketCap;
                essentials.NoOfShares = item.NoOfShares;
                essentials.Cash = item.Cash;
                essentials.DivYield = item.DivYield;
                essentials.PromoterHolding = item.PromoterHolding;
                essentials.Sector = item.Sector;
                await _companyEssentialsContext.SaveChangesAsync();
                return essentials;
         }
        #endregion 
    }
}
