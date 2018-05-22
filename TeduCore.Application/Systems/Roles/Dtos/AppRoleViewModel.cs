using System;
using System.ComponentModel.DataAnnotations;

namespace TeduCore.Application.Systems.Roles.Dtos
{
    public class AppRoleViewModel
    {
        public Guid Id { set; get; }

        [Required(ErrorMessage = "Bạn phải nhập tên")]
        public string Name { set; get; }

        public string Description { set; get; }

    }
}