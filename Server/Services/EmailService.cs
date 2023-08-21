using HIVE.Shared.Request;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using HIVE.Server.Services.Interface;
using HIVE.Shared.Model;
using Microsoft.Extensions.Options;

namespace HIVE.Server.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting _emailSetting;

        public EmailService(IOptions<EmailSetting> emailSetting)
        {
            _emailSetting = emailSetting.Value;
        }
        public void SendEmail(SendMail mail)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailSetting.Username));
            email.To.Add(MailboxAddress.Parse(mail.To));
            email.Subject = mail.Subject;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = mail.Body
            };
            using var smtp = new SmtpClient();
            smtp.Connect(_emailSetting.Host, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSetting.Username, _emailSetting.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
