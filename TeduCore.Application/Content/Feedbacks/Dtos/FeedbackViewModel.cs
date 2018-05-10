﻿using System;
using System.ComponentModel.DataAnnotations;
using TeduCore.Application.Content.Contacts.Dtos;
using TeduCore.Infrastructure.Enums;

namespace TeduCore.Application.Content.Feedbacks.Dtos
{
    public class FeedbackViewModel
    {
        public Guid id { set; get; }

        [MaxLength(250, ErrorMessage = "Tên không được quá 250 ký tự")]
        [Required(ErrorMessage = "Tên phải nhập")]
        public string Name { set; get; }

        [MaxLength(250, ErrorMessage = "Email không được quá 250 ký tự")]
        public string Email { set; get; }

        [MaxLength(500, ErrorMessage = "Tin nhắn không được quá 500 ký tự")]
        public string Message { set; get; }

        public DateTime CreatedDate { set; get; }

        [Required(ErrorMessage = "Phải nhập trạng thái")]
        public Status Status { set; get; }

        public ContactDetailViewModel ContactDetail { set; get; }
    }
}