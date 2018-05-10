using TeduCore.Application.Dtos;

namespace TeduCore.Application.Content.Blogs.Dtos
{
    public class BlogTagViewModel
    {
        public int BlogId { set; get; }

        public string TagId { set; get; }

        public BlogViewModel Blog { set; get; }

        public TagViewModel Tag { set; get; }
    }
}