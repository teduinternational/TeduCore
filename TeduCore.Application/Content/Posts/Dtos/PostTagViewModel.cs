using TeduCore.Application.Dtos;

namespace TeduCore.Application.Content.Posts.Dtos
{
    public class PostTagViewModel
    {
        public int BlogId { set; get; }

        public string TagId { set; get; }

        public PostViewModel Blog { set; get; }

        public TagViewModel Tag { set; get; }
    }
}