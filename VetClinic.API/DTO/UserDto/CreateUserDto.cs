using System.Collections.Generic;
using VetClinic.API.DTO.UserDto;

namespace VetClinic.API.DTO
{
    public class CreateUserDto : BaseUserDto
    {
        public string Password { get; set; }
    }
}
