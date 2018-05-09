using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TeduCore.Data.EF.Extensions;
using TeduCore.Data.Entities;

namespace TeduCore.Data.EF.Configurations
{
    public class ProductTagConfiguration : DbEntityConfiguration<ProductTag>
    {
        public override void Configure(EntityTypeBuilder<ProductTag> entity)
        {
            entity.Property(c => c.TagId).HasMaxLength(255).IsRequired()
            .HasColumnType("varchar(255)");
            // etc.
        }
    }
}
