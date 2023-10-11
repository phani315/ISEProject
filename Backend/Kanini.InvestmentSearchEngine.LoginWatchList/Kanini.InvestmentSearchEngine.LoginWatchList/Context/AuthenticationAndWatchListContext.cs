using Kanini.InvestmentSearchEngine.LoginWatchList.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.LoginWatchList.Context
{
    [ExcludeFromCodeCoverage]
    public class AuthenticationAndWatchListContext : DbContext
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the AuthenticationAndWatchListContext class with the specified options.
        /// </summary>
        /// <param name="options">The DbContext options.</param>
        public AuthenticationAndWatchListContext(DbContextOptions<AuthenticationAndWatchListContext> options) : base(options)
        {
        }

        #endregion

        #region DbSets

        /// <summary>
        /// Gets or sets the DbSet for UserInfos.
        /// </summary>
        public DbSet<UserInfo> UserDetails { get; set; } = null!;

        /// <summary>
        /// Gets or sets the DbSet for WatchLists.
        /// </summary>
        public DbSet<WatchList> WatchLists { get; set; } = null!;

        /// <summary>
        /// Gets or sets the DbSet for Users.
        /// </summary>
        public DbSet<User> Users { get; set; } = null!;

        #endregion

        #region Model Configuration

        /// <summary>
        /// Configures the database model using the Fluent API.
        /// </summary>
        /// <param name="modelBuilder">The model builder instance.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the UserInfo entity
            modelBuilder.Entity<UserInfo>().Property(ud => ud.UserId).ValueGeneratedNever();

            // Create a unique index on the EmailId property of the User entity
            modelBuilder.Entity<User>().HasIndex(u => u.EmailId).IsUnique(true);
        }

        #endregion
    }
}
