using AutoMapper;
using AutoMapper.QueryableExtensions;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TeduCore.Application.Dtos;
using TeduCore.Application.ECommerce.Products.Dtos;
using TeduCore.Data.Entities;
using TeduCore.Data.Entities.ECommerce;
using TeduCore.Data.Enums;
using TeduCore.Infrastructure.Enums;
using TeduCore.Infrastructure.Interfaces;
using TeduCore.Utilities.Dtos;
using TeduCore.Utilities.Helpers;

namespace TeduCore.Application.ECommerce.Products
{
    public class ProductService : WebServiceBase<Product, Guid, ProductViewModel>, IProductService
    {
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IRepository<Tag, string> _tagRepository;
        private readonly IRepository<ProductTag, Guid> _productTagRepository;
        private readonly IRepository<ProductImage, Guid> _productImageRepository;
        private readonly IRepository<ProductCategory, Guid> _productCategoryRepository;
        private readonly IRepository<ProductWishlist, Guid> _productWishlistRepository;

        public ProductService(IRepository<Product, Guid> productRepository,
            IRepository<ProductTag, Guid> productTagRepository,
            IRepository<ProductWishlist, Guid> productWishlistRepository,
            IRepository<Tag, string> tagRepository,
            IRepository<ProductCategory, Guid> productCategoryRepository,
            IRepository<ProductImage, Guid> productImageRepository,
            IUnitOfWork unitOfWork)
            : base(productRepository, unitOfWork)
        {
            _productRepository = productRepository;
            _productTagRepository = productTagRepository;
            _productImageRepository = productImageRepository;
            _productCategoryRepository = productCategoryRepository;
            _tagRepository = tagRepository;
            _productWishlistRepository = productWishlistRepository;

        }

        public override void Add(ProductViewModel productVm)
        {
            var product = Mapper.Map<ProductViewModel, Product>(productVm);
           

            if (!string.IsNullOrEmpty(productVm.Tags))
            {
                string[] tags = productVm.Tags.Split(',');
                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.GetAll().Where(x => x.Id == tagId).Any())
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = TagType.Product
                        };
                        _tagRepository.Insert(tag);
                    }

                    ProductTag productTag = new ProductTag
                    {
                        TagId = tagId
                    };
                    //product.ProductTags.Insert(productTag);
                }
            }
            _productRepository.Insert(product);
        }

        public PagedResult<ProductViewModel> GetAllPaging(Guid? categoryId, string keyword, int page, int pageSize, string sortBy)
        {
            var query = _productRepository.GetAll().Where(c => c.Status == Status.Actived);
           

            int totalRow = query.Count();
            switch (sortBy)
            {
                case "price":
                    query = query.OrderByDescending(x => x.Price);
                    break;

               

                case "lastest":
                    query = query.OrderByDescending(x => x.DateCreated);
                    break;

                default:
                    query = query.OrderByDescending(x => x.DateCreated);
                    break;
            }
            query = query.Skip((page - 1) * pageSize)
                .Take(pageSize);

            var data = query.ProjectTo<ProductViewModel>().ToList();
            var paginationSet = new PagedResult<ProductViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public override void Update(ProductViewModel productVm)
        {
            var product = Mapper.Map<ProductViewModel, Product>(productVm);
            _productTagRepository.Delete(x => x.Id == product.Id);

            if (!string.IsNullOrEmpty(productVm.Tags))
            {
                string[] tags = productVm.Tags.Split(',');
                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.GetAll().Where(x => x.Id == tagId).Any())
                    {
                        Tag tag = new Tag();
                        tag.Id = tagId;
                        tag.Name = t;
                        tag.Type = TagType.Product;
                        _tagRepository.Insert(tag);
                    }
                    ProductTag productTag = new ProductTag
                    {
                        TagId = tagId
                    };
                    //product.ProductTags.Insert(productTag);
                }
            }
            _productRepository.Update(product);
        }

        public List<ProductViewModel> GetLastest(int top)
        {
            return _productRepository.GetAll().Where(x => x.Status == Status.Actived).OrderByDescending(x => x.DateCreated)
                .Take(top).ProjectTo<ProductViewModel>().ToList();
        }

        public List<ProductViewModel> GetHotProduct(int top)
        {
            return _productRepository.GetAll().Where(x => x.Status == Status.Actived)
                .OrderByDescending(x => x.DateCreated)
                .Take(top)
                .ProjectTo<ProductViewModel>()
                .ToList();
        }

        public List<ProductViewModel> GetListProductByCategoryIdPaging(Guid categoryId, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _productRepository.GetAll().Where(x => x.Status == Status.Actived);

            switch (sort)
            {
              

                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;

                default:
                    query = query.OrderByDescending(x => x.DateCreated);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<ProductViewModel>().ToList();
        }

        public List<string> GetListProductByName(string name)
        {
            return _productRepository.GetAll().Where(x => x.Status == Status.Actived).Select(y => y.Model).ToList();
        }

        public List<ProductViewModel> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _productRepository.GetAll().Where(x => x.Status == Status.Actived);

            switch (sort)
            {
                

                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;

                default:
                    query = query.OrderByDescending(x => x.DateCreated);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<ProductViewModel>()
                .ToList();
        }

        public List<ProductViewModel> GetReatedProducts(Guid id, int top)
        {
            var product = _productRepository.GetById(id);
            return _productRepository.GetAll().Where(x => x.Status == Status.Actived
                && x.Id != id )
            .OrderByDescending(x => x.DateCreated)
            .Take(top)
            .ProjectTo<ProductViewModel>()
            .ToList();
        }

        public List<TagViewModel> GetListTagByProductId(Guid id)
        {
            return _productTagRepository.GetAll().Where(x => x.ProductId == id)
                //.Select(y => y.Tag)
                .ProjectTo<TagViewModel>()
                .ToList();
        }

        public void IncreaseView(Guid id)
        {
            var product = _productRepository.GetById(id);
           
        }

        public List<ProductViewModel> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow)
        {
            var query = from p in _productRepository.GetAll()
                        join pt in _productTagRepository.GetAll()
                        on p.Id equals pt.ProductId
                        where pt.TagId == tagId
                        select p;
            totalRow = query.Count();

            var model = query.OrderByDescending(x => x.DateCreated)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ProjectTo<ProductViewModel>();
            return model.ToList();
        }

        public TagViewModel GetTag(string tagId)
        {
            return Mapper.Map<Tag, TagViewModel>(_tagRepository.Single(x => x.Id == tagId));
        }


        public List<ProductViewModel> GetListProduct(string keyword)
        {
            IQueryable<ProductViewModel> query;
            if (!string.IsNullOrEmpty(keyword))
                query = _productRepository.GetAll().ProjectTo<ProductViewModel>();
            else
                query = _productRepository.GetAll().ProjectTo<ProductViewModel>();
            return query.ToList();
        }

        public List<TagViewModel> GetListProductTag(string searchText)
        {
            return _tagRepository.GetAll().Where(x => x.Type == TagType.Product
            && searchText.Contains(x.Name)).ProjectTo<TagViewModel>().ToList();
        }

        public void ImportExcel(string filePath, Guid categoryId)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                Product product;
                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    product = new Product();
                   
                    decimal.TryParse(workSheet.Cells[i, 3].Value.ToString(), out var originalPrice);
                    decimal.TryParse(workSheet.Cells[i, 4].Value.ToString(), out var price);
                    product.Price = price;
                    decimal.TryParse(workSheet.Cells[i, 5].Value.ToString(), out var promotionPrice);

               
                    bool.TryParse(workSheet.Cells[i, 9].Value.ToString(), out var hotFlag);
                
                    bool.TryParse(workSheet.Cells[i, 10].Value.ToString(), out var homeFlag);
                    

                    product.Status = Status.Actived;

                    _productRepository.Insert(product);
                }
            }
        }

        public void AddImages(Guid productId, string[] images)
        {
            _productImageRepository.Delete(x => x.ProductId == productId);
            foreach (var image in images)
            {
                _productImageRepository.Insert(new ProductImage()
                {
                    Path = image,
                    ProductId = productId,
                    Caption = string.Empty
                });
            }
        }

        public List<ProductImageViewModel> GetImages(Guid productId)
        {
            return _productImageRepository.GetAll().Where(x => x.ProductId == productId)
                .ProjectTo<ProductImageViewModel>().ToList();
        }

        public List<ProductViewModel> GetUpsellProducts(int top)
        {
            return _productRepository.GetAll()
                .OrderByDescending(x => x.DateModified)
                .Take(top)
                .ProjectTo<ProductViewModel>().ToList();
        }

        public PagedResult<ProductViewModel> GetMyWishlist(Guid userId, int page, int pageSize)
        {
            var query = _productWishlistRepository.GetAll().Where(c => c.UserId == userId);

            int totalRow = query.Count();

            query = query.Skip((page - 1) * pageSize)
                .Take(pageSize);

            var data = query.ProjectTo<ProductViewModel>().ToList();
            var paginationSet = new PagedResult<ProductViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }
    }
}