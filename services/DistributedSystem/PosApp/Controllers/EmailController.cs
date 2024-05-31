using CommonServices;
using Microsoft.AspNetCore.Mvc;
using PosApp.Services.IServices;

namespace PosApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IEmailRequestService _emailRequestService;

        public EmailController(ILogger<EmailController> logger, IEmailRequestService emailRequestService)
        {
            _logger = logger;
            this._emailRequestService = emailRequestService;
        }

        [HttpPost(Name = "SendMail")]
        public IActionResult Post(EmailMessage emailMessage)
        {
            _emailRequestService.SendEmailRequest(emailMessage);
            return Ok();
        }
    }
}
