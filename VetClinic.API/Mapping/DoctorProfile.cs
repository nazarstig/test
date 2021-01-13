using AutoMapper;
using System.Linq;
using VetClinic.API.DTO.Doctor;
using VetClinic.API.DTO.Doctor.SecondaryDto;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Mapping
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<Client, ClientDto>();
            CreateMap<Animal, AnimalDto>();
            CreateMap<Service, ServiceDto>();
            CreateMap<Status, StatusDto>();            
            CreateMap<Procedure, ProcedureDto>();

            CreateMap<Appointment, AppointmentDto>()
               .ForPath(dest => dest.Animal,
                   opt => opt.MapFrom(src => src.Animal))
               .ForPath(dest => dest.Client,
                   opt => opt.MapFrom(src => src.Animal.Client))
               .ForPath(dest => dest.Client.FirstName,
                   opt => opt.MapFrom(src => src.Animal.Client.User.FirstName))
               .ForPath(dest => dest.Client.LastName,
                   opt => opt.MapFrom(src => src.Animal.Client.User.LastName))               
               .ForPath(dest => dest.Animal.AnimalType,
                   opt => opt.MapFrom(src => src.Animal.AnimalType.AnimalTypeName))
               .ForPath(dest => dest.PerformedProcedures,
                   opt => opt.MapFrom(src => src.AppointmentProcedures.Select(ap => ap.Procedure)));

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
