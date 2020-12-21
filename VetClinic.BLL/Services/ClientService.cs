using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;

namespace VetClinic.BLL.Services
{
    public class ClientService : IClientService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private IUserService _userService;

        public ClientService(IRepositoryWrapper repositoryWrapper, IUserService userService)
        {
            _repositoryWrapper = repositoryWrapper;
            _userService = userService; 
        }

        public async Task AddClient(Client client)
        {
            _repositoryWrapper.ClientRepository.Add(client);
            await _repositoryWrapper.SaveAsync();
        }

        public async Task<ICollection<Client>> GetAllClients()
        {
            return await _repositoryWrapper.ClientRepository.GetAsync(include: c => c.Include(c => c.User));
        }

        public async Task<Client> GetClient(int id)
        {
            return await _repositoryWrapper.ClientRepository.GetFirstOrDefaultAsync(
                filter: c => c.Id == id,
                include: c => c.Include(c => c.User)
                );
        }

        public async Task<bool> PutClient(int id, User user, Client client = null)
        {
            var foundClient = await _repositoryWrapper.ClientRepository.GetFirstOrDefaultAsync(filter: c => c.Id == id);
            if (foundClient == null) return false;
            //await PutUser(user);
            await PutClient(client, foundClient);
            await _repositoryWrapper.SaveAsync();
            return true;
        }

        public async Task<bool> PutClient(Client client, Client foundClient)
        {
            if (client == null) return false;
            return true;
        }

        public async Task PutUser(User user)
        {          
            IdentityRole role = new IdentityRole { Name = "member" };
            await _userService.UpdateUser(user, new List<IdentityRole> { role });
        }

        public async Task<bool> DeleteClient(int id)
        {
            var foundClient = await _repositoryWrapper.ClientRepository.GetFirstOrDefaultAsync(filter: c => c.Id == id);
            if (foundClient == null)
                return false;
            _repositoryWrapper.ClientRepository.Remove(foundClient);
            await _repositoryWrapper.SaveAsync();
            return true;
        }

        public Task<bool> PutClient(int id, Client client, User user)
        {
            throw new System.NotImplementedException();
        }
    }
}
