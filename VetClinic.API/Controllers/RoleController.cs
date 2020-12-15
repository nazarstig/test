using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.API.Controllers
{
    public class RoleController : Controller
    {
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            RoleManager = roleManager;
        }

        public RoleManager<IdentityRole> RoleManager { get; }

        public async Task<IActionResult> Get()
        {
            return Ok(new { name ="arrgh"});
        }
    }
}
