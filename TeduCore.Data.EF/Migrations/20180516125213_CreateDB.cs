using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TeduCore.Data.EF.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvertistmentPages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UniqueCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertistmentPages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdvertistmentPositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    PageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertistmentPositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Advertistments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    Image = table.Column<string>(maxLength: 250, nullable: true),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    PositionId = table.Column<Guid>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Url = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertistments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(maxLength: 250, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AnnouncementId = table.Column<Guid>(nullable: false),
                    HasRead = table.Column<bool>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    ProviderKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.RoleId, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Avatar = table.Column<string>(nullable: true),
                    Balance = table.Column<decimal>(nullable: false),
                    BirthDay = table.Column<DateTime>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BrowserInfo = table.Column<string>(nullable: true),
                    ClientIpAddress = table.Column<string>(nullable: true),
                    ClientName = table.Column<string>(nullable: true),
                    CustomData = table.Column<string>(nullable: true),
                    Exception = table.Column<string>(nullable: true),
                    ExecutionDuration = table.Column<int>(nullable: false),
                    ExecutionTime = table.Column<DateTime>(nullable: false),
                    ImpersonatorTenantId = table.Column<Guid>(nullable: true),
                    ImpersonatorUserId = table.Column<Guid>(nullable: true),
                    MethodName = table.Column<string>(nullable: true),
                    Parameters = table.Column<string>(nullable: true),
                    ServiceName = table.Column<string>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PostId = table.Column<Guid>(nullable: false),
                    TagId = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetails",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 255, nullable: false),
                    Address = table.Column<string>(maxLength: 250, nullable: true),
                    Email = table.Column<string>(maxLength: 250, nullable: true),
                    Lat = table.Column<double>(nullable: true),
                    Lng = table.Column<double>(nullable: true),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Other = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Website = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComAddress",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address1 = table.Column<string>(maxLength: 128, nullable: true),
                    Address2 = table.Column<string>(maxLength: 128, nullable: true),
                    City = table.Column<string>(maxLength: 128, nullable: true),
                    Company = table.Column<string>(maxLength: 40, nullable: true),
                    CountryId = table.Column<Guid>(nullable: false),
                    CustomField = table.Column<string>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 32, nullable: true),
                    LastName = table.Column<string>(maxLength: 32, nullable: true),
                    PostCode = table.Column<string>(maxLength: 10, nullable: true),
                    ZoneId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComAddress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComAttributeDescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AttributeId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComAttributeDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComAttributeGroupDescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AttributeGroupId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComAttributeGroupDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComAttributeGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComAttributeGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AttributeGroupId = table.Column<Guid>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComCouponCategorys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CouponId = table.Column<Guid>(nullable: false),
                    ProductCategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComCouponCategorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComCouponDescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CouponId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComCouponDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComCouponHistorys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    CouponId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    OrderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComCouponHistorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComCouponProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CouponId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComCouponProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComCoupons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateEnd = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateStart = table.Column<DateTime>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    Logged = table.Column<bool>(nullable: false),
                    Shipping = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    Type = table.Column<string>(maxLength: 20, nullable: true),
                    UsesCustomer = table.Column<int>(nullable: false),
                    UsesTotal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComCoupons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComCurrencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 3, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DecimalPlace = table.Column<string>(maxLength: 10, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    SymbolLeft = table.Column<string>(maxLength: 12, nullable: true),
                    SymbolRight = table.Column<string>(maxLength: 12, nullable: true),
                    Title = table.Column<string>(maxLength: 32, nullable: true),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComCurrencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComCustomerActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    Data = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Ip = table.Column<string>(maxLength: 40, nullable: true),
                    Key = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComCustomerActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComCustomerLogins",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    Ip = table.Column<string>(maxLength: 40, nullable: true),
                    Total = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComCustomerLogins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComCustomerOnlines",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Ip = table.Column<string>(nullable: true),
                    Referer = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComCustomerOnlines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComCustomerRewards",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OrderId = table.Column<Guid>(nullable: false),
                    Points = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComCustomerRewards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComCustomers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AddressId = table.Column<Guid>(nullable: false),
                    Cart = table.Column<string>(nullable: true),
                    Code = table.Column<string>(maxLength: 40, nullable: true),
                    CustomField = table.Column<string>(nullable: true),
                    CustomerGroupId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    Fax = table.Column<string>(maxLength: 32, nullable: true),
                    FirstName = table.Column<string>(maxLength: 32, nullable: true),
                    IP = table.Column<string>(nullable: true),
                    LanguageId = table.Column<Guid>(nullable: false),
                    LastName = table.Column<string>(maxLength: 32, nullable: true),
                    NewsLetter = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: true),
                    Safe = table.Column<bool>(nullable: false),
                    Salt = table.Column<string>(maxLength: 10, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: false),
                    Telephone = table.Column<string>(maxLength: 32, nullable: true),
                    Token = table.Column<string>(nullable: true),
                    Wishlist = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComCustomers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComCustomerSearchs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Ip = table.Column<string>(maxLength: 40, nullable: true),
                    Keyword = table.Column<string>(maxLength: 256, nullable: true),
                    LanguageId = table.Column<Guid>(nullable: false),
                    ProductCategoryId = table.Column<Guid>(nullable: false),
                    Products = table.Column<int>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: false),
                    SubCategory = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComCustomerSearchs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComCustomerTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OrderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComCustomerTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComCustomerWishlists",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComCustomerWishlists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComCustomFieldCustomerGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomFieldId = table.Column<Guid>(nullable: false),
                    CustomerGroupId = table.Column<Guid>(nullable: false),
                    Required = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComCustomFieldCustomerGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComCustomFieldDescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomFieldId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComCustomFieldDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComCustomFields",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Location = table.Column<string>(maxLength: 10, nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Type = table.Column<string>(maxLength: 32, nullable: true),
                    Validation = table.Column<string>(maxLength: 255, nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComCustomFields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComCustomFieldValueDescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomFieldId = table.Column<Guid>(nullable: false),
                    CustomFieldValueId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComCustomFieldValueDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComCustomFieldValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomFieldId = table.Column<Guid>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComCustomFieldValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComFilterDescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FilterGroupId = table.Column<Guid>(nullable: false),
                    FilterId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComFilterDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComFilterGroupDescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FilterGroupId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComFilterGroupDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComFilterGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComFilterGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComFilters",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FilterGroupId = table.Column<Guid>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComFilters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComLengthClass",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComLengthClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComLengthClassDescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    LengthClassId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 32, nullable: true),
                    Unit = table.Column<string>(maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComLengthClassDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComOptionDescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    OptionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComOptionDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    Type = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComOptionValueDescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    OptionId = table.Column<Guid>(nullable: false),
                    OptionValueId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComOptionValueDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComOptionValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Image = table.Column<string>(maxLength: 128, nullable: true),
                    OptionId = table.Column<Guid>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComOptionValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComOrderHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Notify = table.Column<bool>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    OrderStatusId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComOrderHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComOrderOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    OrderId = table.Column<Guid>(nullable: false),
                    OrderProductId = table.Column<Guid>(nullable: false),
                    ProductOptionId = table.Column<Guid>(nullable: false),
                    ProductOptionValueId = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(maxLength: 32, nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComOrderOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComOrderProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Model = table.Column<string>(maxLength: 64, nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Reward = table.Column<int>(nullable: false),
                    Tax = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComOrderProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComOrderRecurrings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    OrderId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    ProductName = table.Column<string>(maxLength: 255, nullable: true),
                    ProductQuantity = table.Column<int>(nullable: false),
                    RecurringCycle = table.Column<int>(nullable: false),
                    RecurringDescription = table.Column<string>(maxLength: 255, nullable: true),
                    RecurringDuration = table.Column<int>(nullable: false),
                    RecurringFrequency = table.Column<string>(maxLength: 25, nullable: true),
                    RecurringId = table.Column<Guid>(nullable: false),
                    RecurringName = table.Column<string>(maxLength: 255, nullable: true),
                    RecurringPrice = table.Column<decimal>(nullable: false),
                    Reference = table.Column<string>(maxLength: 255, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Trial = table.Column<int>(nullable: false),
                    TrialCycle = table.Column<int>(nullable: false),
                    TrialDuration = table.Column<int>(nullable: false),
                    TrialFrequency = table.Column<string>(nullable: true),
                    TrialPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComOrderRecurrings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComOrderRecurringTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    OrderRecurringId = table.Column<Guid>(nullable: false),
                    Reference = table.Column<string>(maxLength: 255, nullable: true),
                    Type = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComOrderRecurringTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AcceptLanguage = table.Column<string>(maxLength: 255, nullable: true),
                    AffiliateId = table.Column<Guid>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    Commission = table.Column<decimal>(nullable: false),
                    CurrencyCode = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<Guid>(nullable: false),
                    CurrencyValue = table.Column<decimal>(nullable: false),
                    CustomField = table.Column<string>(nullable: true),
                    CustomerGroupId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    Fax = table.Column<string>(maxLength: 32, nullable: true),
                    FirstName = table.Column<string>(maxLength: 32, nullable: true),
                    FowardedIp = table.Column<string>(maxLength: 40, nullable: true),
                    InvoiceNo = table.Column<int>(nullable: false),
                    InvoicePrefix = table.Column<string>(nullable: true),
                    Ip = table.Column<string>(maxLength: 40, nullable: true),
                    LanguageId = table.Column<Guid>(nullable: false),
                    LastName = table.Column<string>(maxLength: 32, nullable: true),
                    MarketingId = table.Column<Guid>(nullable: false),
                    OrderStatusId = table.Column<Guid>(nullable: false),
                    PaymentAddress1 = table.Column<string>(maxLength: 128, nullable: true),
                    PaymentAddress2 = table.Column<string>(maxLength: 128, nullable: true),
                    PaymentAddressFormat = table.Column<string>(nullable: true),
                    PaymentCity = table.Column<string>(maxLength: 128, nullable: true),
                    PaymentCode = table.Column<string>(maxLength: 128, nullable: true),
                    PaymentCompany = table.Column<string>(maxLength: 60, nullable: true),
                    PaymentCountry = table.Column<string>(maxLength: 128, nullable: true),
                    PaymentCountryId = table.Column<Guid>(nullable: false),
                    PaymentCustomField = table.Column<string>(nullable: true),
                    PaymentFirstName = table.Column<string>(maxLength: 32, nullable: true),
                    PaymentLastName = table.Column<string>(maxLength: 32, nullable: true),
                    PaymentMethod = table.Column<string>(maxLength: 128, nullable: true),
                    PaymentPostCode = table.Column<string>(maxLength: 10, nullable: true),
                    PaymentZone = table.Column<string>(maxLength: 128, nullable: true),
                    PaymentZoneId = table.Column<Guid>(nullable: false),
                    ShippingAddress1 = table.Column<string>(maxLength: 128, nullable: true),
                    ShippingAddress2 = table.Column<string>(maxLength: 128, nullable: true),
                    ShippingAddressFormat = table.Column<string>(nullable: true),
                    ShippingCity = table.Column<string>(maxLength: 128, nullable: true),
                    ShippingCode = table.Column<string>(maxLength: 128, nullable: true),
                    ShippingCompany = table.Column<string>(maxLength: 32, nullable: true),
                    ShippingCountry = table.Column<string>(maxLength: 128, nullable: true),
                    ShippingCountryId = table.Column<Guid>(nullable: false),
                    ShippingCustomField = table.Column<string>(nullable: true),
                    ShippingFirstName = table.Column<string>(maxLength: 32, nullable: true),
                    ShippingLastName = table.Column<string>(maxLength: 32, nullable: true),
                    ShippingMethod = table.Column<string>(maxLength: 128, nullable: true),
                    ShippingPostCode = table.Column<string>(maxLength: 10, nullable: true),
                    ShippingZone = table.Column<string>(maxLength: 128, nullable: true),
                    ShippingZoneId = table.Column<Guid>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: false),
                    StoreName = table.Column<string>(maxLength: 64, nullable: true),
                    StoreUrl = table.Column<string>(maxLength: 255, nullable: true),
                    Telephone = table.Column<string>(maxLength: 32, nullable: true),
                    Total = table.Column<decimal>(nullable: false),
                    Tracking = table.Column<string>(maxLength: 64, nullable: true),
                    UserAgent = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComOrderShipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    OrderId = table.Column<Guid>(nullable: false),
                    ShippingCourierId = table.Column<Guid>(nullable: false),
                    TrackingNumber = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComOrderShipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComOrderStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComOrderStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComOrderTotals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    OrderId = table.Column<Guid>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComOrderTotals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComOrderVouchers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Code = table.Column<string>(maxLength: 10, nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    FromEmail = table.Column<string>(maxLength: 200, nullable: true),
                    FromName = table.Column<string>(maxLength: 64, nullable: true),
                    Message = table.Column<string>(nullable: true),
                    OrderId = table.Column<Guid>(nullable: false),
                    ToEmail = table.Column<string>(maxLength: 200, nullable: true),
                    ToName = table.Column<string>(maxLength: 64, nullable: true),
                    VoucherId = table.Column<Guid>(nullable: false),
                    VoucherThemeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComOrderVouchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComProductAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AttributeId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComProductAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComProductCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CurrentIdentity = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    HomeFlag = table.Column<bool>(nullable: true),
                    HomeOrder = table.Column<int>(nullable: true),
                    Image = table.Column<string>(maxLength: 256, nullable: true),
                    ParentId = table.Column<Guid>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComProductCategoryDescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    LanguageId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    ProductCategoryId = table.Column<Guid>(nullable: false),
                    SeoAlias = table.Column<string>(maxLength: 255, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 255, nullable: true),
                    SeoKeywords = table.Column<string>(maxLength: 255, nullable: true),
                    SeoPageTitle = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComProductCategoryDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComProductCategoryFilters",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FilterId = table.Column<Guid>(nullable: false),
                    ProductCategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComProductCategoryFilters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComProductCategoryPaths",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Path = table.Column<Guid>(nullable: false),
                    ProductCategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComProductCategoryPaths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComProductCategoryToLayouts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LayoutId = table.Column<Guid>(nullable: false),
                    ProductCategoryId = table.Column<Guid>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComProductCategoryToLayouts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComProductCategoryToStores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductCategoryId = table.Column<Guid>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComProductCategoryToStores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComProductDescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    LanguageId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    SeoAlias = table.Column<string>(maxLength: 255, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 255, nullable: true),
                    SeoKeywords = table.Column<string>(maxLength: 255, nullable: true),
                    SeoPageTitle = table.Column<string>(maxLength: 255, nullable: true),
                    Tag = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComProductDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComProductDiscounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerGroupId = table.Column<Guid>(nullable: false),
                    DateEnd = table.Column<DateTime>(nullable: false),
                    DateStart = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComProductDiscounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComProductFilters",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FilterId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComProductFilters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComProductImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Caption = table.Column<string>(maxLength: 250, nullable: true),
                    Path = table.Column<string>(maxLength: 250, nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComProductImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComProductOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OptionId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    Required = table.Column<bool>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComProductOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComProductOptionValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OptionId = table.Column<Guid>(nullable: false),
                    OptionValueId = table.Column<Guid>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    PointsPrefix = table.Column<string>(maxLength: 1, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    PricePrefix = table.Column<string>(maxLength: 1, nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    ProductOptionId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Subtract = table.Column<bool>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false),
                    WeightPrefix = table.Column<string>(maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComProductOptionValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComProductRecurrings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerGroupId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    RecurringId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComProductRecurrings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComProductRelateds",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    RelatedId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComProductRelateds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComProductRewards",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerGroupId = table.Column<Guid>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComProductRewards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateAvailable = table.Column<DateTime>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Ean = table.Column<string>(maxLength: 14, nullable: true),
                    Height = table.Column<decimal>(nullable: false),
                    Image = table.Column<string>(maxLength: 255, nullable: true),
                    Isbn = table.Column<string>(maxLength: 17, nullable: true),
                    Jan = table.Column<string>(maxLength: 13, nullable: true),
                    Length = table.Column<decimal>(nullable: false),
                    LengthClassId = table.Column<Guid>(nullable: false),
                    Location = table.Column<string>(maxLength: 128, nullable: true),
                    ManufacturerId = table.Column<Guid>(nullable: false),
                    Minimum = table.Column<int>(nullable: false),
                    Model = table.Column<string>(maxLength: 64, nullable: true),
                    Mpn = table.Column<string>(maxLength: 64, nullable: true),
                    Points = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Shipping = table.Column<bool>(nullable: false),
                    Sku = table.Column<string>(maxLength: 64, nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    StockStatusId = table.Column<Guid>(nullable: false),
                    Subtract = table.Column<bool>(nullable: false),
                    TaxClassId = table.Column<Guid>(nullable: false),
                    Upc = table.Column<string>(maxLength: 12, nullable: true),
                    Viewed = table.Column<int>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false),
                    WeightClassId = table.Column<Guid>(nullable: false),
                    Width = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComProductSpecials",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerGroupId = table.Column<Guid>(nullable: false),
                    DateEnd = table.Column<DateTime>(nullable: false),
                    DateStart = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComProductSpecials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComProductToCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductCategoryId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComProductToCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComProductToLayouts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LayoutId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComProductToLayouts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EComProductToStores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EComProductToStores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(maxLength: 250, nullable: true),
                    Message = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Footers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Content = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Footers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Functions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CssClass = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    ParentId = table.Column<Guid>(nullable: true),
                    ParentList = table.Column<string>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UniqueCode = table.Column<string>(nullable: true),
                    Url = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Functions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Resources = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 255, nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    SeoAlias = table.Column<string>(nullable: true),
                    SeoDescription = table.Column<string>(nullable: true),
                    SeoKeywords = table.Column<string>(nullable: true),
                    SeoPageTitle = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    UniqueCode = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FunctionId = table.Column<Guid>(maxLength: 128, nullable: false),
                    RoleId = table.Column<Guid>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    HomeFlag = table.Column<bool>(nullable: true),
                    HotFlag = table.Column<bool>(nullable: true),
                    Image = table.Column<string>(maxLength: 256, nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    SeoAlias = table.Column<string>(maxLength: 256, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 256, nullable: true),
                    SeoKeywords = table.Column<string>(maxLength: 256, nullable: true),
                    SeoPageTitle = table.Column<string>(maxLength: 256, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Tags = table.Column<string>(nullable: true),
                    ViewCount = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    TagId = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductWishlists",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductWishlists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 255, nullable: false),
                    BooleanValue = table.Column<bool>(nullable: true),
                    DateValue = table.Column<DateTime>(nullable: true),
                    DecimalValue = table.Column<decimal>(nullable: true),
                    IntegerValue = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    TextValue = table.Column<string>(nullable: true),
                    UniqueCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Slides",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    DisplayOrder = table.Column<int>(nullable: true),
                    GroupAlias = table.Column<int>(nullable: false),
                    Image = table.Column<string>(maxLength: 250, nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Url = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slides", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertistmentPages");

            migrationBuilder.DropTable(
                name: "AdvertistmentPositions");

            migrationBuilder.DropTable(
                name: "Advertistments");

            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "AnnouncementUsers");

            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "BlogTags");

            migrationBuilder.DropTable(
                name: "ContactDetails");

            migrationBuilder.DropTable(
                name: "EComAddress");

            migrationBuilder.DropTable(
                name: "EComAttributeDescriptions");

            migrationBuilder.DropTable(
                name: "EComAttributeGroupDescriptions");

            migrationBuilder.DropTable(
                name: "EComAttributeGroups");

            migrationBuilder.DropTable(
                name: "EComAttributes");

            migrationBuilder.DropTable(
                name: "EComCouponCategorys");

            migrationBuilder.DropTable(
                name: "EComCouponDescriptions");

            migrationBuilder.DropTable(
                name: "EComCouponHistorys");

            migrationBuilder.DropTable(
                name: "EComCouponProducts");

            migrationBuilder.DropTable(
                name: "EComCoupons");

            migrationBuilder.DropTable(
                name: "EComCurrencies");

            migrationBuilder.DropTable(
                name: "EComCustomerActivities");

            migrationBuilder.DropTable(
                name: "EComCustomerLogins");

            migrationBuilder.DropTable(
                name: "EComCustomerOnlines");

            migrationBuilder.DropTable(
                name: "EComCustomerRewards");

            migrationBuilder.DropTable(
                name: "EComCustomers");

            migrationBuilder.DropTable(
                name: "EComCustomerSearchs");

            migrationBuilder.DropTable(
                name: "EComCustomerTransactions");

            migrationBuilder.DropTable(
                name: "EComCustomerWishlists");

            migrationBuilder.DropTable(
                name: "EComCustomFieldCustomerGroups");

            migrationBuilder.DropTable(
                name: "EComCustomFieldDescriptions");

            migrationBuilder.DropTable(
                name: "EComCustomFields");

            migrationBuilder.DropTable(
                name: "EComCustomFieldValueDescriptions");

            migrationBuilder.DropTable(
                name: "EComCustomFieldValues");

            migrationBuilder.DropTable(
                name: "EComFilterDescriptions");

            migrationBuilder.DropTable(
                name: "EComFilterGroupDescriptions");

            migrationBuilder.DropTable(
                name: "EComFilterGroups");

            migrationBuilder.DropTable(
                name: "EComFilters");

            migrationBuilder.DropTable(
                name: "EComLengthClass");

            migrationBuilder.DropTable(
                name: "EComLengthClassDescriptions");

            migrationBuilder.DropTable(
                name: "EComOptionDescriptions");

            migrationBuilder.DropTable(
                name: "EComOptions");

            migrationBuilder.DropTable(
                name: "EComOptionValueDescriptions");

            migrationBuilder.DropTable(
                name: "EComOptionValues");

            migrationBuilder.DropTable(
                name: "EComOrderHistories");

            migrationBuilder.DropTable(
                name: "EComOrderOptions");

            migrationBuilder.DropTable(
                name: "EComOrderProducts");

            migrationBuilder.DropTable(
                name: "EComOrderRecurrings");

            migrationBuilder.DropTable(
                name: "EComOrderRecurringTransactions");

            migrationBuilder.DropTable(
                name: "EComOrders");

            migrationBuilder.DropTable(
                name: "EComOrderShipments");

            migrationBuilder.DropTable(
                name: "EComOrderStatus");

            migrationBuilder.DropTable(
                name: "EComOrderTotals");

            migrationBuilder.DropTable(
                name: "EComOrderVouchers");

            migrationBuilder.DropTable(
                name: "EComProductAttributes");

            migrationBuilder.DropTable(
                name: "EComProductCategories");

            migrationBuilder.DropTable(
                name: "EComProductCategoryDescriptions");

            migrationBuilder.DropTable(
                name: "EComProductCategoryFilters");

            migrationBuilder.DropTable(
                name: "EComProductCategoryPaths");

            migrationBuilder.DropTable(
                name: "EComProductCategoryToLayouts");

            migrationBuilder.DropTable(
                name: "EComProductCategoryToStores");

            migrationBuilder.DropTable(
                name: "EComProductDescriptions");

            migrationBuilder.DropTable(
                name: "EComProductDiscounts");

            migrationBuilder.DropTable(
                name: "EComProductFilters");

            migrationBuilder.DropTable(
                name: "EComProductImages");

            migrationBuilder.DropTable(
                name: "EComProductOptions");

            migrationBuilder.DropTable(
                name: "EComProductOptionValues");

            migrationBuilder.DropTable(
                name: "EComProductRecurrings");

            migrationBuilder.DropTable(
                name: "EComProductRelateds");

            migrationBuilder.DropTable(
                name: "EComProductRewards");

            migrationBuilder.DropTable(
                name: "EComProducts");

            migrationBuilder.DropTable(
                name: "EComProductSpecials");

            migrationBuilder.DropTable(
                name: "EComProductToCategories");

            migrationBuilder.DropTable(
                name: "EComProductToLayouts");

            migrationBuilder.DropTable(
                name: "EComProductToStores");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Footers");

            migrationBuilder.DropTable(
                name: "Functions");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "ProductTags");

            migrationBuilder.DropTable(
                name: "ProductWishlists");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Slides");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
