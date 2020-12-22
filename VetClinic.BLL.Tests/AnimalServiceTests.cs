using AutoFixture.Xunit2;
using System.Threading.Tasks;
using AutoFixture.AutoMoq;
using VetClinic.BLL.Services.Realizations;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;
using Moq;
using Xunit;

namespace VetClinic.BLL.Tests
{
    public class AnimalServiceTests
    {
        [Theory,AutoMoqData]
        public async Task GetAsync_AnimalId_ReturnsAnimal(
            [Frozen] Mock<IRepositoryWrapper> repositoryWrapper)
        {
            //Arrange  
            int id = 1;
            var Sut = new AnimalService(repositoryWrapper.Object);

            //Act
            var animal = await Sut.GetAsync(id);

            //Assert
            Assert.Equal(id, animal.Id);
        }
    }
}
