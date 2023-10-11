using Microsoft.EntityFrameworkCore;
using Kanini.InvestmentSearchEngine.CompanyDetails.Interfaces;
using Kanini.InvestmentSearchEngine.CompanyDetails.Models;
using Kanini.InvestmentSearchEngine.CompanyDetails.Exceptions;

namespace Kanini.InvestmentSearchEngine.CompanyDetails.Repositories
{
    public class CompanyDetailsRepository : IRepo<int, CompanyDetail>
    {
        private readonly Context _dbContext;

        public CompanyDetailsRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CompanyDetail?> Add(CompanyDetail item)
        {
            if (_dbContext.CompanyDetailsTable == null)
                throw new DataNotFoundException("No company details are found");
            await _dbContext.CompanyDetailsTable.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }
        public async Task<CompanyDetail?> Update(CompanyDetail item)
        {
            if (_dbContext.CompanyDetailsTable == null)
                throw new DataNotFoundException("No company details are found");
            var company = await Get(item.CompanyId) ?? throw new DataNotFoundException("Company detail is not found");
            company.CompanyName = item.CompanyName;
            company.BSE = item.BSE;
            company.NSE = item.NSE;
            company.Image = item.Image;
            _dbContext.CompanyDetailsTable.Update(company);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<CompanyDetail?> Delete(int key)
        {
            if (_dbContext.CompanyDetailsTable == null)
                throw new DataNotFoundException("company details are not found");
            var item = await Get(key) ?? throw new DataNotFoundException("company detail is not found");
            _dbContext.CompanyDetailsTable.Remove(item);
            await _dbContext.SaveChangesAsync();
            return item;
        } 
        public async Task<CompanyDetail?> Get(int key)
        {
            var item = await _dbContext.CompanyDetailsTable.SingleOrDefaultAsync(c => c.CompanyId == key) ?? throw new DataNotFoundException("company detail is not found");
            return item;
        }

        public async Task<ICollection<CompanyDetail>?> GetAll()
        {
            var items = await _dbContext.CompanyDetailsTable.ToListAsync() ?? throw new DataNotFoundException("No company details found");
            return items;
        }
    } 
 }

