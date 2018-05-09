using Microsoft.AspNetCore.Mvc;
using TeduCore.Services.Interfaces;

namespace TeduCore.WebApi.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult(_productCategoryService.GetAll());
        }
    }
}