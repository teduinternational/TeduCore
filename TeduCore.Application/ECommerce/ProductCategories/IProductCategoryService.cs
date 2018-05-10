using System;
using System.Collections.Generic;
using TeduCore.Application.ECommerce.ProductCategories.Dtos;

namespace TeduCore.Application.ECommerce.ProductCategories
{
    public interface IProductCategoryService
    {
        ProductCategoryViewModel Add(ProductCategoryViewModel productCategoryVm);

        void Update(ProductCategoryViewModel productCategoryVm);

        void Delete(Guid id);

        List<ProductCategoryViewModel> GetAll();

        List<ProductCategoryViewModel> GetAll(string keyword);

        List<ProductCategoryViewModel> GetAllByParentId(Guid? parentId);

        ProductCategoryViewModel GetById(Guid id);

        void UpdateParentId(Guid sourceId, Guid targetId, Dictionary<Guid, int> items);

        void ReOrder(Guid sourceId, Guid targetId);

        List<ProductCategoryViewModel> GetHomeCategories(int top);

        void Save();
    }
}