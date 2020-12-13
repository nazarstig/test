using Microsoft.AspNetCore.Identity;
using System;
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

        public async Task<(bool, string)> CreateUser(User inputUser)
        {
            var user = UserManager.FindByNameAsync(inputUser.UserName).Result;
            if(user == null)
            {
                //validate input user


                //create user
                user = new User
                {
                    UserName = inputUser.UserName,
                    Email = inputUser.Email,
                    PhoneNumber = inputUser.PhoneNumber,
                };

                var result = UserManager.CreateAsync(user, inputUser.PasswordHash).Result;

                if (!result.Succeeded)
                {
                    return (false, string.Empty);
                }

                //whitelist roles
                foreach (string role in inputUser.MyRoles)
                {
                    if (RoleManager.RoleExistsAsync(role).Result)
                    {
                        _ = await UserManager.AddToRoleAsync(user, role);
                    }
                }
                
                return (true, UserManager.FindByNameAsync(user.UserName).Result.Id);
            }
            else
            {
                return (false, string.Empty);
            }
        }

        public async Task<bool> UpdateUser(User inputUser)
        {
            var user = UserManager.FindByNameAsync(inputUser.UserName).Result;

            if(user != null)
            {
                //validate input user


                user.UserName = inputUser.UserName;
                user.Email = inputUser.Email;
                user.PhoneNumber = inputUser.PhoneNumber;

                //We need to pull roles explicitly because they are in a different table
                user.MyRoles = await UserManager.GetRolesAsync(user);

                _ = await UserManager.UpdateAsync(user);

                if (!Equals(user.MyRoles,inputUser.MyRoles))
                {
                    _ = await UserManager.RemoveFromRolesAsync(user, user.MyRoles);
                    foreach (string role in inputUser.MyRoles)
                    {
                        if (RoleManager.RoleExistsAsync(role).Result)
                        {
                            _ = await UserManager.AddToRoleAsync(user, role);
                        }
                    }
                }

                _ = await UserManager.UpdateSecurityStampAsync(user);

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool Equals(IEnumerable<string> arr1, IEnumerable<string> arr2)
        {
            return !arr1.Except(arr2).Any() && !arr2.Except(arr1).Any();
        }
    }
}
