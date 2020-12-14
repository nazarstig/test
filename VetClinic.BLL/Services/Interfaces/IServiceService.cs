using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL.Services.Interfaces
{
    public interface IServiceService
    {
        Task<ICollection<Service>> GetAllServices();

        Task<Service> GetServiceById(int id);

        Task<Service> Add(Service service);

        Task<bool> Update(Service service);

        Task<bool> Remove(int serviceId);
    }
}
