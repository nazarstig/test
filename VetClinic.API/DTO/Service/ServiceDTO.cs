using System.Collections.Generic;
using VetClinic.DAL.Entities;

namespace VetClinic.API.DTO.Service
{
    public class ServiceDTO
    {  
        public string ServiceName { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
    }
}
