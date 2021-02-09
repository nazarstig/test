using AutoFixture.Xunit2;
using Moq;
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
        public async Task UpdateAnimal_AnimalUpdated_UpdateSuccess(
           [Frozen] Mock<IRepositoryWrapper> mockRepositoryWrapper,
           [Frozen] Mock<Animal> animal)
        {
            //Arrange
            int id = 0;
            mockRepositoryWrapper.Setup(x => x.AnimalRepository.Update(It.IsAny<Animal>()));
            var Sut = new AnimalService(mockRepositoryWrapper.Object);

            //Act
            await Sut.UpdateAnimal(id, animal.Object);

            //Assert
            mockRepositoryWrapper.Verify(x => x.AnimalRepository.Update(It.IsAny<Animal>()));
            mockRepositoryWrapper.Verify(x => x.SaveAsync());
        }

        [Theory, AutoMoqData]
        public async Task RemoveAnimal_AnimalRemoved_RemoveSuccess(
           [Frozen] Mock<IRepositoryWrapper> mockRepositoryWrapper)
        {
            //Arrange
            mockRepositoryWrapper.Setup(x => x.AnimalRepository.Remove(It.IsAny<Animal>()));
            Animal animal = new Animal();
            var Sut = new AnimalService(mockRepositoryWrapper.Object);

            //Act
            await Sut.RemoveAnimal(animal);

            //Assert
            mockRepositoryWrapper.Verify(x => x.AnimalRepository.Remove(It.IsAny<Animal>()));
            mockRepositoryWrapper.Verify(x => x.SaveAsync());
        }
    }
}
