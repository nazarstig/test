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

namespace VetClinic.API.Tests.ControllerTests
{
    public class ClientControllerTests
    {     
            ClientController _clientController;
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
                _clientController = new ClientController(_clientService.Object, _mapper);
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
                _clientService.Setup(p => p.GetAllClients()).ReturnsAsync(collection);

                //Action
                var result = await _clientController.GetAsync();

                //Assert
                Assert.True(result.Result is OkObjectResult);
            }

            [Fact]
            public async Task Get_Succeded()
            {
                //Arrange
                _clientService.Setup(p => p.GetClient(9)).ReturnsAsync(_client);
             
                //Action
                var result = await _clientController.GetAsync(9);

                //Assert
                Assert.True(result.Result is OkObjectResult);
            }
            
            [Fact]
            public async Task Get_Failed()
            {   
                //Assert
                _clientService.Setup(p => p.GetClient(9)).ReturnsAsync(_client);

                //Action
                var result = await _clientController.GetAsync(6);

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
                var result = await _clientController.PutAsync(3, dto);

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
                var result = await _clientController.PutAsync(3, dto);

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
