using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TeduCore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using TeduCore.Data.EF.Extensions;

namespace TeduCore.Data.EF.Configurations
{
    public class TagConfiguration : DbEntityConfiguration<Tag>
    {
        public override void Configure(EntityTypeBuilder<Tag> entity)
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).HasMaxLength(255).IsRequired()
            .HasColumnType("varchar(255)");
            // etc.
        }
    }
}
