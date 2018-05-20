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
using TeduCore.Data.Entities.ECommerce;
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

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims").HasKey(x => x.Id);

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims")
                .HasKey(x => x.Id);

            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles")
                .HasKey(x => new { x.RoleId, x.UserId });

            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens")
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

        #region ECommerce
        public DbSet<Address> Addresses { set; get; }
        public DbSet<Entities.ECommerce.Attribute> Attributes { set; get; }
        public DbSet<AttributeDescription> AttributeDescriptions { set; get; }

        public DbSet<AttributeGroup> AttributeGroups { set; get; }
        public DbSet<AttributeGroupDescription> AttributeGroupDescriptions { set; get; }

        public DbSet<Coupon> Coupons { set; get; }
        public DbSet<CouponCategory> CouponCategories { set; get; }
        public DbSet<CouponDescription> CouponDescriptions { set; get; }
        public DbSet<CouponHistory> CouponHistories { set; get; }
        public DbSet<CouponProduct> CouponProducts { set; get; }
        public DbSet<Currency> Currencies { set; get; }
        public DbSet<Customer> Customers { set; get; }
        public DbSet<CustomerActivity> CustomerActivities { set; get; }
        public DbSet<CustomerLogin> CustomerLogins { set; get; }
        public DbSet<CustomerOnline> CustomerOnlines { set; get; }
        public DbSet<CustomerReward> CustomerRewards { set; get; }
        public DbSet<CustomerSearch> CustomerSearches { set; get; }
        public DbSet<CustomerTransaction> CustomerTransactions { set; get; }
        public DbSet<CustomerWishlist> CustomerWishlists { set; get; }
        public DbSet<CustomField> CustomFields { set; get; }
        public DbSet<CustomFieldCustomerGroup> CustomFieldCustomerGroups { set; get; }
        public DbSet<CustomFieldDescription> CustomFieldDescriptions { set; get; }
        public DbSet<CustomFieldValue> CustomFieldValues { set; get; }
        public DbSet<CustomFieldValueDescription> CustomFieldValueDescriptions { set; get; }
        public DbSet<Filter> Filters { set; get; }
        public DbSet<FilterDescription> FilterDescriptions { set; get; }
        public DbSet<FilterGroup> FilterGroups { set; get; }
        public DbSet<FilterGroupDescription> FilterGroupDescriptions { set; get; }
        public DbSet<LengthClass> LengthClasses { set; get; }
        public DbSet<LengthClassDescription> LengthClassDescriptions { set; get; }
        public DbSet<Option> Options { set; get; }
        public DbSet<OptionDescription> OptionDescriptions { set; get; }
        public DbSet<OptionValue> OptionValues { set; get; }
        public DbSet<OptionValueDescription> OptionValueDescriptions { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderHistory> OrderHistories { set; get; }
        public DbSet<OrderOption> OrderOptions { set; get; }
        public DbSet<OrderProduct> OrderProducts { set; get; }
        public DbSet<OrderRecurring> OrderRecurrings { set; get; }
        public DbSet<OrderRecurringTransaction> OrderRecurringTransactions { set; get; }
        public DbSet<OrderShipment> OrderShipments { set; get; }
        public DbSet<OrderStatus> OrderStatuses { set; get; }
        public DbSet<OrderTotal> OrderTotals { set; get; }
        public DbSet<OrderVoucher> OrderVouchers { set; get; }

        public DbSet<Product> Products { set; get; }
        public DbSet<ProductAttribute> ProductAttributes { set; get; }
        public DbSet<ProductCategory> ProductCategories { set; get; }
        public DbSet<ProductCategoryDescription> ProductCategorieDescriptions { set; get; }
        public DbSet<ProductCategoryFilter> ProductCategorieFilters { set; get; }
        public DbSet<ProductCategoryPath> ProductCategoriePaths { set; get; }
        public DbSet<ProductCategoryToLayout> ProductCategorieToLayouts { set; get; }
        public DbSet<ProductCategoryToStore> ProductCategorieToStores { set; get; }
        public DbSet<ProductDescription> ProductDescriptions{ set; get; }
        public DbSet<ProductDiscount> ProductDiscounts{ set; get; }
        public DbSet<ProductFilter> ProductFilters{ set; get; }
        public DbSet<ProductImage> ProductImages{ set; get; }
        public DbSet<ProductOption> ProductOptions{ set; get; }
        public DbSet<ProductOptionValue> ProductOptionValues{ set; get; }
        public DbSet<ProductRecurring> ProductRecurrings{ set; get; }
        public DbSet<ProductRelated> ProductRelateds{ set; get; }
        public DbSet<ProductReward> ProductRewards{ set; get; }
        public DbSet<ProductSpecial> ProductSpecials{ set; get; }
        public DbSet<ProductTag> ProductTags{ set; get; }
        public DbSet<ProductToCategory> ProductToCategories{ set; get; }
        public DbSet<ProductToLayout> ProductToLayouts{ set; get; }
        public DbSet<ProductToStore> ProductToStores{ set; get; }
        public DbSet<ProductWishlist> ProductWishlists{ set; get; }
        

        #endregion

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

        public DbSet<Post> Blogs { set; get; }
        public DbSet<PostTag> BlogTags { set; get; }
        public DbSet<ContactDetail> ContactDetails { set; get; }
        public DbSet<Feedback> Feedbacks { set; get; }
        public DbSet<Footer> Footers { set; get; }
        public DbSet<Page> Pages { set; get; }

        public DbSet<Slide> Slides { set; get; }

        public DbSet<Tag> Tags { set; get; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<AdvertistmentPage> AdvertistmentPages { get; set; }
        public DbSet<Advertistment> Advertistments { get; set; }
        public DbSet<AdvertistmentPosition> AdvertistmentPositions { get; set; }
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