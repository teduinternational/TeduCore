using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeduCore.WebApp.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="không được trống")]
        [Display(Name ="Tài khoản")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "không được trống")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Ghi nhớ mật khẩu?")]
        public bool RememberMe { get; set; }
    }
}
