using System.Net.Mail;
using System.Net;

namespace BakeryApp_v1.Services
{
    public class EmailServiceImpl:EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("adminbakery@gmail.com", "bakeryapp.*"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("adminbakery@gmail.com"),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
