using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.API.DTO.User;
using VetClinic.BLL.Services.Interfaces;

namespace VetClinic.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class UserController : Controller
    {
        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        public IUserService UserService { get; }

        [HttpPut("changepass/{id}")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var res = await UserService.ChangePassword(dto.Id, dto.OldPassword, dto.NewPassword);
            if (res)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPost("validate/pass")]
        public async Task<bool> ValidatePass([FromBody](string id, string oldPass) data)
        {
            bool res = await UserService.OldPasswordExists(data.id,data.oldPass);
            return res;
        }
    }


}
