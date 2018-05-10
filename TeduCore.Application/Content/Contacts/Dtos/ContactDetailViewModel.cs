using System.ComponentModel.DataAnnotations;
using TeduCore.Infrastructure.Enums;

namespace TeduCore.Application.Content.Contacts.Dtos
{
    public class ContactDetailViewModel
    {
        public string Id { set; get; }

        [Required(ErrorMessage = "Tên không được trống")]
        public string Name { set; get; }

        [MaxLength(50, ErrorMessage = "Số điện thoại không vượt quá 50 ký tự")]
        public string Phone { set; get; }

        [MaxLength(250, ErrorMessage = "Email không vượt quá 50 ký tự")]
        public string Email { set; get; }

        [MaxLength(250, ErrorMessage = "Website không vượt quá 50 ký tự")]
        public string Website { set; get; }

        [MaxLength(250, ErrorMessage = "Địa chỉ không vượt quá 50 ký tự")]
        public string Address { set; get; }

        public string Other { set; get; }

        public double? Lat { set; get; }

        public double? Lng { set; get; }

        public Status Status { set; get; }
    }
}