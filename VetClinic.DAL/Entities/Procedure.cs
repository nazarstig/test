using System.Collections.Generic;

namespace VetClinic.DAL.Entities
{
    public class Procedure : IBaseEntity
    {
        public int Id { get; set; }
        public string ProcedureName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsSelectable { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
