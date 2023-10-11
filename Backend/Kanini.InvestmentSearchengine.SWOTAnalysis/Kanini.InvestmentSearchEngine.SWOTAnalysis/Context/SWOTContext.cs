using Kanini.InvestmentSearchEngine.SWOTAnalysis.Models;
using Microsoft.EntityFrameworkCore;

namespace Kanini.InvestmentSearchEngine.SWOTAnalysis.Context
{
    public class SWOTContext : DbContext
    {
        #region Constructors
        public SWOTContext(DbContextOptions options) : base(options)
        {

        }
        #endregion
        #region DbSet Properties
        public DbSet<Strength>? Strengths { get; set; }
        public DbSet<Weakness>? Weaknesses { get; set; }
        public DbSet<Oppurtunity>? Oppurtunities { get; set; }
        public DbSet<Threat>? Threats { get; set; }
        public DbSet<SWOT>? SWOT { get; set; }
        #endregion

        #region Model Configuration

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Strength>().Property(s => s.StrengthId).ValueGeneratedNever();
            modelBuilder.Entity<Weakness>().Property(s => s.WeaknessId).ValueGeneratedNever();
            modelBuilder.Entity<Oppurtunity>().Property(s => s.OppurtunityId).ValueGeneratedNever();
            modelBuilder.Entity<Threat>().Property(s => s.ThreatId).ValueGeneratedNever();
        }
        #endregion

    }

}
