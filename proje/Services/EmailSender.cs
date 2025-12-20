using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace proje.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Fake mail sender – email atmıyoruz
            return Task.CompletedTask;
        }
    }
}
