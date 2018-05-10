using System;
using System.Collections.Generic;
using System.Linq;
using TeduCore.Application.ECommerce.Bills.Dtos;
using TeduCore.Data.Enums;
using TeduCore.Utilities.Dtos;
using TeduCore.Utilities.Extensions;

namespace TeduCore.WebApp.Models
{
    public class CheckoutViewModel : BillViewModel
    {
        public List<ShoppingCartViewModel> Carts { get; set; }

        public List<EnumModel> PaymentMethods
        {
            get
            {
                return ((PaymentMethod[])Enum.GetValues(typeof(PaymentMethod)))
                    .Select(c => new EnumModel
                    {
                        Value = (int)c,
                        Name = c.GetDescription()
                    }).ToList();
            }
        }
    }
}