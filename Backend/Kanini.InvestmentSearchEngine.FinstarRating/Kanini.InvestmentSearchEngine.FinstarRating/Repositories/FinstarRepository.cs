using Kanini.InvestmentSearchEngine.FinstarRating.Interfaces;
using Kanini.InvestmentSearchEngine.FinstarRating.Models;
using Kanini.InvestmentSearchEngine.FinstarRating.Context;
using Microsoft.EntityFrameworkCore;
using Kanini.InvestmentSearchEngine.FinstarRating.CustomExceptions;
using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.FinstarRating.Repositories
{
    [ExcludeFromCodeCoverage]
    public class FinstarRepository : IRepo<int, Finstar>
    {
        #region Private Variable
        private FinstarContext _context;
        #endregion

        #region Constructor
        public FinstarRepository(FinstarContext context_)
        {
            _context = context_;
        }
        #endregion

        #region Add
        /// <summary>
        /// Adds a new Finstar item to the database.
        /// </summary>
        /// <param name="item">The Finstar item to be added.</param>

        public async Task<Finstar?> Add(Finstar item)
        {
            if (_context.Finstar == null)
            {
                throw new ContextNullException("The Finstar Context is null");
            }
            var finstar = _context.Finstar.Add(item) ?? throw new AddObjectException("Adding finstar rating failed ");
            await _context.SaveChangesAsync();
            return item;
        }
        #endregion


        #region Get
        /// <summary>
        /// Gets a Finstar item by its unique key (CompanyId).
        /// </summary>
        /// <param name="key">The key (CompanyId) of the Finstar item to retrieve.</param>
        public async Task<Finstar?> Get(int key)
        {
            if (_context.Finstar == null) throw new ContextNullException("The Finstar Context is null");
            var finstar = await _context.Finstar.Include(finstar => finstar.Financial).Include(finstar => finstar.OwnerShip).Include(finstar => finstar.Valuation).Include(finstar => finstar.Efficiency).FirstOrDefaultAsync(s => s.CompanyId == key);
            return finstar;
        }
        #endregion


        #region Delete
        /// <summary>
        /// Deletes a Finstar item by its unique key (CompanyId).
        /// </summary>
        /// <param name="key">The key (CompanyId) of the Finstar item to delete.</param>
        public async Task<Finstar?> Delete(int key)
        {
            var finstar = await Get(key);
            if (finstar != null)
            {
                if (_context.Finstar == null) throw new ContextNullException("The Finstar Context is null");
                _context.Finstar.Remove(finstar);
                await _context.SaveChangesAsync();
            }
            else throw new Exception("There is no data available with the given CompanyId");
            return finstar;
        }
        #endregion


        #region GetAll
        /// <summary>
        /// Gets all Finstar items from the database.
        /// </summary>
        public async Task<ICollection<Finstar>?> GetAll()
        {
            if (_context.Finstar == null) throw new ContextNullException("The Finstar Context is null");
            var finstars = await _context.Finstar.Include(f => f.Efficiency).Include(f => f.Valuation).Include(f => f.OwnerShip).Include(f => f.Financial).ToListAsync();
            if (finstars.Count == 0)
            {
                throw new Exception("There are no records in the Database to fetch");
            }
            return finstars;
        }
        #endregion


        #region Update
        /// <summary>
        /// Updates a Finstar item in the database.
        /// </summary>
        /// <param name="updatedRating">The updated Finstar item.</param>

        public async Task<Finstar?> Update(Finstar updatedRating)
        {
            if (updatedRating == null || updatedRating == null) throw new NullReferenceException("The finstar item is null");
            var existingRating = await Get(updatedRating.RatingId);
            if (existingRating != null)
            {
                existingRating.TotalRating = updatedRating.TotalRating;
                existingRating.TotalReviewCount = updatedRating.TotalReviewCount;
                existingRating.Financial = updatedRating.Financial;
                existingRating.OwnerShip = updatedRating.OwnerShip;
                existingRating.Efficiency = updatedRating.Efficiency;
                existingRating.Valuation = updatedRating.Valuation;
                await _context.SaveChangesAsync();
                return updatedRating;
            }

            throw new UpdateFailedException("Update operation failed .");
        }
        #endregion
    }
}
