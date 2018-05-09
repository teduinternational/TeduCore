using System.ComponentModel.DataAnnotations;

namespace TeduCore.WebApp.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Bạn phải nhập", AllowEmptyStrings = false)]
        [Display(Name = "Tên đầy đủ")]
        public string FullName { set; get; }

        [Display(Name = "Ngày sinh")]
        public string BirthDay { set; get; }

        [Required(ErrorMessage = "Bạn phải nhập", AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        public string Email { set; get; }

        [Required(ErrorMessage = "Bạn phải nhập", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có 6 ký tự trở lên")]
        [Display(Name = "Mật khẩu")]
        public string Password { set; get; }

        [Required(ErrorMessage = "Bạn phải nhập xác nhận mật khẩu", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng")]
        public string ConfirmPassword { set; get; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Điện thoại")]
        public string PhoneNumber { set; get; }

        [Display(Name = "Ảnh đại diện")]
        public string Avatar { get; set; }
    }
}