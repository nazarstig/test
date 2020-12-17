using AutoFixture;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.BLL.Services.Realizations;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;
using Xunit;

namespace VetClinic.BLL.Tests.Services
{
    public class ServiceServiceTest
    {
        readonly ServiceService _serviceService;
        readonly Mock<IRepositoryWrapper> _wrapper;

        public ServiceServiceTest()
        {
            _wrapper = new Mock<IRepositoryWrapper>();                  
            _serviceService = new ServiceService(_wrapper.Object);
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
        public async Task GetAllServices_ReturnsAllItemsAsync()
        {
            // Arrange
            var services = GetTestServices();
            _wrapper.Setup(s => s.ServiceRepository.GetAsync(null,null,null,false)).ReturnsAsync(services);

            // Act 
            var result = await _serviceService.GetAllServicesAsync();

            // Assert
            Assert.IsAssignableFrom<ICollection<Service>>(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetServiceById_ReturnsRightItem()
        {
            // Arrange
            var testId = 1;
            var service = GetTestServices().Find(s => s.Id == testId);
            _wrapper.Setup(s => s.ServiceRepository.GetFirstOrDefaultAsync(s => s.Id == testId, null, false)).ReturnsAsync(service);

            // Act 
            var result =  await _serviceService.GetServiceByIdAsync(service.Id);

            // Assert
            Assert.IsType<Service>(result);
            Assert.Equal(GetTestServices().Find(s => s.Id == testId).ServiceName, result.ServiceName);
        }

        [Fact]
        public async Task GetServiceById_ReturnsNullIfNoFind()
        {
            // Arrange
            int maxId = GetTestServices().Max(s => s.Id);
            var service = GetTestServices().Find(s => s.Id == maxId + 1);
            _wrapper.Setup(s => s.ServiceRepository.GetFirstOrDefaultAsync(s => s.Id == maxId + 1, null, false)).ReturnsAsync(service);

            // Act 
            Service result = await _serviceService.GetServiceByIdAsync(maxId + 1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddService_ReturnsAddedItem()
        {
            // Arrange
            var service = GetTestServices().Find(s => s.Id == 1);
            _wrapper.Setup(s => s.ServiceRepository.Add(It.IsAny<Service>()));

            // Act
            var result = await _serviceService.AddAsync(service);

            // Assert
            Assert.IsType<Service>(result);
            Assert.Equal(service,result);
        }

        [Fact]
        public async Task UpdateService_ReturnsFalseIfNoFind()
        {
            // Arrange
            int maxId = GetTestServices().Max(s => s.Id);
            var service = GetTestServices().Find(s => s.Id == maxId + 1);
            _wrapper.Setup(s => s.ServiceRepository.GetFirstOrDefaultAsync(s => s.Id == maxId + 1, null, false)).ReturnsAsync(service);

            // Act
            var result = await _serviceService.UpdateAsync(maxId + 1, It.IsAny<Service>());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateService_ReturnsTrueIfUpdated()
        {
            // Arrange
            var testId = 1;
            var service = GetTestServices().Find(s => s.Id == testId);
            _wrapper.Setup(s => s.ServiceRepository.GetFirstOrDefaultAsync(s => s.Id == testId, null, false)).ReturnsAsync(service);           
            _wrapper.Setup(s => s.SaveAsync()).ReturnsAsync(1);

            // Act
            var result = await _serviceService.UpdateAsync(testId, service);

            // Assert
            Assert.True(result);
        }


        [Fact]
        public async Task RemoveService_ReturnsFalseIfNoFind()
        {
            // Arrange
            int maxId = GetTestServices().Max(s => s.Id);
            var service = GetTestServices().Find(s => s.Id == maxId + 1);
            _wrapper.Setup(s => s.ServiceRepository.GetFirstOrDefaultAsync(s => s.Id == maxId + 1, null, false)).ReturnsAsync(service);

            // Act
            var result = await _serviceService.RemoveAsync(maxId + 1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task RemoveService_ReturnsTrueIfExists()
        {
            // Arrange
            var testId = 1;
            var service = GetTestServices().Find(s => s.Id == testId);
            _wrapper.Setup(s => s.ServiceRepository.GetFirstOrDefaultAsync(s => s.Id == testId, null, false)).ReturnsAsync(service);

            // Act
            var result = await _serviceService.RemoveAsync(testId);

            // Assert
            Assert.True(result);
        }
    }
}
