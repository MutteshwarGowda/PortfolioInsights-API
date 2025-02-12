//using IwMetrics.Domain.Aggregates.PortfolioAssets;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace IwMetrics.DAL.Configuration
//{
//    internal class PortfolioConfig : IEntityTypeConfiguration<Portfolio>
//    {
//        public void Configure(EntityTypeBuilder<Portfolio> builder)
//        {
//            // Table Name
//            builder.ToTable("Portfolios");

//            // Primary Key
//            builder.HasKey(p => p.PortfolioId);

//            // Properties
//            builder.Property(p => p.DateModified)
//                   .IsRequired(false);

//            // Foreign Key Configuration for PortfolioManager
//            builder.HasOne(p => p.UserProfile)        // Portfolio has one PortfolioManager
//                   .WithMany()                   // A PortfolioManager can have many Portfolios
//                   .HasForeignKey(p => p.UserProfileId)  // Foreign key property in Portfolio
//                   .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete for PortfolioManager

//            // Relationship with Assets (existing)
//            builder.HasMany(p => p.Assets)
//                   .WithOne(a => a.Portfolio)
//                   .HasForeignKey(a => a.PortfolioId)
//                   .OnDelete(DeleteBehavior.Cascade);   // Cascade delete for related assets

//            // Indexes
//            builder.HasIndex(p => p.Name).IsUnique(); // Ensure portfolio names are unique
//            builder.HasIndex(p => p.CreatedAt);       // Index for CreatedAt for faster queries
//        }
//    }
//}
