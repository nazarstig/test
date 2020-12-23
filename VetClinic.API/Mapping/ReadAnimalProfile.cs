using AutoMapper;
using VetClinic.API.DTO.Animal;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Mapping
{
    public class ReadAnimalProfile : Profile
    {
        public ReadAnimalProfile()
        {
            CreateMap<ReadAnimalDto, Animal>()
                .ForMember(d => d.Name, t => t.MapFrom(o => o.Name))
                .ForMember(d => d.Age, t => t.MapFrom(o => o.Age))
                .ForMember(d => d.Photo, t => t.MapFrom(o => o.Photo))
                .ReverseMap();
        }
    }
}
