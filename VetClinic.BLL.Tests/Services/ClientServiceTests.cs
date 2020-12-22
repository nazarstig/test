using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VetClinic.API.Mapping;
using VetClinic.BLL.Services;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;
using Xunit;

namespace VetClinic.BLL.Tests.ServiceTests.ClientServiceTests
{
    public class ClientServiceTests
    {
        IClientService _clientService;
        Mock<IUserService> _userService;
        Mock<IRepositoryWrapper> _repositoryWrapper;
        Mock<IClientRepository> _clientRepository;
        Client _client;
        User _user;
        IMapper _mapper;

        public ClientServiceTests()
        {
            _repositoryWrapper = new Mock<IRepositoryWrapper>();
            _clientRepository = new Mock<IClientRepository>();
            _repositoryWrapper.Setup(r => r.ClientRepository).Returns(_clientRepository.Object);
            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new ClientProfile());
            }
            );
            _mapper = mapperConfig.CreateMapper();
            _userService = new Mock<IUserService>();
            _clientService = new ClientService(_repositoryWrapper.Object, _userService.Object);
            _client = new Client { Id = 9, UserId = "id" };
            _user = new User { Id = "id" };
        }

        [Fact]
        public async void GetClient_ReturnsResult()
        {
            //arrange
            _repositoryWrapper.Setup(r => r.ClientRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Client, bool>>>(),
                It.IsAny<Func<IQueryable<Client>, IIncludableQueryable<Client, object>>>(),
                It.IsAny<bool>()
                )).ReturnsAsync(_client);

            //action
            var result = await _clientService.GetClient(134);

            //assert
            Assert.Equal(_client.Id, result.Id);
        }

        [Fact]
        public async void GetAllClients_ReturnsResult()
        {
            //arrange
            _repositoryWrapper.Setup(r => r.ClientRepository.GetAsync(
               It.IsAny<Expression<Func<Client, bool>>>(),
               It.IsAny<Func<IQueryable<Client>, IIncludableQueryable<Client, object>>>(),
               It.IsAny<Func<IQueryable<Client>, IOrderedQueryable<Client>>>(),
               It.IsAny<bool>()
               )).ReturnsAsync(ClientsList());

            //action
            var result = await _clientService.GetAllClients();

            //assert
            Assert.Equal(result.Count, ClientsList().Count);
        }

        [Fact]
        public async Task PutOperation_ReturnsResult()
        {
            //arrange
            _clientRepository.Setup(repo => repo.Update(_client)); 

            //action
            var result = await _clientService.PutClient(_user, _client);

            //assert
            Assert.NotNull(result);
        }
      
        [Fact]
        public async void DeleteOperation_ReturnsResult()
        {
            //arrange
            _clientRepository.Setup(repo => repo.Remove(_client));
            Client delClient = new Client { Id = 9, UserId = _user.Id };

            //action
            var result = await _clientService.DeleteClient(9);

            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteOperation_Succeded()
        {
            //arrange
            _repositoryWrapper.Setup(r => r.ClientRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Client, bool>>>(),
               It.IsAny<Func<IQueryable<Client>, IIncludableQueryable<Client, object>>>(),
               It.IsAny<bool>()
               )).ReturnsAsync(_client);
            _userService.Setup(s => s.DeleteUserAsync(_user.Id)).ReturnsAsync(true);
            //action
            var res = await _clientService.DeleteClient(_client.Id);

            //assert
            Assert.Equal(res, true);
        }

        private ICollection<Client> ClientsList()
        {
            return new List<Client>
            {
                new Client { Id = 2, UserId = "5"},
                new Client { Id = 4, UserId = "6" },
                new Client { Id = 6, UserId = "7" }
            };
        }

    }
}
