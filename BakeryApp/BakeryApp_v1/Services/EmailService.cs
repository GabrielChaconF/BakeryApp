using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services
{
    public interface EmailService
    {
        public Task SendEmailAsync(string email, string subject, string message);
    }
}
