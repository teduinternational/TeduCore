using AutoMapper;
using TeduCore.Data.Entities;
using TeduCore.Services.ViewModels;
using TeduCore.Services.ViewModels.Blog;
using TeduCore.Services.ViewModels.Common;
using TeduCore.Services.ViewModels.Product;

namespace TeduCore.Services.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<FunctionViewModel, Function>()
              .ConstructUsing(c => new Function(c.Name, c.URL, c.ParentId,
              c.IconCss, c.DisplayOrder));

            CreateMap<ProductViewModel, Product>()
              .ConstructUsing(c => new Product(c.Id, c.Name, c.Code, c.CategoryId, c.ThumbnailImage, c.Price, c.OriginalPrice,
              c.PromotionPrice, c.Description, c.Content, c.HomeFlag, c.HotFlag, c.Tags, c.Quantity, c.Unit, c.Status,
              c.SeoPageTitle, c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<ProductCategoryViewModel, ProductCategory>()
                .ConstructUsing(c => new ProductCategory(c.Id, c.Name, c.Code, c.Description, c.ParentId, c.HomeOrder, c.Image,
                    c.HomeFlag, c.SortOrder, c.Status, c.SeoPageTitle, c.SeoAlias, c.SeoKeywords, c.SeoDescription))
                .ForMember("Products", conf => conf.Ignore());

            CreateMap<BlogViewModel, Blog>()
              .ConstructUsing(c => new Blog(c.Name, c.Image, c.Description,
              c.Content, c.HomeFlag, c.HotFlag, c.Tags, c.Status,
              c.SeoPageTitle, c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<BillViewModel, Bill>()
              .ConstructUsing(c => new Bill(c.Id, c.CustomerName, c.CustomerAddress, c.CustomerMobile, c.CustomerMessage, c.BillStatus, c.PaymentMethod, c.Status, c.CustomerFacebook, c.ShippingFee));

            CreateMap<BillDetailViewModel, BillDetail>()
              .ConstructUsing(c => new BillDetail(c.Id, c.BillId, c.ProductId, c.Quantity, c.Price));

            CreateMap<AppUserViewModel, AppUser>()
             .ConstructUsing(c => new AppUser(c.FullName, c.UserName, c.Email, c.PhoneNumber, c.Avatar, c.Status));

            CreateMap<PermissionViewModel, Permission>()
             .ConstructUsing(c => new Permission(c.RoleId, c.FunctionId, c.CanCreate, c.CanRead, c.CanUpdate, c.CanDelete));

            CreateMap<SlideViewModel, Slide>()
                .ConstructUsing(c => new Slide(c.Id, c.Name, c.Description, c.Image, c.Url, c.DisplayOrder, c.Status, c.Content, c.GroupAlias));

            CreateMap<PageViewModel, Page>()
               .ConstructUsing(c => new Page(c.Id, c.Name, c.Alias, c.Content, c.Status));

            CreateMap<ContactDetailViewModel, ContactDetail>()
                .ConstructUsing(c => new ContactDetail(c.Id, c.Name, c.Phone, c.Email, c.Website, c.Address, c.Other, c.Lng, c.Lat, c.Status));

            CreateMap<FeedbackViewModel, Feedback>()
                .ConstructUsing(c => new Feedback(c.Id, c.Name, c.Email, c.Message, c.Status));

            CreateMap<ProductWishlistViewModel, ProductWishlist>()
                .ConstructUsing(c => new ProductWishlist(c.Id,c.UserId,c.ProductId));
        }
    }
}