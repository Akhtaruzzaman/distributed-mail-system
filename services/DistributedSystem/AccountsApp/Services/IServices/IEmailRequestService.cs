using CommonServices;

namespace AccountsApp.Services.IServices
{
    public interface IEmailRequestService
    {
        void SendEmailRequest(EmailMessage emailMessage);
    }
}
