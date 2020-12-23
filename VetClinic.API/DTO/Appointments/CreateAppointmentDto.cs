using System;

namespace VetClinic.API.DTO.Appointments
{
    public class CreateAppointmentDto
    {
        public int AnimalId { get; set; }
        public int ServiceId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Complaints { get; set; }
    }
}