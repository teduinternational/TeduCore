using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeduCore.WebApp.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);

        Task SendEmailAsync(string email,string bcc, string subject, string message);
    }
}
