using AutoMapper;
using VetClinic.API.DTO;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Mapping
{
    public class DoctorProfile: Profile
    {
        public DoctorProfile()
        {
            CreateMap<ReadDoctorDto, Doctor>();
            CreateMap<ReadDoctorDto, Doctor>().ReverseMap();

            CreateMap<CreateDoctorDto, Doctor>();
            CreateMap<CreateDoctorDto, Doctor>().ReverseMap();
        }
    }
}
