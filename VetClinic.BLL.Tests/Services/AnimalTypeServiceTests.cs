using AutoFixture.Xunit2;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.API.Tests;
using VetClinic.BLL.Services.Realizations;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;
using Xunit;

namespace VetClinic.BLL.Tests.Services
{
    public class AnimalTypeServiceTests
    {
        [Theory, AutoMoqData]
        public async Task CreateAnimalType_AnimalTypeCreated_CreateSuccess(
            [Frozen] Mock<IRepositoryWrapper> mockRepositoryWrapper,
            [Frozen] Mock<AnimalType> animalType)
        {
            //Arrange
            mockRepositoryWrapper.Setup(x => x.AnimalTypeRepository.Add(It.IsAny<AnimalType>()));
            var Sut = new AnimalTypeService(mockRepositoryWrapper.Object);

            //Act
            await Sut.CreateAnimalType(animalType.Object);

            //Assert
            mockRepositoryWrapper.Verify(x => x.AnimalTypeRepository.Add(It.IsAny<AnimalType>()));
            mockRepositoryWrapper.Verify(x => x.SaveAsync());
        }

        [Theory, AutoMoqData]
        public async Task GetAll_GetSuccess(
           [Frozen] Mock<IRepositoryWrapper> mockRepositoryWrapper,
           [Frozen] ICollection<AnimalType> animalTypes)
        {
            //Arrange
            mockRepositoryWrapper.Setup(x => x.AnimalTypeRepository
            .GetAsync(null, It.IsAny<Func<IQueryable<AnimalType>, IIncludableQueryable<AnimalType, object>>>(), null, null, null, false))
               .ReturnsAsync(animalTypes);
            var Sut = new AnimalTypeService(mockRepositoryWrapper.Object);

            //Act
            var actual = await Sut.GetAllAsync();

            //Assert
            Assert.IsType<List<AnimalType>>(actual);
            mockRepositoryWrapper.Verify(x => x.AnimalTypeRepository.GetAsync(null, It.IsAny<Func<IQueryable<AnimalType>, IIncludableQueryable<AnimalType, object>>>(), null, null, null, false));
        }

        [Theory, AutoMoqData]
        public async Task RemoveAnimalType_AnimalTypeRemoved_RemoveSuccess(
           [Frozen] Mock<IRepositoryWrapper> mockRepositoryWrapper,
           [Frozen] Mock<AnimalType> animalType)
        {
            //Arrange
            mockRepositoryWrapper.Setup(x => x.AnimalTypeRepository.Remove(It.IsAny<AnimalType>()));
            var Sut = new AnimalTypeService(mockRepositoryWrapper.Object);

            //Act
            await Sut.RemoveAnimalType(animalType.Object);

            //Assert
            mockRepositoryWrapper.Verify(x => x.AnimalTypeRepository.Remove(It.IsAny<AnimalType>()));
            mockRepositoryWrapper.Verify(x => x.SaveAsync());
        }
    }
}
