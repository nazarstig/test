using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL.Services.Interfaces
{
    public interface IDoctorService
    {
        public Task<Doctor> GetAsync(int doctorId);
        public Task<ICollection<Doctor>> GetAsync();
        public Task<Doctor> Add(Doctor doctor, User user);
        public Task<bool> Remove(int id);
        public Task<bool> Update(Doctor doctor, User user, int id);
    }
}
