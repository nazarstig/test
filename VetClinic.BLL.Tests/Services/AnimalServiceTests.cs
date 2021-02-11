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
    public class AnimalServiceTests
    {
        [Theory, AutoMoqData]
        public async Task CreateAnimal_AnimalCreated_CreateSuccess(
            [Frozen] Mock<IRepositoryWrapper> mockRepositoryWrapper)
        {
            //Arrange
            mockRepositoryWrapper.Setup(x => x.AnimalRepository.Add(It.IsAny<Animal>()));
            Animal animal = new Animal();
            var Sut = new AnimalService(mockRepositoryWrapper.Object);

            //Act
            await Sut.CreateAnimal(animal);

            //Assert
            mockRepositoryWrapper.Verify(x => x.AnimalRepository.Add(It.IsAny<Animal>()));
            mockRepositoryWrapper.Verify(x => x.SaveAsync());
        }

        [Theory, AutoMoqData]
        public async Task GetAll_GetSuccess(
           [Frozen] Mock<IRepositoryWrapper> mockRepositoryWrapper,
           [Frozen] ICollection<Animal> animals)
        {
            //Arrange
            mockRepositoryWrapper.Setup(x=>x.AnimalRepository
            .GetAsync(null, It.IsAny<Func<IQueryable<Animal>, IIncludableQueryable<Animal, object>>>(), null, null, null, false))
               .ReturnsAsync(animals);

            var Sut = new AnimalService(mockRepositoryWrapper.Object);

            //Act
            var actual = await Sut.GetAllAsync();

            //Assert
            Assert.IsType<List<Animal>>(actual);
            mockRepositoryWrapper.Verify(x => x.AnimalRepository.GetAsync(null, It.IsAny<Func<IQueryable<Animal>, IIncludableQueryable<Animal, object>>>(), null, null, null, false));
        }

        [Theory, AutoMoqData]
        public async Task RemoveAnimal_AnimalRemoved_RemoveSuccess(
           [Frozen] Mock<IRepositoryWrapper> mockRepositoryWrapper,
           [Frozen] Mock<Animal> animal)
        {
            //Arrange
            mockRepositoryWrapper.Setup(x => x.AnimalRepository.Remove(It.IsAny<Animal>()));
            var Sut = new AnimalService(mockRepositoryWrapper.Object);

            //Act
            await Sut.RemoveAnimal(animal.Object);

            //Assert
            mockRepositoryWrapper.Verify(x => x.AnimalRepository.Remove(It.IsAny<Animal>()));
            mockRepositoryWrapper.Verify(x => x.SaveAsync());
        }
    }
}
