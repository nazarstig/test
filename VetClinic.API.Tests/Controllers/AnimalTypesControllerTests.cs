using AutoFixture.Xunit2;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using VetClinic.API.Controllers;
using VetClinic.API.DTO.AnimalType;
using VetClinic.API.DTO.Queries;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using Xunit;

namespace VetClinic.API.Tests.Controllers
{
    public class AnimalTypesControllerTests
    {
        Mock<IMapper> _mapper;

        public AnimalTypesControllerTests()
        {

            _mapper = new Mock<IMapper>();
        }

        [Theory, AutoMoqData]
        public async Task GetAsync_WhenCalled_ReturnsOkObjectResult(
            [Frozen] Mock<IAnimalTypeService> _animalTypeService,
            [Frozen] Mock<PaginationQuery> paginationQuery)
        {
            //Arrange
            _mapper.Setup(x => x.Map<ReadAnimalTypeDto>(It.IsAny<AnimalType>()));
            var Sut = new AnimalTypesController(_animalTypeService.Object, _mapper.Object);

            //Act
            var actual = await Sut.GetAsync(paginationQuery.Object);

            //Assert
            Assert.NotNull(actual);
            Assert.IsType<OkObjectResult>(actual);
        }

        [Theory, AutoMoqData]
        public async Task GetAsync_AnimalTypeId_ReturnsOkObjectResult(
            [Frozen] Mock<IAnimalTypeService> _animalTypeService)
        {
            //Arrange
            int id = 2;
            _mapper.Setup(x => x.Map<ReadAnimalTypeDto>(It.IsAny<AnimalType>()));
            var Sut = new AnimalTypesController(_animalTypeService.Object, _mapper.Object);

            //Act
            var actual = await Sut.GetAsync(id);

            //Assert
            Assert.NotNull(actual);
            Assert.IsType<OkObjectResult>(actual);
        }

        [Theory, AutoMoqData]
        public async Task PostAsync_AnimalTypeDto_ReturnsCreateAnimalDto(
            [Frozen] Mock<IAnimalTypeService> _animalTypeService,
            [Frozen] Mock<AnimalTypeDto> animalTypeDto)
        {
            //Arrange
            _mapper.Setup(x => x.Map<AnimalTypeDto>(It.IsAny<AnimalType>()));
            var Sut = new AnimalTypesController(_animalTypeService.Object, _mapper.Object);

            //Act
            var actual = await Sut.PostAsync(animalTypeDto.Object);

            //Assert
            _animalTypeService.Verify(x => x.CreateAnimalType(It.IsAny<AnimalType>()));
            Assert.NotNull(actual);
            Assert.IsType<CreatedResult>(actual);
        }

        [Theory, AutoMoqData]
        public async Task PutAsync_AnimalTypeDto_PutSuccessReturnsNoContent(
            [Frozen] Mock<IAnimalTypeService> _animalTypeService,
            [Frozen] Mock<AnimalTypeDto> animalTypeDto)
        {
            //Arrange
            int id = 1;
            _mapper.Setup(x => x.Map<AnimalTypeDto>(It.IsAny<AnimalType>()));
            var Sut = new AnimalTypesController(_animalTypeService.Object, _mapper.Object);

            //Act
            var actual = await Sut.PutAsync(id, animalTypeDto.Object);

            //Assert
            _animalTypeService.Verify(x => x.UpdateAnimalType(id, It.IsAny<AnimalType>()));
            Assert.NotNull(actual);
            Assert.IsType<NoContentResult>(actual);
        }

        [Theory, AutoMoqData]
        public async Task DeleteAsync_AnimalTypeId_DeleteSuccessReturnsNoContent(
            [Frozen] Mock<IAnimalTypeService> _animalTypeService)
        {
            //Arrange
            int id = 1;
            var Sut = new AnimalTypesController(_animalTypeService.Object, _mapper.Object);

            //Act
            var actual = await Sut.DeleteAsync(id);

            //Assert
            _animalTypeService.Verify(x => x.RemoveAnimalType(It.IsAny<AnimalType>()));
            Assert.NotNull(actual);
            Assert.IsType<NoContentResult>(actual);
        }
    }
}
