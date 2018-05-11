using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using TeduCore.Application.Content.Contacts;
using TeduCore.Application.Content.Feedbacks;
using TeduCore.WebApp.Models;
using TeduCore.WebApp.Services;

namespace TeduCore.WebApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IFeedbackService _feedbackService;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly IViewRenderService _viewRenderService;

        public ContactController(IContactService contactSerivce,
            IViewRenderService viewRenderService,
            IConfiguration configuration,
            IEmailSender emailSender, IFeedbackService feedbackService)
        {
            _contactService = contactSerivce;
            _feedbackService = feedbackService;
            _emailSender = emailSender;
            _configuration = configuration;
            _viewRenderService = viewRenderService;
        }

        [Route("lien-he.html", Name = "Contact")]
        public IActionResult Index()
        {
            var contact = _contactService.GetById("default");
            var model = new ContactViewModel { ContactDetail = contact };
            return View(model);
        }

        [Route("lien-he.html", Name = "Contact")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Index(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _feedbackService.Add(model.Feedback);
                _feedbackService.Save();
                var content = await _viewRenderService.RenderToStringAsync("Contact/_ContactMail", model.Feedback);
                await _emailSender.SendEmailAsync(_configuration["MailSettings:AdminMail"], "Có liên hệ mới", content);
                ViewData["Success"] = true;
            }

            model.ContactDetail = _contactService.GetById("default");

            return View("Index", model);
        }
    }
}