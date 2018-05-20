using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCore.Data.Interfaces;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.Entities.ECommerce
{
    [Table("EComCouponDescriptions")]
    public class CouponDescription : DomainEntity<Guid>, IMultiLanguage<Guid>
    {
        public CouponDescription()
        {
        }

        public CouponDescription(Guid couponId, Guid languageId, string name)
        {
            CouponId = couponId;
            LanguageId = languageId;
            Name = name;
        }

        [Required]
        public Guid CouponId { set; get; }

        [Required]
        public Guid LanguageId { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }
    }
}