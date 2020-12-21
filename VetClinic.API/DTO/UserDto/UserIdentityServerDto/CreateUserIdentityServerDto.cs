using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.API.DTO.UserDto.UserIdentityServerDto
{
    public class CreateUserIdentityServerDto : CreateUserDto
    {
        public IEnumerable<RoleDto> MyRoles { get; set; }
    }
}
