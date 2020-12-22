using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL.Services.Interfaces
{
    public interface IClientService
    {
        public Task<Client> AddClient(User user, Client client);
        public Task<ICollection<Client>> GetAllClients();
        public Task<Client> GetClient(int id);
        public Task<bool> PutClient(User user, Client client);
        public Task<bool> DeleteClient(int id);
    }
}
