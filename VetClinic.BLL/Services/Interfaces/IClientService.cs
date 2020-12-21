using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL.Services.Interfaces
{
    public interface IClientService
    {
        public Task AddClient(Client client);
        public Task<ICollection<Client>> GetAllClients();
        public Task<Client> GetClient(int id);
        public Task<bool> PutClient(int id, Client client, User user);
        public Task<bool> DeleteClient(int id);
    }
}
