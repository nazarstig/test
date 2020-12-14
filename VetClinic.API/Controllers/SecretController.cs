using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetClinic.BLL.Services.Interfaces;

namespace VetClinic.API.Controllers
{
    [Authorize]
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
    }
}
