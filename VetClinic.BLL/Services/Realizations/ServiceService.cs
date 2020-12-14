using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;

namespace VetClinic.BLL.Services.Realizations
{
    public class ServiceService : IServiceService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ServiceService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<ICollection<Service>> GetAllServices()
        {
            return await _repositoryWrapper.ServiceRepository.GetAsync(
                filter: s => s.Id > 2,
                include: source => source.Include(s => s.Appointments));
        }

        public async Task<Service> GetServiceById(int id)
        {
            return await _repositoryWrapper.ServiceRepository.GetFirstOrDefaultAsync(
                filter: s => s.Id == id, 
                include: source => source.Include(s => s.Appointments));
        }

        public async Task<Service> Add(Service service)
        {
            _repositoryWrapper.ServiceRepository.Add(service);
            await _repositoryWrapper.SaveAsync();
            return service;
        }

        public async Task<bool> Update(Service service)
        {
            var serviceUpdated = await _repositoryWrapper.ServiceRepository.GetFirstOrDefaultAsync(
               filter: s => s.Id == service.Id);

            if (serviceUpdated == null)
            {
                return false;
            }

            serviceUpdated.Id = service.Id;
            serviceUpdated.ServiceName = service.ServiceName;
            serviceUpdated.Appointments = service.Appointments;

              _repositoryWrapper.ServiceRepository.Update(serviceUpdated);
             int affectedRows =  await _repositoryWrapper.SaveAsync();
             return affectedRows > 0;
        }

        public async Task<bool> Remove(int serviceId)
        {
            var service = await _repositoryWrapper.ServiceRepository.GetFirstOrDefaultAsync(
                filter: s => s.Id == serviceId);

            if(service == null)
            {
                return false;
            }

            _repositoryWrapper.ServiceRepository.Remove(service);
            await _repositoryWrapper.SaveAsync();
            return true;
        }
    }
}
