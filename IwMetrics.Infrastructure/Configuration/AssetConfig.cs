﻿using IwMetrics.Domain.Aggregates.PortfolioAssets;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwMetrics.Infrastructure.Configuration
{
    internal class AssetConfig : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            
            builder.ToTable("Assets");

            // Primary Key
            builder.HasKey(a => a.AssetId);

            // Proerties
            builder.Property(p => p.DateModified)
                   .IsRequired(false);

            // Asset Type Configuration
            builder.Property(a => a.Type)
                   .IsRequired()
                   .HasConversion<string>();  //store Enum as string in DB

            // Relationships
            builder.HasOne(a => a.Portfolio)
                   .WithMany(p => p.Assets)
                   .HasForeignKey(a => a.PortfolioId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
