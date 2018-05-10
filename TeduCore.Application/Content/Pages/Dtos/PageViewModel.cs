using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TeduCore.Infrastructure.Enums;

namespace TeduCore.Application.Content.Pages.Dtos
{
    public class PageViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [MaxLength(256)]
        [Required]
        public string Alias { set; get; }

        public string Content { set; get; }
        public Status Status { set; get; }
    }
}
