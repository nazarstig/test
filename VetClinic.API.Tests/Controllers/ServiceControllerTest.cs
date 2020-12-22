using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.API.Controllers;
using VetClinic.API.DTO;
using VetClinic.API.DTO.Service;
using VetClinic.API.Mapping;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using Xunit;

namespace VetClinic.API.Tests
{
    public class ServiceControllerTest
    {
        readonly ServiceController _controller;
        readonly Mock<IServiceService> _service;
        IMapper _mapper;       
       
        public ServiceControllerTest()
        {
            _service = new Mock<IServiceService>();
            InitializeMapper();
            _controller = new ServiceController(_mapper, _service.Object);
        }

        private void InitializeMapper()
        {
            var profile = new ServiceProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(configuration);
        }

        private List<Service> GetTestServices()
        {
            return new List<Service>(){
                new Service { Id = 1, ServiceName = "Urgently", Appointments = new List<Appointment>(){ new Appointment() { Id = 1, AppointmentDate = DateTime.Now, ServiceId = 1} } },
                new Service { Id = 2, ServiceName = "Makeup", Appointments = new List<Appointment>(){ new Appointment() { Id = 2, AppointmentDate = DateTime.Now, ServiceId = 2} } },
                new Service { Id = 3, ServiceName = "Inspection", Appointments = new List<Appointment>(){ new Appointment() { Id = 3, AppointmentDate = DateTime.Now, ServiceId = 3} } }
            };
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            _service.Setup(m => m.GetAllServicesAsync()).ReturnsAsync(GetTestServices());
            
            // Act
            var okResult = await _controller.Index();
            
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsAllItemsAsync()
        {
            // Arrange
            _service.Setup(m => m.GetAllServicesAsync()).ReturnsAsync(GetTestServices());

            // Act
            var result = await _controller.Index();
            var okResult = result.Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
            var items = Assert.IsType<List<ServiceDto>>(okResult.Value);
            Assert.Equal(3, items.Count);           
        }

        [Fact]
        public async Task GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {

            // Arrange
            var testId = 2;
            _service.Setup(m => m.GetServiceByIdAsync(testId)).ReturnsAsync(new Service { Id = 2, ServiceName = "Makeup", Appointments = new List<Appointment>() { new Appointment() { Id = 2, AppointmentDate = DateTime.Now, ServiceId = 2 } } });

            // Act
            var notFoundResult = await _controller.Show(testId + new Random().Next(1,100));
            
            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public async Task GetById_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            _service.Setup(m => m.GetServiceByIdAsync(It.IsAny<int>())).ReturnsAsync(new Service { Id = 2, ServiceName = "Makeup", Appointments = new List<Appointment>() { new Appointment() { Id = 2, AppointmentDate = DateTime.Now, ServiceId = 2 } } });
            
            // Act
            var okResult = await _controller.Show(It.IsAny<int>());
            
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public async Task GetById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var testService = new Service()
            {
               Id = 1,
               ServiceName = "Urgently",
               Appointments = new List<Appointment>(){new Appointment() { Id = 1, AppointmentDate = DateTime.Now, ServiceId = 2 }}
            };
            _service.Setup(m => m.GetServiceByIdAsync(testService.Id)).ReturnsAsync(testService);

            // Act
            var result = await _controller.Show(testService.Id);
            var okResult = result.Result as OkObjectResult;
            
            // Assert
            Assert.IsType<ServiceDto>(okResult.Value);
            Assert.Equal(testService.Id, (okResult.Value as ServiceDto).Id);
            Assert.Equal(testService.ServiceName, (okResult.Value as ServiceDto).ServiceName);
        }

        [Fact]
        public async Task Create_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            Service testItem = new Service()
            {
                ServiceName = "Surgery"
            };

            _service.Setup(s => s.AddAsync(It.IsAny<Service>())).ReturnsAsync(testItem);

            // Act           
            var createdResponse = await _controller.Create(It.IsAny<ServiceCreateDto>());

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse.Result);
        }
       
        [Fact]
        public async Task Create_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new Service()
            {
                ServiceName = "Surgery"
            };
            _service.Setup(s => s.AddAsync(It.IsAny<Service>())).ReturnsAsync(testItem);
            
            // Act
            var createdAtActionResult = await _controller.Create(It.IsAny<ServiceCreateDto>());
            var result = (ServiceDto)((CreatedAtActionResult)createdAtActionResult.Result).Value;
            
            // Assert
            Assert.IsType<ServiceDto>(result);
            Assert.Equal(testItem.ServiceName, result.ServiceName);
        }

        [Fact]
        public async Task DeleteService_ServiceDoesNotExist_ReturnsNotFoundResult()
        {
            // Arrange
            _service.Setup(s => s.RemoveAsync(It.IsAny<int>())).ReturnsAsync(false);

            // Act
            var result = await _controller.Destroy(It.IsAny<int>());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteService_ServiceExists_ReturnsNoContent()
        {
            // Arrange
            _service.Setup(s => s.RemoveAsync(It.IsAny<int>())).ReturnsAsync(true);
            
            // Act
            var result = await _controller.Destroy(It.IsAny<int>());
            
            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateService_ServiceExists_ReturnsNoContent()
        {   
            // Arrange
            _service.Setup(s => s.UpdateAsync(It.IsAny<int>(),It.IsAny<Service>())).ReturnsAsync(true);

            // Act
            var result = await _controller.Update(It.IsAny<int>(),It.IsAny<ServiceUpdateDto>());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateService_ServiceDoesNotExist_ReturnNotFound()
        {
            // Arrange
            _service.Setup(s => s.UpdateAsync(It.IsAny<int>(),It.IsAny<Service>())).ReturnsAsync(false);
           
            // Act
            var result = await _controller.Update(It.IsAny<int>(),It.IsAny<ServiceUpdateDto>());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
