using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using MimeKit;

namespace Vue
{
    public class Feedback
    {
        public async Task SendEmailAsync(string Email, string subject, string message, string ContactEmail, string ContactPassword, int SmtpPort, string SmtpServer)
        {
            var EmailMessage = new MimeMessage();

            EmailMessage.From.Add(new MailboxAddress(Email, Email));
            EmailMessage.To.Add(new MailboxAddress("Me", ContactEmail));
            EmailMessage.Subject = subject;
            EmailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var smtpClient = new SmtpClient())
            {
                await smtpClient.ConnectAsync(SmtpServer, SmtpPort, false);
                await smtpClient.AuthenticateAsync(ContactEmail, ContactPassword);
                await smtpClient.SendAsync(EmailMessage);

                await smtpClient.DisconnectAsync(true);
            }
        }
    }
    public class EmailProperties
    {
        public string ContactEmail { get; set; }
        public string ContactPassword { get; set; }
        public string SmtpPort { get; set; }
        public string SmtpServer { get; set; }
    }
}
