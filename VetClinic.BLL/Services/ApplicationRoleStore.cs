using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using VetClinic.DAL;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL.Services
{
    public class ApplicationRoleStore : RoleStore<AppRole, ApplicationContext, int, AppUserRole, AppRoleClaim>
    {

    }
}
