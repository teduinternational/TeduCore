using AutoMapper;
using TeduCore.Application.Content.Contacts.Dtos;
using TeduCore.Application.Content.Feedbacks.Dtos;
using TeduCore.Application.Content.Pages.Dtos;
using TeduCore.Application.Content.Posts.Dtos;
using TeduCore.Application.Content.Slides.Dtos;
using TeduCore.Application.Dtos;
using TeduCore.Application.ECommerce.Products.Dtos;
using TeduCore.Application.Systems.Functions.Dtos;
using TeduCore.Application.Systems.Roles.Dtos;
using TeduCore.Application.Systems.Settings.Dtos;
using TeduCore.Application.Systems.Users.Dtos;
using TeduCore.Application.ViewModels;
using TeduCore.Data.Entities;
using TeduCore.Data.Entities.ECommerce;

namespace TeduCore.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Function, FunctionViewModel>().MaxDepth(2);

            CreateMap<Tag, TagViewModel>().MaxDepth(2);
            CreateMap<ProductTag, ProductTagViewModel>().MaxDepth(2);
            CreateMap<Post, PostViewModel>().MaxDepth(2);
            CreateMap<PostTag, PostTagViewModel>().MaxDepth(2);
            CreateMap<Footer, FooterViewModel>().MaxDepth(2);
            CreateMap<Slide, SlideViewModel>().MaxDepth(2);
            CreateMap<Setting, SystemConfigViewModel>().MaxDepth(2);
            CreateMap<AppUser, AppUserViewModel>().MaxDepth(2);
            CreateMap<AppRole, AppRoleViewModel>().MaxDepth(2);
            CreateMap<ProductImage, ProductImageViewModel>().MaxDepth(2);
            CreateMap<Page, PageViewModel>().MaxDepth(2);

            CreateMap<ContactDetail, ContactDetailViewModel>().MaxDepth(2);
            CreateMap<Feedback, FeedbackViewModel>().MaxDepth(2);
            CreateMap<ProductWishlist, ProductWishlistViewModel>().MaxDepth(2);
        }
    }
}