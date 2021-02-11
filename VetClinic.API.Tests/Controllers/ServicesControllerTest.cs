using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VetClinic.API.Controllers;
using VetClinic.API.DTO;
using VetClinic.API.DTO.Responses;
using VetClinic.API.DTO.Service;
using VetClinic.API.Mapping;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using Xunit;

namespace VetClinic.API.Tests.Controllers
{
    public class ServicesControllerTest
    {
        readonly ServicesController _controller;
        readonly Mock<IServiceService> _service;
        IMapper _mapper;       
       
        public ServicesControllerTest()
        {
            _service = new Mock<IServiceService>();
            InitializeMapper();
            _controller = new ServicesController(_mapper, _service.Object);
        }

        private void InitializeMapper()
        {
            var profile = new ServiceProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(configuration);
        }

        [Theory, AutoMoqData]
        public async Task GetAsync_WhenCalled_ReturnsOkResult(
            [Frozen] List<Service> services)
        {
            // Arrange
            _service.Setup(m => m.GetAllServicesAsync()).ReturnsAsync(services);
            
            // Act
            var okResult = await _controller.GetAsync();
            
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Theory, AutoMoqData]
        public async Task GetAsync_WhenCalled_ReturnsAllItemsAsync(
            [Frozen] ICollection<Service> services)
        {
            // Arrange          
            var testItemCount = services.Count;
            _service.Setup(m => m.GetAllServicesAsync()).ReturnsAsync(services);

            // Act
            var result = await _controller.GetAsync();
            var okResult = result.Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
            var items = Assert.IsType<Response<ICollection<ServiceDto>>>(okResult.Value);
            Assert.Equal(testItemCount, items.Data.Count);           
        }

        [Theory, AutoMoqData]
        public async Task GetAsyncById_UnknownIdPassed_ReturnsNotFoundResult(
            [Frozen] Service testService)
        {
            // Arrange
            var testId = testService.Id;
            _service.Setup(m => m.GetServiceByIdAsync(testId)).ReturnsAsync(testService);

            // Act
            var notFoundResult = await _controller.GetAsync(testId + new Random().Next(1,100));
            
            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Theory, AutoMoqData]
        public async Task GetAsyncById_ExistingIdPassed_ReturnsOkResult(
            [Frozen] Service testService)
        {
            // Arrange
            _service.Setup(m => m.GetServiceByIdAsync(It.IsAny<int>())).ReturnsAsync(testService);
            
            // Act
            var okResult = await _controller.GetAsync(It.IsAny<int>());
            
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Theory, AutoMoqData]
        public async Task GetAsyncById_ExistingIdPassed_ReturnsRightItem(
            [Frozen] Service testService)
        {
            // Arrange
            _service.Setup(m => m.GetServiceByIdAsync(It.IsAny<int>())).ReturnsAsync(testService);

            // Act
            var result = await _controller.GetAsync(testService.Id);
            var okResult = result.Result as OkObjectResult;
            
            // Assert
            Assert.IsType<Response<ServiceDto>>(okResult.Value);
            Assert.Equal(testService.Id, (okResult.Value as Response<ServiceDto>).Data.Id);
            Assert.Equal(testService.ServiceName, (okResult.Value as Response<ServiceDto>).Data.ServiceName);
        }

        [Theory, AutoMoqData]
        public async Task PostAsync_ValidObjectPassed_ReturnsCreatedResponse(
            [Frozen] Service testService)
        {
            // Arrange
            _service.Setup(s => s.AddAsync(It.IsAny<Service>())).ReturnsAsync(testService);

            // Act           
            var createdResponse = await _controller.PostAsync(It.IsAny<ServiceCreateDto>());

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse.Result);
        }

        [Theory, AutoMoqData]
        public async Task PostAsync_ValidObjectPassed_ReturnedResponseHasCreatedItem(
            [Frozen] Service testService)
        {
            // Arrange
            _service.Setup(s => s.AddAsync(It.IsAny<Service>())).ReturnsAsync(testService);
            
            // Act
            var createdAtActionResult = await _controller.PostAsync(It.IsAny<ServiceCreateDto>());
            var result = (Response<ServiceDto>)((CreatedAtActionResult)createdAtActionResult.Result).Value;
            
            // Assert
            Assert.IsType<ServiceDto>(result.Data);
            Assert.Equal(testService.ServiceName, result.Data.ServiceName);
        }

        [Fact]
        public async Task DeleteAsync_ServiceDoesNotExist_ReturnsNotFoundResult()
        {
            // Arrange
            _service.Setup(s => s.RemoveAsync(It.IsAny<int>())).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteAsync(It.IsAny<int>());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteAsync_ServiceExists_ReturnsNoContent()
        {
            // Arrange
            _service.Setup(s => s.RemoveAsync(It.IsAny<int>())).ReturnsAsync(true);
            
            // Act
            var result = await _controller.DeleteAsync(It.IsAny<int>());
            
            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutAsync_ServiceExists_ReturnsNoContent()
        {   
            // Arrange
            _service.Setup(s => s.UpdateAsync(It.IsAny<int>(),It.IsAny<Service>())).ReturnsAsync(true);

            // Act
            var result = await _controller.PutAsync(It.IsAny<int>(),It.IsAny<ServiceUpdateDto>());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutAsync_ServiceDoesNotExist_ReturnNotFound()
        {
            // Arrange
            _service.Setup(s => s.UpdateAsync(It.IsAny<int>(),It.IsAny<Service>())).ReturnsAsync(false);
           
            // Act
            var result = await _controller.PutAsync(It.IsAny<int>(),It.IsAny<ServiceUpdateDto>());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
