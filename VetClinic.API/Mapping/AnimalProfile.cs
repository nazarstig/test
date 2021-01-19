using AutoMapper;
using VetClinic.API.DTO.Animal;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Mapping
{
    public class AnimalProfile : Profile
    {
        public AnimalProfile()
        {
            CreateMap<CreateAnimalDto, Animal>()
                .ForMember(d => d.Name, t => t.MapFrom(o => o.Name))
                .ForMember(d => d.Age, t => t.MapFrom(o => o.Age))
                .ForMember(d => d.AnimalTypeId, t => t.MapFrom(o => o.AnimalTypeId))
                .ForMember(d => d.ClientId, t => t.MapFrom(o => o.ClientId))
                .ForMember(d => d.Photo, t => t.MapFrom(o => o.Photo))
                .ForAllOtherMembers(d => d.Ignore());

            CreateMap<ReadAnimalDto,Animal>()
                .ForMember(d => d.Id, t => t.MapFrom(o => o.Id))
                .ForMember(d => d.Name, t => t.MapFrom(o => o.Name))
                .ForMember(d => d.Age, t => t.MapFrom(o => o.Age))
                .ForMember(d => d.AnimalTypeId, t => t.MapFrom(o => o.AnimalTypeId))
                .ForMember(d => d.ClientId, t => t.MapFrom(o => o.ClientId))
                .ForMember(d => d.Photo, t => t.MapFrom(o => o.Photo))
                .ForAllOtherMembers(d => d.Ignore());
        }
    }
}
