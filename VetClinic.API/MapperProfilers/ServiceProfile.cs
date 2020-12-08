using AutoMapper;
using VetClinic.API.DTO.Service;
using VetClinic.DAL.Entities;

namespace VetClinic.API.MapperProfilers
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Service, ServiceDTO>().ReverseMap();
            
        }
    }
}
