﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TeduCore.Application.ECommerce.ProductCategories;

namespace TeduCore.WebApp.Controllers.Components
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private IProductCategoryService _productCategoryService;

        public CategoryMenuViewComponent(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult<IViewComponentResult>(View(_productCategoryService.GetAll()));

        }
    }
}