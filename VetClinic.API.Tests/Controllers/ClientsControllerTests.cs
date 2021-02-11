using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VetClinic.API.Controllers;
using VetClinic.API.DTO.ClientDto;
using VetClinic.API.DTO.Queries;
using VetClinic.API.Mapping;
using VetClinic.BLL.Domain;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;
using Xunit;

namespace VetClinic.API.Tests.Controllers
{
    public class ClientsControllerTests
    {
        ClientsController _clientsController;
        Mock<IRepositoryWrapper> _repositoryWrapper;
        Mock<IClientRepository> _clientRepository;
        Mock<IClientService> _clientService;
        Mock<IUserService> _userService;
        Mock<IMapper> _mapper;
        Client _client;

        public ClientsControllerTests()
        {
            _repositoryWrapper = new Mock<IRepositoryWrapper>();
            _clientRepository = new Mock<IClientRepository>();
            _repositoryWrapper.Setup(r => r.ClientRepository).Returns(_clientRepository.Object);
           
            _mapper = new Mock<IMapper>();
            _clientService = new Mock<IClientService>();
            _userService = new Mock<IUserService>();
            _clientsController = new ClientsController(_clientService.Object, _mapper.Object);
            _client = new Client
            {
                Id = 9,
                UserId = "4"
            };
        }

        [Fact]
        public async Task GetAll_ReturnsResult()
        {
            //Arrange
            ICollection<Client> collection = ClientsList();
            Mock<PaginationQuery> query = new Mock<PaginationQuery>();
            Mock<PaginationFilter> paginationFilter = new Mock<PaginationFilter>();
            _mapper.Setup(x => x.Map<PaginationFilter>(query));
            _mapper.Setup(x => x.Map<ClientsFilter>(query));

            var pagination =  _mapper.Object.Map<PaginationFilter>(query);
            var filter = _mapper.Object.Map<ClientsFilter>(query);

            _clientService.Setup(p => p.GetAllClients(filter, pagination)).ReturnsAsync(collection);

            //Action
            var result = await _clientsController.GetAsync(null, query.Object);

            //Assert
            Assert.True(result.Result is OkObjectResult);
        }

        [Fact]
        public async Task Get_Succeded()
        {
            //Arrange
            _clientService.Setup(p => p.GetClient(9)).ReturnsAsync(_client);

            //Action
            var result = await _clientsController.GetAsync(9);

            //Assert
            Assert.True(result.Result is OkObjectResult);
        }

        [Fact]
        public async Task Get_Failed()
        {
            //Assert
            _clientService.Setup(p => p.GetClient(9)).ReturnsAsync(_client);

            //Action
            var result = await _clientsController.GetAsync(6);

            //Assert
            Assert.True(result.Result is NotFoundResult);
        }

        [Fact]
        public async Task Put_ReturnsResult()
        {
            //Arrange
            UpdateClientDto dto = new UpdateClientDto { };
            User user = new User { };
            _clientService.Setup(p => p.PutClient(user, _client)).ReturnsAsync(false);

            //Action
            var result = await _clientsController.PutAsync(3, dto);

            //Assert
            Assert.False(result is null);
        }

        [Fact]
        public async Task Put_Failed()
        {
            //Arrange
            UpdateClientDto dto = new UpdateClientDto { };
            User user = new User { };
            _clientService.Setup(p => p.PutClient(user, _client)).ReturnsAsync(false);

            //Action
            var result = await _clientsController.PutAsync(3, dto);

            //Assert
            Assert.True(result is NotFoundResult);
        }

        private ICollection<Client> ClientsList()
        {
            return new List<Client>
               {
                   new Client{Id=1, UserId = "1"},
                   new Client{Id=1, UserId = "1"},
                   new Client{Id=1, UserId = "1"},
               };
        }
    }
}
