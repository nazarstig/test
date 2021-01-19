using AutoFixture.Xunit2;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.API.Tests;
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

        [Theory, AutoMoqData]
        public async Task GetAllServices_ReturnsAllItemsAsync(
            [Frozen] List<Service> services)
        {
            // Arrange
            _wrapper.Setup(s => s.ServiceRepository.GetAsync(null,null,null,null,null,false)).ReturnsAsync(services);

            // Act 
            var result = await _serviceService.GetAllServicesAsync();

            // Assert
            Assert.IsAssignableFrom<ICollection<Service>>(result);
            Assert.Equal(services.Count, result.Count);
        }

        [Theory, AutoMoqData]
        public async Task GetServiceById_ReturnsRightItem(
            [Frozen] Service testService)
        {
            // Arrange
            var testId = testService.Id;
            _wrapper.Setup(s => s.ServiceRepository.GetFirstOrDefaultAsync(s => s.Id == testId, null, false)).ReturnsAsync(testService);

            // Act 
            var result =  await _serviceService.GetServiceByIdAsync(testService.Id);

            // Assert
            Assert.IsType<Service>(result);
            Assert.Equal(testService.ServiceName, result.ServiceName);
        }

        [Theory, AutoMoqData]
        public async Task GetServiceById_ReturnsNullIfNoFind(
            [Frozen] Service testService)
        {
            // Arrange
            _wrapper.Setup(s => s.ServiceRepository.GetFirstOrDefaultAsync(s => s.Id == testService.Id, null, false)).ReturnsAsync(value: null);

            // Act 
            Service result = await _serviceService.GetServiceByIdAsync(testService.Id);

            // Assert
            Assert.Null(result);
        }

        [Theory, AutoMoqData]
        public async Task AddService_ReturnsAddedItem(
            [Frozen] Service testService)
        {
            // Arrange
            _wrapper.Setup(s => s.ServiceRepository.Add(It.IsAny<Service>()));

            // Act
            var result = await _serviceService.AddAsync(testService);

            // Assert
            Assert.IsType<Service>(result);
            Assert.Equal(testService, result);
        }

        [Theory, AutoMoqData]
        public async Task UpdateService_ReturnsFalseIfNoFind(
            [Frozen] Service testService)
        {
            // Arrange
            var testId = testService.Id;
            _wrapper.Setup(s => s.ServiceRepository.GetFirstOrDefaultAsync(s => s.Id == testId, null, false)).ReturnsAsync(value: null);

            // Act
            var result = await _serviceService.UpdateAsync(testId, It.IsAny<Service>());

            // Assert
            Assert.False(result);
        }

        [Theory, AutoMoqData]
        public async Task UpdateService_ReturnsTrueIfUpdated(
            [Frozen] Service testService)
        {
            // Arrange
            var testId = testService.Id;
            _wrapper.Setup(s => s.ServiceRepository.GetFirstOrDefaultAsync(s => s.Id == testId, null, false)).ReturnsAsync(testService);           

            // Act
            var result = await _serviceService.UpdateAsync(testId, testService);

            // Assert
            Assert.True(result);
        }


        [Theory, AutoMoqData]
        public async Task RemoveService_ReturnsFalseIfNoFind(
            [Frozen] Service testService)
        {
            // Arrange
            var testId = testService.Id;
            _wrapper.Setup(s => s.ServiceRepository.GetFirstOrDefaultAsync(s => s.Id == testId, null, false)).ReturnsAsync(value: null);

            // Act
            var result = await _serviceService.RemoveAsync(testId);

            // Assert
            Assert.False(result);
        }

        [Theory, AutoMoqData]
        public async Task RemoveService_ReturnsTrueIfExists(
            [Frozen] Service testService)
        {
            // Arrange
            var testId = testService.Id;
            _wrapper.Setup(s => s.ServiceRepository.GetFirstOrDefaultAsync(s => s.Id == testId, null, false)).ReturnsAsync(testService);

            // Act
            var result = await _serviceService.RemoveAsync(testId);

            // Assert
            Assert.True(result);
        }
    }
}
