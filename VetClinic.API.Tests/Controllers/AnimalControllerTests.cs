using AutoFixture.Xunit2;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using VetClinic.API.Controllers;
using VetClinic.API.DTO;
using VetClinic.API.Mapping;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using Xunit;

namespace VetClinic.API.Tests.Controllers
{
    public class AnimalControllerTests
    {
        IMapper _mapper;

        public AnimalControllerTests()
        {
            var profile = new ReadAnimalProfile();
            var profile1 = new CreateAnimalProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(configuration);
        }

        [Theory,AutoMoqData]
        public async Task GetAsync_WhenCalled_ReturnsOkObjectResult(
            [Frozen] Mock<IAnimalService> _animalService)
        {
            //Arrange
            var Sut = new AnimalsController(_animalService.Object, _mapper);

            //Act
            var actual = await Sut.GetAsync();

            //Assert
            Assert.NotNull(actual);
            Assert.IsType<OkObjectResult>(actual);
        }

        [Theory, AutoMoqData]
        public async Task GetAsync_AnimalId_ReturnsOkObjectResult(
            [Frozen] Mock<IAnimalService> _animalService)
        {
            //Arrange
            int id = 2;
            var Sut = new AnimalsController(_animalService.Object, _mapper);

            //Act
            var actual = await Sut.GetAsync(id);

            //Assert
            Assert.NotNull(actual);
            Assert.IsType<OkObjectResult>(actual);
        }

        [Theory, AutoMoqData]
        public async Task PostAsync_CreateAnimalDto_ReturnsCreateAnimalDto(
            [Frozen] Mock<IAnimalService> _animalService)
        {
            //Arrange
            var animalDto = new Mock<CreateAnimalDto>();
            var Sut = new AnimalsController(_animalService.Object, _mapper);

            //Act
            var actual = await Sut.PostAsync(animalDto.Object);

            //Assert
            _animalService.Verify(x => x.CreateAnimal(It.IsAny<Animal>()));
            Assert.NotNull(actual);
            Assert.IsType<OkObjectResult>(actual);
        }

        [Theory, AutoMoqData]
        public async Task PutAsync_UpdateAnimalDto_PutSuccessReturnsNoContent(
            [Frozen] Mock<IAnimalService> _animalService)
        {
            //Arrange
            int id = 1;
            var animalDto = new Mock<UpdateAnimalDto>();
            var Sut = new AnimalsController(_animalService.Object, _mapper);

            //Act
            var actual = await Sut.PutAsync(id, animalDto.Object);

            //Assert
            _animalService.Verify(x => x.UpdateAnimal(It.IsAny<Animal>()));
            Assert.NotNull(actual);
            Assert.IsType<NoContentResult>(actual);
        }

        [Theory, AutoMoqData]
        public async Task DeleteAsync_AnimalId_DeleteSuccessReturnsNoContent(
            [Frozen] Mock<IAnimalService> _animalService)
        {
            //Arrange
            int id = 1;
            var Sut = new AnimalsController(_animalService.Object, _mapper);

            //Act
            var actual = await Sut.DeleteAsync(id);

            //Assert
            _animalService.Verify(x => x.RemoveAnimal(It.IsAny<Animal>()));
            Assert.NotNull(actual);
            Assert.IsType<NoContentResult>(actual);
        }
    }
}
