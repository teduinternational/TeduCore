using System.Collections.Generic;
using TeduCore.Application.Content.Posts.Dtos;
using TeduCore.Application.Content.Slides.Dtos;
using TeduCore.Application.ECommerce.ProductCategories.Dtos;
using TeduCore.Application.ECommerce.Products.Dtos;

namespace TeduCore.WebApp.Models
{
    public class HomeViewModel
    {
        public List<PostViewModel> LastestBlogs { get; set; }
        public List<SlideViewModel> HomeSlides { get; set; }
        public List<ProductViewModel> HotProducts { get; set; }
        public List<ProductViewModel> TopSellProducts { get; set; }

        public List<ProductCategoryViewModel> HomeCategories { set; get; }

        public string Title { set; get; }
        public string MetaKeyword { set; get; }
        public string MetaDescription { set; get; }
    }
}