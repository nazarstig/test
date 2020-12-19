using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL.Services.Interfaces
{
    public interface IDoctorService
    {
        public Task<(Doctor, User)> GetDoctorAsync(int doctorId);
        public Task<(ICollection<Doctor>, ICollection<User>)> GetDoctorAsync();
        public Task<(Doctor,User)> AddDoctor(Doctor doctor, User user);
        public Task<bool> RemoveDoctor(int id);
        public Task<bool> UpdateDoctor(Doctor doctor, User user, int doctorId);
    }
}
