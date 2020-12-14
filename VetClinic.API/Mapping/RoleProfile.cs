using AutoMapper;
using Microsoft.AspNetCore.Identity;
using VetClinic.API.DTO;

namespace VetClinic.API.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleDto, IdentityRole>().ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForAllOtherMembers(o => o.Ignore());
        }
    }
}
