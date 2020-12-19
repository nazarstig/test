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
            var okResult = await _controller.Get();
            
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsAllItemsAsync()
        {
            // Arrange
            _service.Setup(m => m.GetAllServicesAsync()).ReturnsAsync(GetTestServices());

            // Act
            var result = await _controller.Get();
            var okResult = result.Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
            var items = Assert.IsType<List<ServiceDTO>>(okResult.Value);
            Assert.Equal(3, items.Count);           
        }

        [Fact]
        public async Task GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {

            // Arrange
            var testId = 2;
            _service.Setup(m => m.GetServiceByIdAsync(testId)).ReturnsAsync(new Service { Id = 2, ServiceName = "Makeup", Appointments = new List<Appointment>() { new Appointment() { Id = 2, AppointmentDate = DateTime.Now, ServiceId = 2 } } });

            // Act
            var notFoundResult = await _controller.Get(testId + new Random().Next(1,100));
            
            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public async Task GetById_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            _service.Setup(m => m.GetServiceByIdAsync(It.IsAny<int>())).ReturnsAsync(new Service { Id = 2, ServiceName = "Makeup", Appointments = new List<Appointment>() { new Appointment() { Id = 2, AppointmentDate = DateTime.Now, ServiceId = 2 } } });
            
            // Act
            var okResult = await _controller.Get(It.IsAny<int>());
            
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
            var result = await _controller.Get(testService.Id);
            var okResult = result.Result as OkObjectResult;
            
            // Assert
            Assert.IsType<ServiceDTO>(okResult.Value);
            Assert.Equal(testService.Id, (okResult.Value as ServiceDTO).Id);
            Assert.Equal(testService.ServiceName, (okResult.Value as ServiceDTO).ServiceName);
        }

        [Fact]
        public async Task Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            Service testItem = new Service()
            {
                ServiceName = "Surgery"
            };

            _service.Setup(s => s.AddAsync(It.IsAny<Service>())).ReturnsAsync(testItem);

            // Act           
            var createdResponse = await _controller.PostService(It.IsAny<ServiceCreateDTO>());

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse.Result);
        }
       
        [Fact]
        public async Task Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new Service()
            {
                ServiceName = "Surgery"
            };
            _service.Setup(s => s.AddAsync(It.IsAny<Service>())).ReturnsAsync(testItem);
            
            // Act
            var createdAtActionResult = await _controller.PostService(It.IsAny<ServiceCreateDTO>());
            var result = (ServiceDTO)((CreatedAtActionResult)createdAtActionResult.Result).Value;
            
            // Assert
            Assert.IsType<ServiceDTO>(result);
            Assert.Equal(testItem.ServiceName, result.ServiceName);
        }

        [Fact]
        public async Task DeleteService_ServiceDoesNotExist_ReturnsNotFoundResult()
        {
            // Arrange
            _service.Setup(s => s.RemoveAsync(It.IsAny<int>())).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteService(It.IsAny<int>());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteService_ServiceExists_ReturnsNoContent()
        {
            // Arrange
            _service.Setup(s => s.RemoveAsync(It.IsAny<int>())).ReturnsAsync(true);
            
            // Act
            var result = await _controller.DeleteService(It.IsAny<int>());
            
            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutService_ServiceExists_ReturnsNoContent()
        {   
            // Arrange
            _service.Setup(s => s.UpdateAsync(It.IsAny<int>(),It.IsAny<Service>())).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateService(It.IsAny<int>(),It.IsAny<ServiceUpdateDTO>());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutService_ServiceDoesNotExist_ReturnNotFound()
        {
            // Arrange
            _service.Setup(s => s.UpdateAsync(It.IsAny<int>(),It.IsAny<Service>())).ReturnsAsync(false);
           
            // Act
            var result = await _controller.UpdateService(It.IsAny<int>(),It.IsAny<ServiceUpdateDTO>());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
