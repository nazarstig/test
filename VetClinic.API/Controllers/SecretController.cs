using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace VetClinic.API.Controllers
{
    [Authorize]
    public class SecretController : Controller
    {
        [Route("/secret")]
        [Authorize(Roles ="admin")]
        public string Index()
        {
            return "VetClinic Secret";
        }
    }
}
