using AutoMapper;
using TeduCore.Application.Content.Contacts.Dtos;
using TeduCore.Application.Content.Feedbacks.Dtos;
using TeduCore.Application.Content.Pages.Dtos;
using TeduCore.Application.Content.Posts.Dtos;
using TeduCore.Application.Content.Slides.Dtos;
using TeduCore.Application.ECommerce.Products.Dtos;
using TeduCore.Application.Systems.Functions.Dtos;
using TeduCore.Application.Systems.Permissions.Dtos;
using TeduCore.Application.Systems.Users.Dtos;
using TeduCore.Data.Entities;

namespace TeduCore.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<FunctionViewModel, Function>()
              .ConstructUsing(c => new Function(c.Name, c.URL, c.ParentId,
              c.IconCss, c.DisplayOrder));

            CreateMap<PostViewModel, Post>()
              .ConstructUsing(c => new Post(c.Name, c.Image, c.Description,
              c.Content, c.HomeFlag, c.HotFlag, c.Tags, c.Status,
              c.SeoPageTitle, c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<AppUserViewModel, AppUser>()
             .ConstructUsing(c => new AppUser(c.FullName, c.UserName, c.Email, c.PhoneNumber, c.Avatar, c.Status));

            CreateMap<PermissionViewModel, Permission>()
             .ConstructUsing(c => new Permission(c.RoleId, c.FunctionId));

            CreateMap<SlideViewModel, Slide>()
                .ConstructUsing(c => new Slide(c.Id, c.Name, c.Description, c.Image, c.Url, c.DisplayOrder, c.Status, c.Content, c.GroupAlias));

            CreateMap<PageViewModel, Page>()
               .ConstructUsing(c => new Page(c.Id, c.Name, c.Alias, c.Content, c.Status));

            CreateMap<ContactDetailViewModel, ContactDetail>()
                .ConstructUsing(c => new ContactDetail(c.Id, c.Name, c.Phone, c.Email, c.Website, c.Address, c.Other, c.Lng, c.Lat, c.Status));

            CreateMap<FeedbackViewModel, Feedback>()
                .ConstructUsing(c => new Feedback(c.Id, c.Name, c.Email, c.Message, c.Status));

            CreateMap<ProductWishlistViewModel, ProductWishlist>()
                .ConstructUsing(c => new ProductWishlist(c.Id, c.UserId, c.ProductId));
        }
    }
}