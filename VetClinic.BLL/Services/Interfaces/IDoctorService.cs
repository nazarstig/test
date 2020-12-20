using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL.Services.Interfaces
{
    public interface IDoctorService
    {
        public Task<Doctor> GetDoctorAsync(int doctorId);
        public Task<ICollection<Doctor>> GetDoctorAsync();
        public Task<Doctor> AddDoctor(Doctor doctor, User user);
        public Task<bool> RemoveDoctor(int doctorId);
        public Task<bool> UpdateDoctor(Doctor inputDoctor, User inputUser, int doctorId);
    }
}
