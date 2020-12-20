using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        public DoctorService(IRepositoryWrapper repositoryWrapper, IUserService userService, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _repositoryWrapper = repositoryWrapper;
            _userService = userService;
        }       

        public async Task<Doctor> AddDoctor(Doctor doctor, User user)
        {

            var role =await  _roleManager.FindByNameAsync("doctor");
            if (role == null)
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = "doctor" });
                role = await _roleManager.FindByNameAsync("doctor");
            }
                        
            List<IdentityRole> roles = new List<IdentityRole> { role};
            var (sucssess, userId) =await _userService.CreateUser(user, roles);
            if (sucssess)
            {
                _repositoryWrapper.DoctorRepository.Add(doctor);
                await _repositoryWrapper.SaveAsync();
                return doctor;
            }

            return null;
        }

        public async Task<Doctor> GetDoctorAsync(int doctorId)
        {
            Doctor doctor = await _repositoryWrapper.DoctorRepository.GetFirstOrDefaultAsync(
                filter: d => d.Id == doctorId,
                include: d=>d.Include(c=>c.User));           
            return (doctor);
        }

        public async Task<ICollection<Doctor>> GetDoctorAsync()
        {
            var doctors = await _repositoryWrapper.DoctorRepository.GetAsync(
                include:c=>c.Include(i=>i.User));            
            return doctors;

        }

        public async Task<bool> RemoveDoctor(int doctorId)
        {
            var doctor = await _repositoryWrapper.DoctorRepository.GetFirstOrDefaultAsync(d => d.Id == doctorId);
            if (doctor != null)
            {
                _repositoryWrapper.DoctorRepository.Remove(doctor);
                await _repositoryWrapper.SaveAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateDoctor(Doctor inputDoctor, User inputUser, int doctorId)
        {
            var role = await _roleManager.FindByNameAsync("doctor");
            if (role == null)
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = "doctor" });
                role = await _roleManager.FindByNameAsync("doctor");
            }

            List<IdentityRole> roles = new List<IdentityRole> { role };
            var doctor= await _repositoryWrapper.DoctorRepository.GetFirstOrDefaultAsync(c => c.Id == doctorId
            );
            if (doctor==null)             
                return false;

            if (inputDoctor == null && inputUser == null)
                return false;

            if (inputDoctor != null)
            {
                inputDoctor.Id = doctorId;                
                inputDoctor.User = null;
                _repositoryWrapper.DoctorRepository.Update(inputDoctor);                
                await _repositoryWrapper.SaveAsync();
                return true;
            }
            return false;
        }

    }
}
