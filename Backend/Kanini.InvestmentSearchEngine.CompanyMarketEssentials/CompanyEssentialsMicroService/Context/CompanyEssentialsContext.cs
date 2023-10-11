using KaniniInvestmentSearchEngineCompanyMarketEssentials.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace KaniniInvestmentSearchEngineCompanyMarketEssentials.Context
{
    [ExcludeFromCodeCoverage]
    public class CompanyEssentialsContext : DbContext
    {
        #region Constructor
        public CompanyEssentialsContext(DbContextOptions<CompanyEssentialsContext> options) : base(options)
        {

        }
        #endregion

        #region DbSet
        public DbSet<CompanyEssentials> CompanyEssentials { get; set; } = null!;
        #endregion
    }
}
