using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace VetClinic.DAL.Entities
{
    public partial class AppUserLogin : IdentityUserLogin<int>
    {
        public int Id { get; set; }
    }
}
