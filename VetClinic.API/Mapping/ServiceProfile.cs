using AutoMapper;
using VetClinic.API.DTO;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Mapping
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Service, ServiceDTO>().ReverseMap();

        }
    }
}
