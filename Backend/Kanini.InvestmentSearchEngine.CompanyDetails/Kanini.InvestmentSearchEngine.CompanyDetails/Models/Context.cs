using Microsoft.EntityFrameworkCore;

namespace Kanini.InvestmentSearchEngine.CompanyDetails.Models
{
        public class Context : DbContext
        {
            public Context()
            {
                
            }
            public Context(DbContextOptions Options) : base(Options)
            {

            }
        public virtual DbSet<CompanyDetail> CompanyDetailsTable { get; set; } = null!;
          
        }
 }

