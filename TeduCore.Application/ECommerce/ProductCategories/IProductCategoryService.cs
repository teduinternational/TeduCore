using System;
using TeduCore.Application.ECommerce.ProductCategories.Dtos;
using TeduCore.Data.Entities.ECommerce;

namespace TeduCore.Application.ECommerce.ProductCategories
{
    public interface IProductCategoryService : IWebServiceBase<ProductCategory, Guid, ProductCategoryViewModel>
    {
    }
}