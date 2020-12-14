using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL.Services.Interfaces
{
    public interface IUserService
    {
        public Task<(bool, string)> CreateUser(User inputUser, IEnumerable<IdentityRole> inputRoles);
        public Task<bool> UpdateUser(User inputUser, IEnumerable<IdentityRole> inputRoles);
    }
}
