using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TeduCore.Infrastructure.Enums;

namespace TeduCore.Application.Systems.Functions.Dtos
{
    public class FunctionViewModel
    {
        public Guid Id { set; get; }

        [Required]
        [MaxLength(50)]
        public string Name { set; get; }

        [Required]
        [MaxLength(256)]
        public string URL { set; get; }

        public int DisplayOrder { set; get; }

        public string ParentId { set; get; }

        public Status Status { set; get; }

        public string IconCss { get; set; }
    }
}
