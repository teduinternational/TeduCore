using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TeduCore.Data.EF.Extensions;
using TeduCore.Data.Entities;

namespace TeduCore.Data.EF.Configurations
{
    public class ContactDetailConfiguration : DbEntityConfiguration<ContactDetail>
    {
        public override void Configure(EntityTypeBuilder<ContactDetail> entity)
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).HasMaxLength(255).IsRequired();
            // etc.
        }
    }
}
