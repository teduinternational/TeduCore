using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TeduCore.Application.ECommerce.ProductCategories;

namespace TeduCore.WebApp.Controllers.Components
{
    public class MobileMenuViewComponent : ViewComponent
    {
        private IProductCategoryService _productCategoryService;

        public MobileMenuViewComponent(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult<IViewComponentResult>(View(_productCategoryService.GetAll()));
        }
    }
}