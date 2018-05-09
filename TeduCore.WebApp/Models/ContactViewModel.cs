using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCore.Services.ViewModels;

namespace TeduCore.WebApp.Models
{
    public class ContactViewModel
    {
        public ContactDetailViewModel ContactDetail { set; get; }

        public FeedbackViewModel Feedback { set; get; }
    }
}
