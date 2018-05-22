using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TeduCore.Application.ECommerce.ProductCategories;
using TeduCore.Application.ECommerce.Products;
using TeduCore.Application.Systems.Commons;
using TeduCore.Data.Enums;
using TeduCore.WebApp.Models;
using TeduCore.Application.Content.Posts;

namespace TeduCore.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;

        private readonly IPostService _blogService;
        private readonly ICommonService _commonService;

        public HomeController(IProductService productService,
        IPostService blogService, ICommonService commonService,
        IProductCategoryService productCategoryService)
        {
            _blogService = blogService;
            _commonService = commonService;
            _productService = productService;
            _productCategoryService = productCategoryService;
        }

        public IActionResult Index()
        {
            ViewData["BodyClass"] = "cms-index-index cms-home-page";
            var homeVm = new HomeViewModel
            {
                //HomeCategories = _productCategoryService.GetHomeCategories(5),
                HotProducts = _productService.GetHotProduct(5),
                TopSellProducts = _productService.GetLastest(5),
                LastestBlogs = _blogService.GetLastest(5),
                HomeSlides = _commonService.GetSlides(SlideGroup.Top),
                Title = _commonService.GetSystemConfig("HomeTitle").Value1,
                MetaKeyword = _commonService.GetSystemConfig("HomeMetaKeyword").Value1,
                MetaDescription = _commonService.GetSystemConfig("HomeMetaDescription").Value1
            };
            return View(homeVm);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}