using System.Collections.Generic;

namespace VetClinic.API.DTO
{
    public class ReadUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public IEnumerable<RoleDto> MyRoles { get; set; }
    }
}
