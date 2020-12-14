using System.Collections.Generic;
using VetClinic.API.DTO.Appointment;

namespace VetClinic.API.DTO
{
    public class ServiceDTO
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }

        public ICollection<AppointmentDTO> Appointments { get; set; }
    }
}
