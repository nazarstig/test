using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VetClinic.API.Mapping;
using VetClinic.BLL.Services;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.BLL.Services.Realizations;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;
using VetClinic.DAL.Repositories.Realizations;
using Xunit;

namespace VetClinic.BLL.Tests.ServiceTests.ClientServiceTests
{
    public class ClientServiceTests
    {
        IClientService _clientService;
        Mock<IUserService> _userService;
        Mock<IRepositoryWrapper> _repositoryWrapper;
        Mock<IClientRepository> _clientRepository;
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
        }

        [Fact]
        public async void GetClient_ReturnsResult()
        {
            
            Client client = new Client { Id = 9, UserId = "4"};

            _repositoryWrapper.Setup(r => r.ClientRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Client, bool>>>(),
                It.IsAny<Func<IQueryable<Client>, IIncludableQueryable<Client, object>>>(),
                It.IsAny<bool>()
                )).ReturnsAsync(client);

            var result = await _clientService.GetClient(134);
            Assert.Equal(client.Id, result.Id);
        }

        [Fact]
        public async void GetAllClients_ReturnsResult()
        {
            _repositoryWrapper.Setup(r => r.ClientRepository.GetAsync(
               It.IsAny<Expression<Func<Client, bool>>>(),
               It.IsAny<Func<IQueryable<Client>, IIncludableQueryable<Client, object>>>(),
               It.IsAny<Func<IQueryable<Client>, IOrderedQueryable<Client>>>(),
               It.IsAny<bool>()
               )).ReturnsAsync(ClientsList());

            var result = await _clientService.GetAllClients();
            Assert.Equal(result.Count, ClientsList().Count);
        }

        [Fact]
        public async void AddOperationTest_Invoked()
        {
            //arrange
            Client client = new Client { Id = 9, UserId = "4" };
            _clientRepository.Setup(repo => repo.Add(client));
            await _clientService.AddClient(client);
            _clientRepository.Verify(r => r.Add(It.IsAny<Client>()), Times.Once);
        }

        [Fact]
        public async void PutOperation_Invoked()
        {
            Client client = new Client { Id = 9, UserId = "4" };
            User user = new User { };
            _clientRepository.Setup(repo => repo.Update(client)); 
            var result = await _clientService.PutClient(3, client, user);
            _clientRepository.Verify(r => r.Update(It.IsAny<Client>()), Times.Once);
        }

        [Fact]
        public async void DeleteOperation_ReturnsResult()
        {
            Client client = new Client { Id = 9, UserId = "4" };
            _clientRepository.Setup(repo => repo.Remove(client));
            var result = await _clientService.DeleteClient(9);
            Assert.NotNull(result);
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
