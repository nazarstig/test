using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL.Services.Realizations
{
    public class UserService : IUserService
    {
        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public UserManager<User> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }

        public async Task<(bool, string)> CreateUser(User inputUser, IEnumerable<IdentityRole> inputRoles)
        {
            var user = UserManager.FindByNameAsync(inputUser.UserName).Result;
            if(user == null)
            {
                var result = UserManager.CreateAsync(inputUser, inputUser.PasswordHash).Result;

                if (!result.Succeeded)
                {
                    return (false, string.Empty);
                }

                //whitelist roles
                    
                foreach (IdentityRole role in inputRoles)
                {
                    if (RoleManager.RoleExistsAsync(role.Name).Result)
                    {
                        _ = await UserManager.AddToRoleAsync(inputUser, role.Name);
                    }
                }
                    
                return (true, UserManager.FindByNameAsync(inputUser.UserName).Result.Id);
                
            }
            return (false, string.Empty);
        }
        
        public async Task<bool> UpdateUser(User inputUser, IEnumerable<IdentityRole> inputRoles)
        {
            var user = UserManager.FindByNameAsync(inputUser.UserName).Result;

            if(user != null)
            {

                user.UserName = inputUser.UserName;
                user.FirstName = inputUser.FirstName;
                user.LastName = inputUser.LastName;
                user.Email = inputUser.Email;
                user.PhoneNumber = inputUser.PhoneNumber;

                //We need to pull roles explicitly because they are in a different table
                var MyRoles = await UserManager.GetRolesAsync(user);

                _ = await UserManager.UpdateAsync(user);

                if (!Equals(MyRoles, inputRoles))
                {
                    _ = await UserManager.RemoveFromRolesAsync(user, MyRoles);
                    foreach (IdentityRole role in inputRoles)
                    {
                        if (RoleManager.RoleExistsAsync(role.Name).Result)
                        {
                            _ = await UserManager.AddToRoleAsync(user, role.Name);
                        }
                    }
                }

                _ = await UserManager.UpdateSecurityStampAsync(user);

                return true;
                
            }
            return false;
        }
        
        private bool Equals(IEnumerable<string> arr1, IEnumerable<IdentityRole> arr2)
        {
            IEnumerable<string> roles = arr2.Select(arr2 => arr2.Name);

            return !arr1.Except(roles).Any() && !roles.Except(arr1).Any();
        }
    }
}
