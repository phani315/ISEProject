using Kanini.InvestmentSearchEngine.CompanyEssentials.CustomExceptions;
using Kanini.InvestmentSearchEngine.CompanyMarketEssentials.Messages;
using KaniniInvestmentSearchEngineCompanyMarketEssentials.Interfaces;
using KaniniInvestmentSearchEngineCompanyMarketEssentials.Models;
 
namespace KaniniInvestmentSearchEngineCompanyMarketEssentials.Services
{
    public class CompanyEssentialsServices : ICompanyEssentialsServices<int, CompanyEssentials>
    {
        #region Private Field
        private readonly IRepository<int, CompanyEssentials> _companyEssentialsRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the CompanyEssentialsServices class.
        /// </summary>
        /// <param name="companyEssentialsRepsitory">The repository for CompanyEssentials.</param>

        public CompanyEssentialsServices(IRepository<int,CompanyEssentials> companyEssentialsRepsitory)
        {
            _companyEssentialsRepository = companyEssentialsRepsitory;
        }
        #endregion

        #region Services Method for AddEssentials
        /// <summary>
        /// Adds CompanyEssentials information and calculates additional metrics like EPS, Price to Earnings, and Price to Book.
        /// </summary>
        /// <param name="item">The CompanyEssentials item to add.</param>
        public async Task<CompanyEssentials?> AddEssentials(CompanyEssentials item)
        {
            var existingCompany = (await _companyEssentialsRepository.GetAll())?.SingleOrDefault(c => c.CompanyID == item.CompanyID);
            if (existingCompany != null) throw new GroupExceptions(Messages.messages[5]);

            if (item.NoOfShares != 0)
                item.Eps = Math.Round(item.NetIncome / item.NoOfShares, 2);
            if (item.Eps != null && item.Eps != 0)
                item.PriceToEarning = Math.Round(item.Price / item.Eps.Value, 2);
            if (item.BookValue != 0)
                item.PriceToBook = Math.Round(item.Price / item.BookValue, 2);
             return await _companyEssentialsRepository.Add(item);
        }

        #endregion

        #region  Services Method for DeleteEssentials 
        /// <summary>
        /// Deletes a CompanyEssentials item by its key (CompanyID).
        /// </summary>
        /// <param name="key">The key (CompanyID) of the CompanyEssentials item to delete.</param>
        public async Task<CompanyEssentials?> DeleteEssential(int key)
        {   
           return await _companyEssentialsRepository.Delete(key);
        }
        #endregion

        #region  Services Method for GetEssentials
        /// <summary>
        /// Gets a CompanyEssentials item by its key (CompanyID).
        /// </summary>
        /// <param name="key">The key (CompanyID) of the CompanyEssentials item to retrieve.</param>
        public async Task<CompanyEssentials?> GetEssential(int key)
        {
            return await _companyEssentialsRepository.Get(key);

        }

        #endregion

        #region Services Method for GetAllEssentials
        /// <summary>
        /// Gets all CompanyEssentials items from the database.
        /// </summary>
        public async Task<ICollection<CompanyEssentials>?> GetAllEssentials()
        {
            return await _companyEssentialsRepository.GetAll();
        }
        #endregion



        #region Services Method for UpdateEssentials
        /// <summary>
        /// Updates a CompanyEssentials item in the database.
        /// </summary>
        /// <param name="item">The updated CompanyEssentials item.</param>
        public async Task<CompanyEssentials?> UpdateEssential(CompanyEssentials item)
        {
            return await _companyEssentialsRepository.Update(item);
        }

        #endregion

        #region Services Method for GetFilteredCompaniese
        /// <summary>
        /// Filters and retrieves a collection of CompanyEssentials items based on specific criteria.
        /// </summary>
        public async Task<ICollection<CompanyEssentials?>?> FilterCompanies()
        {
            var companies = await _companyEssentialsRepository.GetAll();
            if (companies == null) throw new GroupExceptions(Messages.messages[3]);
           
            var filteredCompanies = companies
                .Where(company => company.PriceToEarning < 25 && company.PriceToBook > 25)
                .GroupBy(company => company.Sector)
                .Select(group => group.OrderBy(company => company.DivYield).FirstOrDefault())
                .Take(5)
                .ToList();
            return filteredCompanies;
        }
        #endregion
        } 
}
