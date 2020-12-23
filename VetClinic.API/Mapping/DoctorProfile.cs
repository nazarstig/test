using AutoMapper;
using VetClinic.API.DTO.Doctor;
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

            CreateMap<UpdateDoctorDto, Doctor>();
            CreateMap<UpdateDoctorDto, Doctor>().ReverseMap();
        }
    }
}
