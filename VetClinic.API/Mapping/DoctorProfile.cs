using AutoMapper;
using VetClinic.API.DTO.Doctor;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Mapping
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<ReadDoctorDto, Doctor>().ReverseMap()
                .ForMember(c => c.PositionName, d => d.MapFrom(p => p.Position.PositionName));
            CreateMap<ReadDoctorDto, Position>()
                .ForMember(x => x.PositionName, opt => opt.MapFrom(model => model.PositionName));

            CreateMap<CreateDoctorDto, Doctor>();
            CreateMap<CreateDoctorDto, Doctor>().ReverseMap();

            CreateMap<UpdateDoctorDto, Doctor>();
            CreateMap<UpdateDoctorDto, Doctor>().ReverseMap();
        }
    }
}
