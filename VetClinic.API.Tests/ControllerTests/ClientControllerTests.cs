using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VetClinic.API.Controllers;
using VetClinic.API.Mapping;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;
using VetClinic.API.DTO.ClientDto;
using Xunit;
using VetClinic.API.DTO.ClientDTO;

namespace VetClinic.API.Tests.ControllerTests
{
    public class ClientControllerTests
    {
      
            ClientController _clientController;
            Mock<IProcedureService> _procedureService;
            Mock<IRepositoryWrapper> _repositoryWrapper;
            Mock<IClientRepository> _clientRepository;
            Mock<IClientService> _clientService;
            Mock<IUserService> _userService;
            IMapper _mapper;
            Client _client;

            public ClientControllerTests()
            {
                _repositoryWrapper = new Mock<IRepositoryWrapper>();
                _clientRepository = new Mock<IClientRepository>();
                _repositoryWrapper.Setup(r => r.ClientRepository).Returns(_clientRepository.Object);
                var mapperConfig = new MapperConfiguration(m =>
                {
                    m.AddProfile(new ClientProfile());
                    m.AddProfile(new UserProfile());
                }
                );

                _mapper = mapperConfig.CreateMapper();
                _clientService = new Mock<IClientService>();
                _userService = new Mock<IUserService>();
                _clientController = new ClientController(_clientService.Object, _userService.Object, _mapper);

                _client = new Client
                {
                    Id = 9,
                    UserId = "4"
                };
                           
            }

            [Fact]
            public async Task IndexOperationCheck()
            {
                ICollection<Client> collection = ClientsList();
                _clientService.Setup(p => p.GetAllClients()).ReturnsAsync(collection);
                var result = await _clientController.Index();
                Assert.True(result.Result is OkObjectResult);
            }

        [Fact]
        public async Task ShowOperationCheck_Succeded()
        {
            _clientService.Setup(p => p.GetClient(9)).ReturnsAsync(_client);
            var result = await _clientController.Show(9);
            Assert.True(result.Result is OkObjectResult);
        }

        [Fact]
        public async Task ShowOperationCheck_Failed()
        {
            _clientService.Setup(p => p.GetClient(9)).ReturnsAsync(_client);
            var result = await _clientController.Show(6);
            Assert.True(result.Result is NotFoundResult);
        }

        [Fact]
        public async Task UpdateOperation_Failed()
        {
            UpdateClientDto dto = new UpdateClientDto { };
            User user = new User { };
            _clientService.Setup(p => p.PutClient(3, _client, user)).ReturnsAsync(false);
            var result = await _clientController.Update(3, dto);
            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public async Task UpdateOperation_Succeded()
        {
            UpdateClientDto dto = new UpdateClientDto { };
            User user = _mapper.Map<User>(dto);
            Client client;
            //_clientService.Setup(p => p.PutClient()).ReturnsAsync(true);
            var result = await _clientController.Update(3, dto);
            Assert.True(result is NoContentResult);
        }

        [Fact]
        public async Task CreateOperationTest()
        {
            CreateClientDto dto = new CreateClientDto { };
            _clientService.Setup(p => p.AddClient(_client));
            var result = await _clientController.Create(dto);
            Assert.True(result is CreatedAtActionResult);
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
