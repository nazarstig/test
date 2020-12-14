using AutoMapper;
using VetClinic.API.DTO.Appointment;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Mapping
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentDTO>().ReverseMap();
        }
    }
}
