using IwMetrics.Domain.Aggregates.UserProfileAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwMetrics.Infrastructure.Configuration
{
    internal class BasicInfoConfig : IEntityTypeConfiguration<BasicInfo>
    {
        public void Configure(EntityTypeBuilder<BasicInfo> builder)
        {
            builder.HasKey();
        }
    }
}
