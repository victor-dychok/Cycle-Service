using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using NotificationApplication;

namespace NotificationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : Controller
    {
        [AllowAnonymous]
        [HttpPost("Send email")]
        public async Task<IActionResult> SendMessage(NotificationApplication.IEmailSender sender,
            string email, 
            string subject,
            string message)
        {
            NotificationApplication.IEmailSender emailSender = sender;
            await emailSender.SendEmailAsync(email, subject, message);
            return Ok();
        }
    }
}
