using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.BLL.Services.Interfaces;

namespace VetClinic.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(IUserService userService)
        {
            UserService = userService;
        }

        public IUserService UserService { get; }

        [HttpGet("{userName}")]
        public async Task<bool> UserNameExists(string userName)
        {
            if (userName == null)
            {
                return false;
            }
            return await UserService.UserNameExistsAsync(userName);
        }
    }
}
