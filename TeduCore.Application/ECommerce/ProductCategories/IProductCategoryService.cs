using System;
using System.Collections.Generic;
using TeduCore.Application.ECommerce.ProductCategories.Dtos;
using TeduCore.Data.Entities;

namespace TeduCore.Application.ECommerce.ProductCategories
{
    public interface IProductCategoryService : IWebServiceBase<ProductCategory, Guid, ProductCategoryViewModel>
    {
        List<ProductCategoryViewModel> GetAll(string keyword);

        List<ProductCategoryViewModel> GetAllByParentId(Guid? parentId);

        void UpdateParentId(Guid sourceId, Guid targetId, Dictionary<Guid, int> items);

        void ReOrder(Guid sourceId, Guid targetId);

        List<ProductCategoryViewModel> GetHomeCategories(int top);
    }
}