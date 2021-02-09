using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.API.DTO.Animal;
using VetClinic.API.DTO.Queries;
using VetClinic.API.DTO.Responses;
using VetClinic.BLL.Domain;
using VetClinic.BLL.Email;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailNotificationsController : ControllerBase
    {
        private readonly IEmailNotificationService _emailNotificationService;

        public EmailNotificationsController(IEmailNotificationService emailNotificationService)
        {
            _emailNotificationService = emailNotificationService;
        }


        [HttpGet("appointments/{appointmentId}")]
        public async Task<IActionResult> SendAppointmentNotification(int appointmentId)
        {
            await _emailNotificationService.SendAppointmentNotifications(appointmentId);
            return NoContent();
        }

        [HttpPost("appointments/receipt")]
        public async Task<IActionResult> SendAppointmentsReceipt(int clientId, int appointmentId)
        {
            await _emailNotificationService.SendClientAppointmentsReceipt(clientId, appointmentId);
            return NoContent();
        }

        [HttpGet("registration/{username}")]
        public async Task<IActionResult> SendRegistrationNotification(string username)
        {
            await _emailNotificationService.SendClientRegistrationNotification(username);
            return NoContent();
        }

        [HttpGet("services/{serviceId}")]
        public async Task<IActionResult> SendNewServiceNotification(int serviceId)
        {
            await _emailNotificationService.SendNewServiceNotification(serviceId);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> SendWrittenNotification(string subject, string message)
        {
            EmailModel email = new EmailModel
            {
                Subject = subject,
                Message = message
            };
            await _emailNotificationService.SendNotificationToAllUsers(email);
            return NoContent();
        }

    }
}
