using AutoMapper;
using VetClinic.API.DTO;
using VetClinic.API.DTO.Service;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Mapping
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Service, ServiceDTO>().ReverseMap();
            CreateMap<Service, ServiceCreateDTO>().ReverseMap();
            CreateMap<Service, ServiceUpdateDTO>().ReverseMap();
        }
    }
}
