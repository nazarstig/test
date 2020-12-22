using AutoMapper;
using VetClinic.API.DTO;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Mapping
{
    public class AnimalTypeProfile:Profile
    {
        public AnimalTypeProfile()
        {
            CreateMap<ReadAnimalTypeDto, AnimalType>()
                .ReverseMap()
                .ForMember(d => d.AnimalTypeName, t => t.MapFrom(o => o.AnimalTypeName))
                .ForAllOtherMembers(d => d.Ignore());
        }
    }
}
