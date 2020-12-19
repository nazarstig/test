using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL.Services.Realizations
{
    public class DoctorService : IDoctorService
    {
        private readonly IUserService _userService;
        public DoctorService(IUserService userService)
        {
            _userService = userService;
        }       

        public  Task<Doctor> Add(Doctor doctor, User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<Doctor> GetAsync(int doctorId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<Doctor>> GetAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Update(Doctor doctor, User user, int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
