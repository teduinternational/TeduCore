using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeduCore.Data.EF.Extensions;
using TeduCore.Data.Entities;

namespace TeduCore.Data.EF.Configurations
{
    public class BlogTagConfiguration : DbEntityConfiguration<BlogTag>
    {
        public override void Configure(EntityTypeBuilder<BlogTag> entity)
        {
            entity.Property(c => c.TagId).HasMaxLength(255).IsRequired()
            .HasColumnType("varchar(255)");
            // etc.
        }
    }
}