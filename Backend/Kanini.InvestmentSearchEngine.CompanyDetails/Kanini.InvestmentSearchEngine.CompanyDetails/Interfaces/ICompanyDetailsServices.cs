using Kanini.InvestmentSearchEngine.CompanyDetails.Models;

namespace Kanini.InvestmentSearchEngine.CompanyDetails.Interfaces
{
    public interface ICompanyDetailsServices
    {
        public Task<CompanyDetail?> AddCompanyDetails(CompanyDetail companyDetails);
        public Task<CompanyDetail?> UpdateCompanyDetails(CompanyDetail companyDetails);
        public Task<CompanyDetail?> DeleteCompanyDetails(int companyId);  
        public Task<CompanyDetail?> GetCompanyDetailsById(int companyId); 
        public Task<ICollection<CompanyDetail>?> GetAllCompanyDetails();
        public Task<List<CompanyDetail>> SearchCompanies(string searchTerm);

    }
}
