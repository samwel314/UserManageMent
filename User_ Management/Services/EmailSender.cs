using Microsoft.AspNetCore.Identity.UI.Services;

namespace User__Management.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var from = "";
        }
    }
}
