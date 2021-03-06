using AutoFixture.Xunit2;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using VetClinic.API.Controllers;
using VetClinic.API.DTO.Animal;
using VetClinic.API.DTO.Queries;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using Xunit;

namespace VetClinic.API.Tests.Controllers
{
    public class AnimalsControllerTests
    {
        Mock<IMapper> _mapper;

        public AnimalsControllerTests()
        {

            _mapper = new Mock<IMapper>();
        }

        [Theory, AutoMoqData]
        public async Task GetAsync_WhenCalled_ReturnsOkObjectResult(
            [Frozen] Mock<IAnimalService> _animalService,
            [Frozen] Mock<PaginationQuery> paginationQuery)
        {
            //Arrange
            _mapper.Setup(x => x.Map<ReadAnimalDto>(It.IsAny<Animal>()));
            var Sut = new AnimalsController(_animalService.Object, _mapper.Object);

            //Act
            var actual = await Sut.GetAsync(null, paginationQuery.Object);

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
            _mapper.Setup(x => x.Map<ReadAnimalDto>(It.IsAny<Animal>()));
            var Sut = new AnimalsController(_animalService.Object, _mapper.Object);

            //Act
            var actual = await Sut.GetAsync(id);

            //Assert
            Assert.NotNull(actual);
            Assert.IsType<OkObjectResult>(actual);
        }

        [Theory, AutoMoqData]
        public async Task PostAsync_CreateAnimalDto_ReturnsCreateAnimalDto(
            [Frozen] Mock<IAnimalService> _animalService,
            [Frozen] Mock<CreateAnimalDto> animalDto)
        {
            //Arrange
            _mapper.Setup(x => x.Map<CreateAnimalDto>(It.IsAny<Animal>()));
            var Sut = new AnimalsController(_animalService.Object, _mapper.Object);

            //Act
            var actual = await Sut.PostAsync(animalDto.Object);

            //Assert
            _animalService.Verify(x => x.CreateAnimal(It.IsAny<Animal>()));
            Assert.NotNull(actual);
            Assert.IsType<CreatedResult>(actual);
        }

        [Theory, AutoMoqData]
        public async Task PutAsync_UpdateAnimalDto_PutSuccessReturnsNoContent(
            [Frozen] Mock<IAnimalService> _animalService,
            [Frozen] Mock<UpdateAnimalDto> animalDto)
        {
            //Arrange
            int id = 1;
            _mapper.Setup(x => x.Map<UpdateAnimalDto>(It.IsAny<Animal>()));
            var Sut = new AnimalsController(_animalService.Object, _mapper.Object);

            //Act
            var actual = await Sut.PutAsync(id, animalDto.Object);

            //Assert
            _animalService.Verify(x => x.UpdateAnimal(id, It.IsAny<Animal>()));
            Assert.NotNull(actual);
            Assert.IsType<NoContentResult>(actual);
        }

        [Theory, AutoMoqData]
        public async Task DeleteAsync_AnimalId_DeleteSuccessReturnsNoContent(
            [Frozen] Mock<IAnimalService> _animalService)
        {
            //Arrange
            int id = 1;
            var Sut = new AnimalsController(_animalService.Object, _mapper.Object);

            //Act
            var actual = await Sut.DeleteAsync(id);

            //Assert
            _animalService.Verify(x => x.RemoveAnimal(It.IsAny<Animal>()));
            Assert.NotNull(actual);
            Assert.IsType<NoContentResult>(actual);
        }
    }
}
