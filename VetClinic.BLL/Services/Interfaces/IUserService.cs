﻿using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL.Services.Interfaces
{
    public interface IUserService
    {
        public Task<(bool, string)> CreateUserAsync(User inputUser, params IdentityRole[] inputRoles);
        public Task<bool> UpdateUserAsync(string id, User inputUser, params IdentityRole[] inputRoles);
        public Task<bool> DeleteUserAsync(string id);
    }
}
