using CommonServices;

namespace EmailSenderApp.Services.IServices
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(EmailMessage emailMessage);
    }
}
