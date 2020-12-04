using System.Collections.Generic;

namespace VetClinic.DAL.Entities
{
    public class Doctor : User
    {
        public int DoctorId { get; set; }
        public string Education { get; set; }
        public string Biography { get; set; }
        public string Experience { get; set; }
        public string Photo { get; set; }
        public int PositionId { get; set; }

        public Position Position { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }  
}
