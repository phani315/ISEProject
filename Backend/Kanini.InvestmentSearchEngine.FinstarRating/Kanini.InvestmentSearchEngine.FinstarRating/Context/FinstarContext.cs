using Kanini.InvestmentSearchEngine.FinstarRating.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.FinstarRating.Context
{
    [ExcludeFromCodeCoverage]

    public class FinstarContext : DbContext
    {
        public FinstarContext(DbContextOptions options) : base(options)
        {


        }

        public DbSet<Finstar>? Finstar { get; set; }

        public DbSet<Financial>? Financial { get; set; }
        public DbSet<Efficiency>? Efficiency { get; set; }
        public DbSet<OwnerShip>? OwnerShip { get; set; }
        public DbSet<Valuation>? Valuation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Financial>().Property(f => f.FinancialID).ValueGeneratedNever();
            modelBuilder.Entity<Valuation>().Property(v => v.ValuationID).ValueGeneratedNever();
            modelBuilder.Entity<Efficiency>().Property(e => e.EfficiencyId).ValueGeneratedNever();
            modelBuilder.Entity<OwnerShip>().Property(o => o.OwnerShipID).ValueGeneratedNever();
        }


    }

}
