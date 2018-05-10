using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;
using TeduCore.Application.ECommerce.ProductCategories;
using TeduCore.Application.ECommerce.Products;
using TeduCore.WebApp.Extensions;
using TeduCore.WebApp.Models.ProductViewModels;

namespace TeduCore.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IConfiguration _configuration;

        public ProductController(IProductService productService, IConfiguration configuration,
            IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _configuration = configuration;
        }

        [Route("san-pham.html", Name = "AllProducts")]
        public IActionResult Index()
        {
            ViewData["BodyClass"] = "product-page";
            var categories = _productCategoryService.GetAll();
            return View(categories);
        }

        [Route("{alias}-c.{id}.html", Name = "ProductCatalog")]
        public IActionResult Catalog(Guid id, string keyword, int? pageSize, string sortBy, int page = 1)
        {
            var model = new CatalogViewModel();

            model.BodyClass = "product-page";

            if (pageSize == null)
                pageSize = _configuration.GetValue<int>("PageSize");

            model.PageSize = pageSize;
            model.SortType = sortBy;

            model.Category = _productCategoryService.GetById(id);

            model.Data = _productService.GetAllPaging(id, keyword, page, pageSize.Value, sortBy);

            model.LastestProducts = _productService.GetLastest(3);

            model.AllCategories = _productCategoryService.GetAll();

            return View(model);
        }

        [Route("tim-kiem.html", Name = "Search")]
        public IActionResult Search(string keyword, int? pageSize, string sortBy, int page = 1)
        {
            var model = new SearchResultViewModel();

            model.BodyClass = "product-page";

            if (pageSize == null)
                pageSize = _configuration.GetValue<int>("PageSize");

            model.PageSize = pageSize;
            model.SortType = sortBy;

            model.Keyword = keyword;

            model.Data = _productService.GetAllPaging(null, keyword, page, pageSize.Value, sortBy);

            model.LastestProducts = _productService.GetLastest(3);

            return View(model);
        }

        [Route("{alias}-p.{id}.html", Name = "ProductDetail")]
        public IActionResult Details(Guid id)
        {
            ViewData["BodyClass"] = "product-page";
            var model = new DetailViewModel();
            model.Product = _productService.GetById(id);
            model.Category = _productCategoryService.GetById(model.Product.CategoryId);
            model.RelatedProducts = _productService.GetReatedProducts(id, 4);
            model.LastestProducts = _productService.GetLastest(3);
            model.UpsellProducts = _productService.GetUpsellProducts(8);
            model.ProductImages = _productService.GetImages(id);
            return View(model);
        }

        [Route("san-pham-yeu-thich.html", Name = "Wishlist")]
        public IActionResult Wishlist(int? pageSize, int page = 1)
        {
            ViewData["BodyClass"] = "wishlist_page";

            if (pageSize == null)
                pageSize = _configuration.GetValue<int>("PageSize");
            var userId = Guid.Parse(((ClaimsIdentity)User.Identity).GetSpecificClaim("UserId"));
            var model = _productService.GetMyWishlist(userId, page, pageSize.Value);
            return View(model);
        }
    }
}