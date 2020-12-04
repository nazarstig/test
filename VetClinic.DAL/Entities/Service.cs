using System;
using System.Collections.Generic;
using System.Text;

namespace VetClinic.DAL.Entities
{
    class Service : IBaseEntity
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
