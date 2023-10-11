using Kanini.InvestmentSearchEngine.CompanyDetails.Exceptions;
using Kanini.InvestmentSearchEngine.CompanyDetails.Interfaces;
using Kanini.InvestmentSearchEngine.CompanyDetails.Models;
namespace Kanini.InvestmentSearchEngine.CompanyDetails.Services
{
    public class CompanyDetailsServices : ICompanyDetailsServices
    {
        private readonly IRepo<int, CompanyDetail> _companyDetailsRepo;

        public CompanyDetailsServices(IRepo<int,CompanyDetail> companyDetailsRepo)
        {
            _companyDetailsRepo = companyDetailsRepo;
        }
        public async Task<CompanyDetail?> AddCompanyDetails(CompanyDetail companyDetails)
        {
            var myCompanyDetails = await _companyDetailsRepo.Add(companyDetails) ?? throw new DataNotFoundException("Unable to add");
            return myCompanyDetails;
        }

        public async  Task<CompanyDetail?> DeleteCompanyDetails(int companyId)
        {
                return await _companyDetailsRepo.Delete(companyId) ?? throw new DataNotFoundException("Unable to delete");
        }

        public async Task<ICollection<CompanyDetail>?> GetAllCompanyDetails()
        {
                return await _companyDetailsRepo.GetAll() ?? throw new DataNotFoundException("Unable to fetch data");
        }

        public async Task<CompanyDetail?> GetCompanyDetailsById(int companyId)
        {
            return await _companyDetailsRepo.Get(companyId) ?? throw new DataNotFoundException("Unable to fetch data");
        }

        public async  Task<CompanyDetail?> UpdateCompanyDetails(CompanyDetail companyDetails)
        {
            return await _companyDetailsRepo.Update(companyDetails) ?? throw new DataNotFoundException("Unable to update data");
        }


        public async Task<List<CompanyDetail>> SearchCompanies(string searchTerm)
        {
            var companies = await _companyDetailsRepo.GetAll();

            return companies
                .Where(c => c.CompanyName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true)
                .ToList();
        }
    }
}
