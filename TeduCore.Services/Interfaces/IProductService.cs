using System.Collections.Generic;
using TeduCore.Services.ViewModels;
using TeduCore.Services.ViewModels.Product;
using TeduCore.Utilities.Dtos;

namespace TeduCore.Services.Interfaces
{
    public interface IProductService
    {
        ProductViewModel Add(ProductViewModel product);

        void Update(ProductViewModel product);

        void Delete(int id);

        List<ProductViewModel> GetAll();

        PagedResult<ProductViewModel> GetAllPaging(int? categoryId, string keyword, int page, int pageSize, string sortBy);

        List<ProductViewModel> GetLastest(int top);

        List<ProductViewModel> GetHotProduct(int top);

        List<ProductViewModel> GetListProductByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow);

        List<ProductViewModel> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        List<ProductViewModel> GetListProduct(string keyword);

        List<ProductViewModel> GetReatedProducts(int id, int top);

        List<string> GetListProductByName(string name);

        ProductViewModel GetById(int id);

        void Save();

        List<TagViewModel> GetListTagByProductId(int id);

        TagViewModel GetTag(string tagId);

        void IncreaseView(int id);

        List<ProductViewModel> GetListProductByTag(string tagId, int page, int pagesize, out int totalRow);

        bool SellProduct(int productId, int quantity);

        List<TagViewModel> GetListProductTag(string searchText);

        void ImportExcel(string filePath, int categoryId);

        void AddImages(int productId, string[] images);

        List<ProductImageViewModel> GetImages(int productId);

        void AddQuantity(int productId, List<ProductQuantityViewModel> quantities);

        List<ProductQuantityViewModel> GetQuantities(int productId);

        void AddWholePrice(int productId, List<WholePriceViewModel> wholePrices);

        List<WholePriceViewModel> GetWholePrices(int productId);

        List<ProductViewModel> GetUpsellProducts(int top);

        PagedResult<ProductViewModel> GetMyWishlist(string userId, int page, int pageSize);
    }
}