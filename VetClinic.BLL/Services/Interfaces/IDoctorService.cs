using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL.Services.Interfaces
{
    public interface IDoctorService
    {
        public Task<Doctor> GetDoctorAsync(int doctorId);
        public Task<ICollection<Doctor>> GetDoctorAsync();
        public Task<Doctor> AddDoctorAsync(Doctor doctor, User user);
        public Task<bool> RemoveDoctorAsync(int doctorId);
        public Task<bool> UpdateDoctorAsync(Doctor inputDoctor, User inputUser, int doctorId);
    }
}
