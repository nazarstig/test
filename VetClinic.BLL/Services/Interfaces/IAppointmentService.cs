using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL.Services.Interfaces
{
    public interface IAppointmentService
    {
        public Task<ICollection<Appointment>> GetAllAppointmentsAsync();
        public Task<Appointment> GetAppointmentByIdAsync(int id);
        public Task<Appointment> CreateAppointmentAsync(Appointment appointment);
        public Task<Appointment> UpdateAppointmentAsync(int id, Appointment appointment);
        public Task<Appointment> DeleteAppointmentAsync(int id);
    }
}