using IwMetrics.DAL.Configuration;
using IwMetrics.Domain.Aggregates.PortfolioAssets;
using IwMetrics.Domain.Aggregates.UserProfileAggregate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwMetrics.DAL
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
                
        }

        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfiguration(new AssetConfig());
            modelBuilder.ApplyConfiguration(new PortfolioConfig());
            modelBuilder.ApplyConfiguration(new IdentityUserLoginConfig());
            modelBuilder.ApplyConfiguration(new IdentityuserRoleConfig());
            modelBuilder.ApplyConfiguration(new IdentityUserTokenConfig());
            modelBuilder.ApplyConfiguration(new UserProfileConfig());
        }

    }

}

