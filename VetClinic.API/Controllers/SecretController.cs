using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VetClinic.API.DTO;
using VetClinic.API.Mapping;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Controllers
{
    //[Authorize]
    public class SecretController : Controller
    {

        public SecretController(IUserService userService)
        {
            UserService = userService;
        }

        public IUserService UserService { get; }

        [Route("/secret")]
        [Authorize(Roles = "admin")]
        public string Index()
        {
            return "VetClinic Secret";
        }

        [Route("/map")]
        public async Task<OkObjectResult> Map()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>());
            var mapper = new Mapper(config);
            var dto = new CreateUserDto()
            {
                UserName = "Greg",
                Email = "HouseRSA",
                PhoneNumber = "123456",
                Password = "Pass1234$",
                MyRoles = new string[] { "admin", "doctor" },
            };
            User user = mapper.Map<CreateUserDto, User>(dto);

            var (result, id) = await UserService.CreateUser(user);

            if (result) { 
                return Ok(new
                {
                    UserName = user.UserName,
                    email = user.Email,
                    phone = user.PhoneNumber,
                    pass = user.PasswordHash,
                    roles = user.MyRoles,
                    //ID = ID,
                });
            }
            else
            {
                return Ok(new
                {
                    error = "user already exists"
                });
            }
        }
    }
}
