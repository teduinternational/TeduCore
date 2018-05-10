using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Data.Enums;
using TeduCore.Data.Interfaces;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities
{
    [Table("Bills")]
    public class Bill : DomainEntity<Guid>, IDateTracking, IHasUniqueCode
    {
        public Bill()
        {
        }

        public Bill(Guid id, string customerName, string customerAddress,
            string customerMobile, string customerMessage,
           BillStatus billStatus, PaymentMethod paymentMethod,
           string customerFacebook, decimal? shippingFee)
        {
            Id = id;
            CustomerName = customerName;
            CustomerAddress = customerAddress;
            CustomerMobile = customerMobile;
            CustomerMessage = customerMessage;
            BillStatus = billStatus;
            PaymentMethod = paymentMethod;
            CustomerFacebook = customerFacebook;
            ShippingFee = shippingFee;
        }

        [Required]
        [MaxLength(256)]
        public string CustomerName { set; get; }

        [Required]
        [MaxLength(256)]
        public string CustomerAddress { set; get; }

        [Required]
        [MaxLength(50)]
        public string CustomerMobile { set; get; }

        [MaxLength(256)]
        public string CustomerMessage { set; get; }

        public string CustomerFacebook { set; get; }

        public decimal? ShippingFee { set; get; }

        public PaymentMethod PaymentMethod { set; get; }

        public BillStatus BillStatus { set; get; }

        public DateTime DateCreated { set; get; }
        public DateTime? DateModified { set; get; }
        public DateTime? DateDeleted { set; get; }
        public string CustomerId { set; get; }
        public string UniqueCode { set; get; }
    }
}