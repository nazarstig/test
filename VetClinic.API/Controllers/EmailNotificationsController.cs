using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.API.DTO.Animal;
using VetClinic.API.DTO.Queries;
using VetClinic.API.DTO.Responses;
using VetClinic.BLL.Domain;
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


        [HttpGet("{appointmentId}")]
        public async Task<IActionResult> SendAppointmentNotification(int appointmentId)
        {
            await _emailNotificationService.SendAppointmentNotifications(appointmentId);
            return NoContent();
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> SendRegistrationNotification(string username)
        {
            await _emailNotificationService.SendClientRegistrationNotification(username);
            return NoContent();
        }
    }
}
