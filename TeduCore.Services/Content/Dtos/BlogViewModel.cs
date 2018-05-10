using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeduCore.Infrastructure.Enums;

namespace TeduCore.Services.ViewModels.Blog
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [MaxLength(256)]
        public string Image { set; get; }

        [MaxLength(500)]
        public string Description { set; get; }

        public string Content { set; get; }

        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }

        public string Tags { get; set; }

        public ICollection<BlogTagViewModel> BlogTags { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public Status Status { set; get; }

        public string SeoPageTitle { set; get; }
        public string SeoAlias { set; get; }
        public string SeoKeywords { set; get; }
        public string SeoDescription { set; get; }
    }
}