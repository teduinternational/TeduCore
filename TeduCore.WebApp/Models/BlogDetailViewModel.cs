using System.Collections.Generic;
using TeduCore.Application.Content.Posts.Dtos;

namespace TeduCore.WebApp.Models
{
    public class BlogDetailViewModel
    {
        public PostViewModel Blog { get; set; }

        public List<PostViewModel> MostBlogs { get; set; }
    }
}