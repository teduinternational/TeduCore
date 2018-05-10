using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using TeduCore.Data.EF.Configurations;
using TeduCore.Data.EF.Extensions;
using TeduCore.Data.Entities;
using TeduCore.Data.Interfaces;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Identity Config

            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("AppUserClaims").HasKey(x => x.Id);

            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("AppRoleClaims")
                .HasKey(x => x.Id);

            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("AppUserRoles")
                .HasKey(x => new { x.RoleId, x.UserId });

            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("AppUserTokens")
               .HasKey(x => new { x.UserId });

            #endregion Identity Config

            modelBuilder.AddConfiguration(new TagConfiguration());
            modelBuilder.AddConfiguration(new BlogTagConfiguration());
            modelBuilder.AddConfiguration(new ContactDetailConfiguration());

            modelBuilder.AddConfiguration(new FooterConfiguration());
            modelBuilder.AddConfiguration(new PageConfiguration());
            modelBuilder.AddConfiguration(new FooterConfiguration());

            modelBuilder.AddConfiguration(new ProductTagConfiguration());
            modelBuilder.AddConfiguration(new SystemConfigConfiguration());
            modelBuilder.AddConfiguration(new AdvertistmentPositionConfiguration());
        }

        public override int SaveChanges()
        {
            try
            {
                var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
                foreach (EntityEntry item in modified)
                {
                    if (item.Entity is IDateTracking changedOrAddedItem)
                    {
                        if (item.State == EntityState.Added)
                        {
                            changedOrAddedItem.DateCreated = DateTime.Now;
                        }
                        changedOrAddedItem.DateModified = DateTime.Now;
                    }
                }
                return base.SaveChanges();
            }
            catch (DbUpdateException entityException)
            {
                throw new ModelValidationException(entityException.Message);
            }
        }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Setting> SystemConfigs { get; set; }
        public DbSet<Function> Functions { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Announcement> Announcements { set; get; }
        public DbSet<AnnouncementUser> AnnouncementUsers { set; get; }

        public DbSet<AuditLog> AuditLogs { set; get; }

        public DbSet<Post> Bills { set; get; }
        public DbSet<BillDetail> BillDetails { set; get; }
        public DbSet<Post> Blogs { set; get; }
        public DbSet<PostTag> BlogTags { set; get; }
        public DbSet<ContactDetail> ContactDetails { set; get; }
        public DbSet<Feedback> Feedbacks { set; get; }
        public DbSet<Footer> Footers { set; get; }
        public DbSet<Page> Pages { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<ProductCategory> ProductCategories { set; get; }
        public DbSet<ProductImage> ProductImages { set; get; }
        public DbSet<ProductTag> ProductTags { set; get; }

        public DbSet<Slide> Slides { set; get; }

        public DbSet<Tag> Tags { set; get; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<AdvertistmentPage> AdvertistmentPages { get; set; }
        public DbSet<Advertistment> Advertistments { get; set; }
        public DbSet<AdvertistmentPosition> AdvertistmentPositions { get; set; }
        public DbSet<ProductWishlist> ProductWishlists { get; set; }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString("AppDbConnection");
            builder.UseSqlServer(connectionString);
            return new AppDbContext(builder.Options);
        }
    }
}