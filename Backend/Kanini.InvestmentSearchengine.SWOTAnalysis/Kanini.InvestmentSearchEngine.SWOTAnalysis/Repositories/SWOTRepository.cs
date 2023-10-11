using Kanini.InvestmentSearchEngine.SWOTAnalysis.Context;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Interfaces;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Models;
using Microsoft.EntityFrameworkCore;


namespace Kanini.InvestmentSearchEngine.SWOTAnalysis.Repositories
{
    public class SWOTRepository : IRepository<int, SWOT>
    {
        #region Private Field
        private readonly SWOTContext _context;

        #endregion

        #region Constructors
        /// <summary>
        /// Initialize a new instance of the SWOTRepository class with the provided SWOTContext.
        /// </summary>
        /// <param name="SWOTContext">The SWOTContext is used for database operations related to SWOT analysis..</param>
        public SWOTRepository(SWOTContext context)
        {
            _context = context;
           
        }
        #endregion
        #region Repository Method for Adding SWOT 
        /// <summary>
        ///  Add a new SWOT analysis item to the database.
        /// </summary>
        /// <param name="item">The item parameter represents the SWOT object to be added. </param>
        /// <returns>If successful, the added item is returned.</returns>
        /// <exception cref="NullReferenceException">Thrown when the SWOT context is null.</exception>
        public async Task<SWOT?> Add(SWOT item)
        {
            if (_context.SWOT == null)
                throw new NullReferenceException("The SWOT context is null");
            await _context.SWOT.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }
        #endregion

        #region Repository Method for deleting SWOT
        /// <summary>
        ///  Delete a SWOT analysis item from the database based on the provided key. 
        /// </summary>
        /// <param name="key">It first checks if the necessary context and related data (strengths, weaknesses, opportunities, threats) are available. </param>
        /// <returns> If found and deletable, the deleted item is returned.</returns>
        /// <exception cref="NullReferenceException">Thrown when the SWOT context is null.</exception>
        public async Task<SWOT?> Delete(int key)
        {

            if (_context.SWOT != null && _context.Strengths != null && _context.Weaknesses != null && _context.Oppurtunities != null && _context.Threats != null)
            {
                var swot = await Get(key);
                if (swot != null && swot.Strength != null && swot.Weakness != null && swot.Oppurtunity != null && swot.Threat != null)
                {
                    _context.SWOT.Remove(swot);
                    await _context.SaveChangesAsync();
                    return swot;
                }
            }
            throw new NullReferenceException("No data found");
        }
        #endregion

        #region Repository method to Get SWOT Details
        /// <summary>
        ///  Retrieve a SWOT analysis item from the database based on the provided key. 
        /// </summary>
        /// <param name="key">It fetches the SWOT object along with its related strengths, weaknesses, opportunities, and threats.</param>
        /// <returns>If found, the retrieved item is returned.</returns>
        /// <exception cref="NullReferenceException">Thrown when the SWOT context is null.</exception>

        public async Task<SWOT?> Get(int key)
        {

            if (_context.SWOT != null)
            {
                var swot = await _context.SWOT.Include(swot => swot.Strength).Include(swot => swot.Weakness).Include(swot => swot.Oppurtunity).Include(swot => swot.Threat).FirstOrDefaultAsync(s => s.SwotId == key);
                return swot;
            }
            throw new NullReferenceException("No data found");

        }
        #endregion

        #region Repository method to GetAll SWOT details
        /// <summary>
        /// Retrieve all SWOT analysis items from the database. his method fetches a collection of SWOT objects along with their related strengths, weaknesses, opportunities, and threats.
        /// </summary>
        /// <returns>If found, the collection is returned</returns>
        /// <exception cref="NullReferenceException">otherwise, a NullReferenceException is thrown.</exception>
        public async Task<ICollection<SWOT>?> GetAll()
        {

            if (_context.SWOT != null)
            {
                var swot = await _context.SWOT.Include(swot => swot.Strength).Include(swot => swot.Weakness).Include(swot => swot.Oppurtunity).Include(swot => swot.Threat).ToListAsync();
                return swot ?? throw new NullReferenceException("No data found");

            }
            throw new NullReferenceException("No data found");
        }
        #endregion

        #region Repository method to update SWOT
        /// <summary>
        /// Update an existing SWOT analysis item in the database.  It first fetches the existing SWOT object based on the provided item's SwotId.
        /// </summary>
        /// <param name="item">Then, it updates the object's properties with the values from the provided item. </param>
        /// <returns>If the update is successful, the updated item is returned</returns>
        /// <exception cref="NullReferenceException"> Otherwise, a NullReferenceException is thrown.</exception>
        public async Task<SWOT?> Update(SWOT item)
        {

            var swot = await Get(item.SwotId);
            if (swot != null)
            {
                swot.SwotId = item.SwotId;
                swot.CompanyID = item.CompanyID;
                swot.Strength = item.Strength;
                swot.Weakness = item.Weakness;
                swot.Oppurtunity = item.Oppurtunity;
                swot.Threat = item.Threat;
                await _context.SaveChangesAsync();
                return swot ?? throw new NullReferenceException("No data found");
            }
            throw new NullReferenceException("No data found");
        }
        #endregion

    }

}