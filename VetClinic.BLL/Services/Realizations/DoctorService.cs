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

        public async Task<ICollection<Doctor>> GetDoctorAsync()
        {
            var doctors = await _repositoryWrapper.DoctorRepository.GetAsync(
                include: c => c.Include(i => i.User));
            return doctors;
        }

        public async Task<Doctor> GetDoctorAsync(int doctorId)
        {
            Doctor doctor = await _repositoryWrapper.DoctorRepository.GetFirstOrDefaultAsync(
                filter: d => d.Id == doctorId,
                include: d => d.Include(c => c.User));

            if (doctor == null)
                return null;

            return doctor;
        }

        public async Task<Doctor> AddDoctorAsync(Doctor doctor, User user)
        {
            var role = await _roleManager.FindByNameAsync("doctor");
            if (role == null)
            {
                await _roleManager.CreateAsync(
                    new IdentityRole() { Name = "doctor" });
                role = await _roleManager.FindByNameAsync("doctor");
            }

            var (sucssess, userId) = await _userService.CreateUserAsync(user, role);
            if (sucssess)
            {
                _repositoryWrapper.DoctorRepository.Add(doctor);
                await _repositoryWrapper.SaveAsync();
            }

            return doctor;
        }

        public async Task<bool> RemoveDoctorAsync(int doctorId)
        {
            var doctor = await _repositoryWrapper.DoctorRepository
                .GetFirstOrDefaultAsync(d => d.Id == doctorId);
            if (doctor != null)
            {
                _repositoryWrapper.DoctorRepository.Remove(doctor);
                await _userService.DeleteUserAsync(doctor.UserId);

                await _repositoryWrapper.SaveAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateDoctorAsync(Doctor inputDoctor, User inputUser, int doctorId)
        {
            var role = await _roleManager.FindByNameAsync("doctor");
            if (role == null)
            {
                await _roleManager.CreateAsync(
                    new IdentityRole() { Name = "doctor" });
                role = await _roleManager.FindByNameAsync("doctor");
            }

            var doctor = await _repositoryWrapper
                .DoctorRepository
                .GetFirstOrDefaultAsync(
                filter: c => c.Id == doctorId,
                include: c => c.Include(d => d.User));

            if (doctor == null)
                return false;

            if (inputDoctor == null)
                return false;

            else
            {
                doctor.Education = inputDoctor.Education;
                doctor.Biography = inputDoctor.Biography;
                doctor.Experience = inputDoctor.Experience;
                doctor.Photo = inputDoctor.Photo;
                doctor.PositionId = inputDoctor.PositionId;

                doctor.User.UserName = inputUser.UserName;
                doctor.User.FirstName = inputUser.FirstName;
                doctor.User.LastName = inputUser.LastName;
                doctor.User.Email = inputUser.Email;
                doctor.User.PhoneNumber = inputUser.PhoneNumber;

                _repositoryWrapper.DoctorRepository.Update(doctor);
                await _userService.UpdateUserAsync(doctor.UserId, doctor.User, role);

                await _repositoryWrapper.SaveAsync();
                return true;
            }
        }
    }
}
