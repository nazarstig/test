using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace VetClinic.DAL.Entities
{
    public class User : IdentityUser
    {
        public IEnumerable<string> MyRoles { get; set; }
    }
}
