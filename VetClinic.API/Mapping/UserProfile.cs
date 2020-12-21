using AutoMapper;
using VetClinic.API.DTO;
using VetClinic.API.DTO.UserDto;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>().ForMember(d => d.UserName, o => o.MapFrom(s => s.UserName))
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.PhoneNumber))
                .ForMember(d => d.PasswordHash, o => o.MapFrom(s => s.Password))
                .ForAllOtherMembers(o => o.Ignore());

            CreateMap<UpdateUserDto, User>().ForMember(d => d.UserName, o => o.MapFrom(s => s.UserName))
               .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
               .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName))
               .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
               .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.PhoneNumber))
               .ForMember(d => d.PasswordHash, o => o.MapFrom(s => s.Password))
               .ForAllOtherMembers(o => o.Ignore());
        }
    }
}
