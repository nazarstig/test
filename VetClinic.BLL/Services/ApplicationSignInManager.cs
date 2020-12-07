using Microsoft.AspNetCore.Identity;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL.Services
{
    public class ApplicationSignInManager : SignInManager<User>
    {
        public ApplicationSignInManager() : base()
        {
            
        }
    }
}
