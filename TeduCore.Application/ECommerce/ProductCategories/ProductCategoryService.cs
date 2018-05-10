using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TeduCore.Application.ECommerce.ProductCategories.Dtos;
using TeduCore.Application.ECommerce.Products.Dtos;
using TeduCore.Data.Entities;
using TeduCore.Infrastructure.Enums;
using TeduCore.Infrastructure.Interfaces;
using TeduCore.Utilities.Helpers;

namespace TeduCore.Application.ECommerce.ProductCategories
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IRepository<ProductCategory, Guid> _productCategoryRepository;

        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoryService(IRepository<ProductCategory, Guid> productCategoryRepository,
            IRepository<Product, Guid> productRepository,
            IUnitOfWork unitOfWork)
        {
            _productCategoryRepository = productCategoryRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public ProductCategoryViewModel Add(ProductCategoryViewModel productCategoryVm)
        {
            if (string.IsNullOrEmpty(productCategoryVm.SeoAlias))
                productCategoryVm.SeoAlias = TextHelper.ToUnsignString(productCategoryVm.Name);

            var productCategory = Mapper.Map<ProductCategoryViewModel, ProductCategory>(productCategoryVm);
            _productCategoryRepository.Insert(productCategory);
            return productCategoryVm;
        }

        public void Delete(Guid id)
        {
            _productCategoryRepository.Delete(id);
        }

        public List<ProductCategoryViewModel> GetAll()
        {
            return _productCategoryRepository.GetAll().OrderBy(x => x.ParentId)
                .ProjectTo<ProductCategoryViewModel>()
                .ToList();
        }

        public List<ProductCategoryViewModel> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _productCategoryRepository.GetAll().Where(x => x.Name.Contains(keyword)
                || x.Description.Contains(keyword))
                    .OrderBy(x => x.ParentId).ProjectTo<ProductCategoryViewModel>().ToList();
            return _productCategoryRepository.GetAll().OrderBy(x => x.ParentId)
                .ProjectTo<ProductCategoryViewModel>()
                .ToList();
        }

        public List<ProductCategoryViewModel> GetAllByParentId(Guid? parentId)
        {
            return _productCategoryRepository.GetAll().Where(x => x.Status == Status.Actived && x.ParentId == parentId)
                .ProjectTo<ProductCategoryViewModel>()
                .ToList();
        }

        public ProductCategoryViewModel GetById(Guid id)
        {
            return Mapper.Map<ProductCategory, ProductCategoryViewModel>(_productCategoryRepository.Get(id));
        }

        public List<ProductCategoryViewModel> GetHomeCategories(int top)
        {
            var query = _productCategoryRepository.GetAll().Where(x => x.HomeFlag == true)
                .OrderBy(x => x.HomeOrder).Take(top).ProjectTo<ProductCategoryViewModel>();
            var categories = query.ToList();
            foreach (var category in categories)
            {
                category.Products = _productRepository
                    .GetAll().Where(x => x.CategoryId == category.Id)
                    .OrderByDescending(x => x.DateCreated)
                    .Take(5)
                    .ProjectTo<ProductViewModel>().ToList();
            }
            return categories;
        }

        public void ReOrder(Guid sourceId, Guid targetId)
        {
            var source = _productCategoryRepository.Get(sourceId);
            var target = _productCategoryRepository.Get(targetId);
            int tempOrder = source.SortOrder;

            source.SortOrder = target.SortOrder;
            target.SortOrder = tempOrder;

            _productCategoryRepository.Update(source);
            _productCategoryRepository.Update(target);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductCategoryViewModel productCategoryVm)
        {
            if (string.IsNullOrEmpty(productCategoryVm.SeoAlias))
                productCategoryVm.SeoAlias = TextHelper.ToUnsignString(productCategoryVm.Name);

            var productCategory = Mapper.Map<ProductCategoryViewModel, ProductCategory>(productCategoryVm);

            _productCategoryRepository.Update(productCategory);
        }

        public void UpdateParentId(Guid sourceId, Guid targetId, Dictionary<Guid, int> items)
        {
            //Update parent id for source
            var category = _productCategoryRepository.Get(sourceId);
            category.ParentId = targetId;
            _productCategoryRepository.Update(category);

            //Get all sibling
            var sibling = _productCategoryRepository.GetAll().Where(x => items.ContainsKey(x.Id));
            foreach (var child in sibling)
            {
                child.SortOrder = items[child.Id];
                _productCategoryRepository.Update(child);
            }
        }
    }
}