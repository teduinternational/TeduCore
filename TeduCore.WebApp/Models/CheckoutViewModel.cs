using System;
using System.Collections.Generic;
using System.Linq;
using TeduCore.Data.Enums;
using TeduCore.Services.ViewModels;
using TeduCore.Services.ViewModels.Common;
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