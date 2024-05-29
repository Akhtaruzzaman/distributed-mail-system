using CommonServices;

namespace PosApp.Services.IServices
{
    public interface IEmailRequestService
    {
        void SendEmailRequest(EmailMessage emailMessage);
    }
}
