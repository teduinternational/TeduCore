using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TeduCore.Data.Entities;
using TeduCore.Services.ViewModels;
using TeduCore.Services.ViewModels.Blog;
using TeduCore.Services.ViewModels.Common;
using TeduCore.Services.ViewModels.Product;
using TeduCore.Services.ViewModels.System;

namespace TeduCore.Services.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Function, FunctionViewModel>().MaxDepth(2);
            CreateMap<Bill, BillViewModel>().MaxDepth(1);
            CreateMap<BillDetail, BillDetailViewModel>().MaxDepth(1);
            CreateMap<ProductCategory, ProductCategoryViewModel>().MaxDepth(2);
            CreateMap<Product, ProductViewModel>().MaxDepth(2);
            CreateMap<Tag, TagViewModel>().MaxDepth(2);
            CreateMap<Color, ColorViewModel>().MaxDepth(2);
            CreateMap<Size, SizeViewModel>().MaxDepth(2);
            CreateMap<ProductTag, ProductTagViewModel>().MaxDepth(2);
            CreateMap<Blog, BlogViewModel>().MaxDepth(2);
            CreateMap<BlogTag, BlogTagViewModel>().MaxDepth(2);
            CreateMap<Footer, FooterViewModel>().MaxDepth(2);
            CreateMap<Slide, SlideViewModel>().MaxDepth(2);
            CreateMap<SystemConfig, SystemConfigViewModel>().MaxDepth(2);
            CreateMap<AppUser, AppUserViewModel>().MaxDepth(2);
            CreateMap<AppRole, AppRoleViewModel>().MaxDepth(2);
            CreateMap<ProductImage, ProductImageViewModel>().MaxDepth(2);
            CreateMap<ProductQuantity, ProductQuantityViewModel>().MaxDepth(2);
            CreateMap<WholePrice, WholePriceViewModel>().MaxDepth(2);
            CreateMap<Page, PageViewModel>().MaxDepth(2);

            CreateMap<ContactDetail, ContactDetailViewModel>().MaxDepth(2);
            CreateMap<Feedback, FeedbackViewModel>().MaxDepth(2);
            CreateMap<ProductWishlist, ProductWishlistViewModel>().MaxDepth(2);
        }
    }
}
