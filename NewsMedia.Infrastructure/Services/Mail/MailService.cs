using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace NewsMedia.Infrastructure.Services.Mail
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendMessage(string to, string message)
        {
            MailMessage mail = new MailMessage();
            mail.Subject = _configuration["EmailCredintials:Subject"];
            mail.Body = message;
            mail.From = new MailAddress(_configuration["EmailCredintials:Email"], _configuration["EmailCredintials:Name"], System.Text.Encoding.UTF8);
            mail.To.Add(to);
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential(_configuration["EmailCredintials:Email"], _configuration["EmailCredintials:Password"]);
            smtp.Port = Int32.Parse(_configuration["EmailCredintials:Port"]);
            smtp.Host = _configuration["EmailCredintials:Host"];
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(mail);
        }
    }
}
