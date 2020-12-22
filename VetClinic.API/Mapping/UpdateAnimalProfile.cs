using AutoMapper;
using VetClinic.API.DTO;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Mapping
{
    public class UpdateAnimalProfile :Profile
    {
        public UpdateAnimalProfile()
        {
            CreateMap<Animal, UpdateAnimalDto>()
                .ForMember(d => d.Name, t => t.MapFrom(o => o.Name))
                .ForMember(d => d.Age, t => t.MapFrom(o => o.Age))
                .ForMember(d => d.Photo, t => t.MapFrom(o => o.Photo))
                .ForMember(d => d.AnimalTypeId, t => t.MapFrom(o => o.AnimalTypeId))
                .ReverseMap();
        }
    }
}
