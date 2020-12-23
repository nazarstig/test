using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.API.DTO.Appointments;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IMapper mapper, IAppointmentService appointmentService)
        {
            _mapper = mapper;
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();

            var appointmentsDto = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);

            return Ok(appointmentsDto);
        }

        [HttpGet("{id}", Name = "GetAsync")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);

            var appointmentDto = _mapper.Map<AppointmentDto>(appointment);

            return Ok(appointmentDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateAppointmentDto createAppointmentDto)
        {
            var appointment = _mapper.Map<Appointment>(createAppointmentDto);

            var createdAppointment = await _appointmentService.CreateAppointmentAsync(appointment);

            var readAppointmentDto = _mapper.Map<AppointmentDto>(createdAppointment);

            return CreatedAtRoute(nameof(GetAsync), new { readAppointmentDto.Id }, readAppointmentDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] UpdateAppointmentDto updateAppointmentDto)
        {
            var updateAppointment = _mapper.Map<Appointment>(updateAppointmentDto);

            await _appointmentService.UpdateAppointmentAsync(id, updateAppointment);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await _appointmentService.DeleteAppointmentAsync(id);

            return NoContent();
        }
    }
}