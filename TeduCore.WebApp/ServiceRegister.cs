using Microsoft.Extensions.DependencyInjection;
using TeduCore.Application.Content.Contacts;
using TeduCore.Application.Content.Feedbacks;
using TeduCore.Application.Content.Pages;
using TeduCore.Application.Content.Posts;
using TeduCore.Application.Content.Slides;
using TeduCore.Application.Dapper.Reports;
using TeduCore.Application.ECommerce.Products;
using TeduCore.Application.Systems.Commons;
using TeduCore.Application.Systems.Functions;
using TeduCore.Application.Systems.Roles;
using TeduCore.Application.Systems.Users;

namespace TeduCore.WebApp
{
    public static class ServiceRegister
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IFunctionService, FunctionService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<IPageService, PageService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ISlideService, SlideService>();
            services.AddTransient<IPageService, PageService>();

            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IFeedbackService, FeedbackService>();
            services.AddTransient<IReportService, ReportService>();
        }
    }
}