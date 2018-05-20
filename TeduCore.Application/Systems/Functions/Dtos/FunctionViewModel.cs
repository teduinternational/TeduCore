using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeduCore.Infrastructure.Enums;

namespace TeduCore.Application.Systems.Functions.Dtos
{
    public class FunctionViewModel
    {
        public FunctionViewModel()
        {
            ChildFunctions = new List<FunctionViewModel>();
        }

        public Guid Id { set; get; }

        [Required]
        [MaxLength(50)]
        public string Name { set; get; }

        [Required]
        [MaxLength(256)]
        public string URL { set; get; }

        public int DisplayOrder { set; get; }

        public Guid? ParentId { set; get; }

        public Status Status { set; get; }

        public string IconCss { get; set; }

        public List<FunctionViewModel> ChildFunctions { get; set; }
    }
}