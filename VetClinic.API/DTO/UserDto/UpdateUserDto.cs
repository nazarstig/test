using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.API.DTO.UserDto
{
    public class UpdateUserDto : BaseUserDto
    {
        public string Password { get; set; }
    }
}
