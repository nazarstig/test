using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;

namespace VetClinic.BLL.Services.Realizations
{
    public class DoctorService : IDoctorService
    {
        private readonly IUserService _userService;        
        private readonly IRepositoryWrapper _repositoryWrapper;
        public DoctorService(IRepositoryWrapper repositoryWrapper, IUserService userService, RoleManager<IdentityRole> roleManager)
        {            
            _repositoryWrapper = repositoryWrapper;
            _userService = userService;
        }       

        public async Task<(Doctor, User)> AddDoctor(Doctor doctor, User user)
        {
            IdentityRole identityRole = new IdentityRole() { Name = "doctor" };
            
                
                await _userService.RoleManager.CreateAsync(identityRole);            
                       
            IdentityRole[] identityRoles = { identityRole };
            
            return (doctor, user);
        }

        public async Task<(Doctor, User)> GetDoctorAsync(int doctorId)
        {
            Doctor doctor = await _repositoryWrapper.DoctorRepository.GetFirstOrDefaultAsync(d => d.Id == doctorId);
            User user = await _userService.GetUserAsync(doctor.UserId);
            return (doctor, user);
        }

        public async Task<(ICollection<Doctor>, ICollection<User>)> GetDoctorAsync()
        {
            var doctors = await _repositoryWrapper.DoctorRepository.GetAsync();
            List<User> users = new List<User>();
            foreach (Doctor doc in doctors)
            {
                users.Add(await _userService.GetUserAsync(doc.UserId));
            }
            return (doctors, users);

        }

        public Task<bool> RemoveDoctor(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateDoctor(Doctor doctor, User user, int doctorId)
        {
            throw new System.NotImplementedException();
        }
                
    }
}
