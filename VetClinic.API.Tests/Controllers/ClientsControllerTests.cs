using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AutoFixture;
using AutoFixture.Xunit2;
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
using VetClinic.API.DTO.Responses;

namespace VetClinic.API.Tests.Controllers
{
    public class ClientsControllerTests
    {
        ClientsController _clientsController;
        IFixture _fixture;
        Mock<IClientService> _clientService;
        Mock<IUserService> _userService;
        Mock<IMapper> _mapper;
        int _id;

        public ClientsControllerTests()
        {
            _fixture = new Fixture();
            _mapper = new Mock<IMapper>();
            _clientService = new Mock<IClientService>();
            _userService = new Mock<IUserService>();
            _clientsController = new ClientsController(_clientService.Object, _mapper.Object);
            _id = 9;
        }

        [Theory, AutoMoqData]
        public async Task GetAll_ReturnsResult(
        [Frozen] ICollection<Client> collection,
        [Frozen] PaginationQuery paginationQuery,
        [Frozen] PaginationFilter paginationFilter)
        {
            //Arrange
            _mapper.Setup(x => x.Map<PaginationFilter>(paginationQuery)).Returns(paginationFilter);

            _clientService.Setup(p => p.GetAllClients(null, paginationFilter)).ReturnsAsync(collection);

            //Action
            var result = await _clientsController.GetAsync(null, paginationQuery);

            //Assert
            Assert.True(result.Result is OkObjectResult);
        }

        [Theory, AutoMoqData]
        public async Task Get_Succeded(
            [Frozen] Client client)
        {
            //Arrange            
            _clientService.Setup(p => p.GetClient(_id)).ReturnsAsync(client);

            //Action
            var result = await _clientsController.GetAsync(_id);

            //Assert
            Assert.True(result.Result is OkObjectResult);
        }

        [Fact]
        public async Task Get_Failed()
        {
            //Assert
            _clientService.Setup(p => p.GetClient(_id)).ReturnsAsync((Client) null);

            //Action
            var result = await _clientsController.GetAsync(_id);

            //Assert
            Assert.True(result.Result is NotFoundResult);
        }

        [Theory, AutoMoqData]
        public async Task Put_Failed(
            [Frozen] UpdateClientDto updateClientDto,
            [Frozen] User user,
            [Frozen] Client client)
        {
            //Arrange 
            _mapper.Setup(m => m.Map<User>(updateClientDto)).Returns(user);
            _clientService.Setup(c => c.GetClient(_id)).ReturnsAsync(client);
            _clientService.Setup(p => p.PutClient(user, client)).ReturnsAsync(false);

            //Action
            var result = await _clientsController.PutAsync(_id, updateClientDto);

            //Assert
            Assert.True(result is NotFoundResult);
        }

        [Theory, AutoMoqData]
        public async Task Put_Succeded(
            [Frozen] UpdateClientDto updateClientDto,
            [Frozen] User user,
            [Frozen] Client client)
        {
            //Arrange
            _mapper.Setup(m => m.Map<User>(updateClientDto)).Returns(user);
            _clientService.Setup(c => c.GetClient(_id)).ReturnsAsync(client);
            _clientService.Setup(c => c.PutClient(user, client)).ReturnsAsync(true);

            //Action
            var result = await _clientsController.PutAsync(_id, updateClientDto);

            //Assert
            Assert.True(result is NoContentResult);
        }

        [Theory, AutoMoqData]
        public async Task Post_Succeeded(
            [Frozen] CreateClientDto createClientDto,
            [Frozen] Client client,
            [Frozen] Client createdClient,
            [Frozen] User user,
            [Frozen] ReadClientDto readClientDto
            )
        {
            //Arrange
            _mapper.Setup(m => m.Map<CreateClientDto, User>(createClientDto)).Returns(user);
            _mapper.Setup(m => m.Map<ReadClientDto>(createdClient)).Returns(readClientDto);
            _clientService.Setup(c => c.CreateClientObject()).Returns(client);           
            _clientService.Setup(c => c.AddClient(user, client)).ReturnsAsync(createdClient);

            //Action
            var result = await _clientsController.PostAsync(createClientDto);

            //Assert
            var resultChecked = Assert.IsType<CreatedResult>(result);
            Assert.True(result is CreatedResult);
            Assert.Equal(readClientDto, ((Response<ReadClientDto>)resultChecked.Value).Data);
        }

        [Fact]
        public async Task Delete_Succeded()
        {
            //Arrange 
            _clientService.Setup(c => c.DeleteClient(_id)).ReturnsAsync(true);

            //Action
            var result = await _clientsController.DeleteAsync(_id);

            //Assert
            Assert.True(result is NoContentResult);
        }

        [Fact]
        public async Task Delete_Failed()
        {
            //Arrange 
            _clientService.Setup(c => c.DeleteClient(_id)).ReturnsAsync(false);

            //Action
            var result = await _clientsController.DeleteAsync(_id);

            //Assert
            Assert.True(result is NotFoundResult);
        }  
    }
}
