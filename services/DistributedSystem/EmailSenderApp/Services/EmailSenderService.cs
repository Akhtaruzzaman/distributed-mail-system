using CommonServices;
using System.Net.Mail;
using System.Net;
using EmailSenderApp.Services.IServices;

namespace EmailSenderApp.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        public async Task SendEmailAsync(EmailMessage emailMessage)
        {
            //// Implement email sending logic here, e.g., using SMTP or an email API
            //var client = new SmtpClient("smtp.example.com")
            //{
            //    Credentials = new NetworkCredential("username", "password")
            //};
            //var mailMessage = new MailMessage(emailMessage.From, emailMessage.To, emailMessage.Subject, emailMessage.Body);
            //foreach (var attachment in emailMessage.Attachments)
            //{
            //    mailMessage.Attachments.Add(new Attachment(attachment));
            //}
            //await client.SendMailAsync(mailMessage);
            await Task.Delay(10000);
        }
    }
}
