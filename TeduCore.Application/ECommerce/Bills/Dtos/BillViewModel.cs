using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeduCore.Application.Systems.Users.Dtos;
using TeduCore.Data.Enums;
using TeduCore.Infrastructure.Enums;

namespace TeduCore.Application.ECommerce.Bills.Dtos
{
    public class BillViewModel
    {
        public Guid Id { set; get; }

        [Required]
        [MaxLength(256)]
        public string CustomerName { set; get; }

        [Required]
        [MaxLength(256)]
        public string CustomerAddress { set; get; }

        [Required]
        [MaxLength(50)]
        public string CustomerMobile { set; get; }

        [Required]
        [MaxLength(256)]
        public string CustomerMessage { set; get; }

        public PaymentMethod PaymentMethod { set; get; }

        public BillStatus BillStatus { set; get; }

        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }

        public Status Status { set; get; }
        public string CustomerFacebook { set; get; }

        public decimal? ShippingFee { set; get; }
        public string CustomerId { set; get; }

        public AppUserViewModel User { set; get; }

        public List<BillDetailViewModel> BillDetails { set; get; }
    }
}