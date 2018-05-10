using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TeduCore.Application.ECommerce.ProductCategories.Dtos;
using TeduCore.Application.ECommerce.Products.Dtos;
using TeduCore.Utilities.Dtos;

namespace TeduCore.WebApp.Models.ProductViewModels
{
    public class CatalogViewModel
    {
        public string BodyClass;
        public PagedResult<ProductViewModel> Data { get; set; }

        public ProductCategoryViewModel Category { set; get; }

        public List<ProductCategoryViewModel> AllCategories { get; set; }

        public List<ProductViewModel> LastestProducts { set; get; }

        public string SortType { set; get; }

        public List<SelectListItem> SortTypes { get; } = new List<SelectListItem>
        {
            new SelectListItem(){Value = "lastest",Text = "Mới nhất"},
            new SelectListItem(){Value = "price",Text = "Giá"},
            new SelectListItem(){Value = "name",Text = "Tên"},
        };

        public int? PageSize { set; get; }

        public List<SelectListItem> PageSizes { get; } = new List<SelectListItem>
        {
            new SelectListItem(){Value = "12",Text = "12"},
            new SelectListItem(){Value = "24",Text = "24"},
            new SelectListItem(){Value = "48",Text = "48"},
        };
    }
}