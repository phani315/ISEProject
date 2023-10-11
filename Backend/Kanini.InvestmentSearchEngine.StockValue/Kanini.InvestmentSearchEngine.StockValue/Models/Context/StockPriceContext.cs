using Microsoft.EntityFrameworkCore;
using Kanini.InvestmentSearchEngine.StockValue.Models;
using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.StockValue.Models.Context
{
    [ExcludeFromCodeCoverage]
    public class StockPriceContext : DbContext
    {
        #region Properties
        public StockPriceContext(DbContextOptions<StockPriceContext> options) : base(options) { }
        public DbSet<StockPrice>? StockPrices { get; set; }
        public DbSet<StockTransaction>? StockTransactions { get; set; }
        #endregion

        #region On model creation method
        /// <summary>
        /// Model creation method
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<StockPrice>()
                .HasIndex(s => s.CompanyId)
                .IsUnique();
        }
        #endregion
    }
}
