using System.Collections.Generic;
using TeduCore.Application.Content.Blogs.Dtos;

namespace TeduCore.WebApp.Models
{
    public class BlogDetailViewModel
    {
        public BlogViewModel Blog { get; set; }

        public List<BlogViewModel> MostBlogs { get; set; }
    }
}