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
    internal class UserProfileConfig : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("UserProfiles");

            builder.HasKey(up => up.UserProfileId);

            // BasicInfo as an owned entity (BasicInfo will be part of UserProfiles table)
            builder.OwnsOne(up => up.BasicInfo, basicInfo =>
            {
                
            });
        }
    }
}
