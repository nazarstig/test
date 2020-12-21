using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.API.DTO.ClientDTO;
using VetClinic.API.DTO;

namespace VetClinic.API.Mapping
{
    public class UserDtoProfile : Profile
    {
        public UserDtoProfile()
        {
            //CreateMap<CreateClientDto, CreateUserDto>().
            //     ForMember(dto => dto.FirstName,
            //     opt => opt.MapFrom(src => src.FirstName)).
            //     ForMember(dto => dto.LastName,
            //     opt => opt.MapFrom(src => src.LastName)).
            //     ForMember(dto => dto.Email,
            //     opt => opt.MapFrom(src => src.Email)).
            //     ForMember(dto => dto.PhoneNumber,
            //     opt => opt.MapFrom(src => src.PhoneNumber)).
            //     ForMember(dto => dto.UserName,
            //     opt => opt.MapFrom(src => src.UserName)).
            //     ForMember(dto => dto.Password,
            //     opt => opt.MapFrom(src => src.Password));
        }
    }
}
