using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.API.DTO.User
{
    public class VerifyPasswordDto
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
    }
}
