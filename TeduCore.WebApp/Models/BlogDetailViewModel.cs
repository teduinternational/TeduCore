using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCore.Services.ViewModels.Blog;

namespace TeduCore.WebApp.Models
{
    public class BlogDetailViewModel
    {
        public BlogViewModel Blog { get; set; }

        public List<BlogViewModel> MostBlogs { get; set; }
    }
}
