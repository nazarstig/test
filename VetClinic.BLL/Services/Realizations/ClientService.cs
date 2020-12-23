using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;

namespace VetClinic.BLL.Services.Realizations
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

        public async Task<Client> AddClient(User user, Client client)
        {         
            IdentityRole role = new IdentityRole { Name = "member" };
            var (res, id) = await _userService.CreateUserAsync(user, role);
            string userId;
            if (res)
            {
                userId = id;
                client = new Client { UserId = userId };
                _repositoryWrapper.ClientRepository.Add(client);
                await _repositoryWrapper.SaveAsync();
            }
            return client;
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

        public async Task<bool> PutClient(User user, Client client)
        {
            IdentityRole role = new IdentityRole { Name = "member" };
            var res = await _userService.UpdateUserAsync(client.UserId, user, role);
            await _repositoryWrapper.SaveAsync();
            return res;
        }

        public async Task<bool> DeleteClient(int id)
        {
            var foundClient = await _repositoryWrapper.ClientRepository.GetFirstOrDefaultAsync(filter: c => c.Id == id);
            if (foundClient == null)
                return false;
            var userDeleted = await _userService.DeleteUserAsync(foundClient.UserId);
            if (!userDeleted) return false;           
            _repositoryWrapper.ClientRepository.Remove(foundClient);            
            await _repositoryWrapper.SaveAsync();
            return true;
        }
    }
}
