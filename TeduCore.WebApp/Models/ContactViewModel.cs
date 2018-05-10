using TeduCore.Application.Content.Contacts.Dtos;
using TeduCore.Application.Content.Feedbacks.Dtos;

namespace TeduCore.WebApp.Models
{
    public class ContactViewModel
    {
        public ContactDetailViewModel ContactDetail { set; get; }

        public FeedbackViewModel Feedback { set; get; }
    }
}