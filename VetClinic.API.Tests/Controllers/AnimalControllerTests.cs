using AutoFixture.Xunit2;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.API.Controllers;
using VetClinic.API.DTO;
using VetClinic.API.DTO.Animal;
using VetClinic.API.Mapping;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using Xunit;

namespace VetClinic.API.Tests.Controllers
{
    public class AnimalControllerTests
    {
        readonly Mock<IAnimalService> _animalService;
        IMapper _mapper;

        public AnimalControllerTests()
        {
            _animalService = new Mock<IAnimalService>();
            var profile = new ReadAnimalProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(configuration);
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ReturnsOkObjectResult()
        {
            //Arrange
            var Sut = new AnimalsController(_animalService.Object, _mapper);

            //Act
            var actual = await Sut.GetAsync();

            //Assert
            Assert.NotNull(actual);
            Assert.IsType<OkObjectResult>(actual);
        }

        [Fact]
        public async Task GetAsync_AnimalId_ReturnsOkObjectResult()
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

        [Fact]
        public async Task PostAsync_CreateAnimalDto_ReturnsCreateAnimalDto()
        {
            //Arrange
            var animalDto = new Mock<CreateAnimalDto>();
            var Sut = new AnimalsController(_animalService.Object, _mapper);

            //Act
            var actual = await Sut.PostAsync(animalDto.Object);

            //Assert
            Assert.NotNull(actual);
            Assert.IsType<OkObjectResult>(actual);
        }
    }
}
