using System.Collections.Generic;

namespace VetClinic.DAL.Entities
{
    public class AppointmentProcedures : IBaseEntity
    {
        public int Id { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Procedure> Procedures { get; set; }
    }
}
