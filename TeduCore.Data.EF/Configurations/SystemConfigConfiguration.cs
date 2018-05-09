using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TeduCore.Data.EF.Extensions;
using TeduCore.Data.Entities;

namespace TeduCore.Data.EF.Configurations
{
    class SystemConfigConfiguration : DbEntityConfiguration<SystemConfig>
    {
        public override void Configure(EntityTypeBuilder<SystemConfig> entity)
        {
            entity.Property(c => c.Id).HasMaxLength(255).IsRequired();
            // etc.
        }
    }
}
