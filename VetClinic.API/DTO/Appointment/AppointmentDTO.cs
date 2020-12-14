using System;

namespace VetClinic.API.DTO.Appointment
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public int? DoctorId { get; set; }
        public int AnimalId { get; set; }
        public int StatusId { get; set; }
        public int ServiceId { get; set; }
        public int AppointmentProcedureId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Complaints { get; set; }
        public string TreatmentDescription { get; set; }
    }
}
