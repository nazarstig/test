using AutoFixture.Xunit2;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.API.Controllers;
using VetClinic.API.DTO.PositionDTO;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using Xunit;

namespace VetClinic.API.Tests
{
    public class PositionControllerTests
    {
        [Theory, AutoMoqData]
        public async Task GetAll_Positions_ReturnsTrue(
            [Frozen] List<Position> positions,
            [Frozen] List<PositionDTO> positionsDTO,
            [Frozen] Mock<IPositionService> positionServiceMock,
            [Frozen] Mock<IMapper> mapper)
        {
            // Arrange
            positionServiceMock.Setup(m => m.GetAsync())            
                .ReturnsAsync(positions);
            mapper.Setup(m => m.Map<ICollection<PositionDTO>>(positions))
                .Returns(positionsDTO);
            var positionController =new PositionController(positionServiceMock.Object,mapper.Object);

            // Act
            var actualResult = await positionController.Get();

            // Assert      
            var result = actualResult.Result as OkObjectResult; 
            
            Assert.Equal(positionsDTO, result.Value );
            Assert.True(actualResult.Result is OkObjectResult);
            positionServiceMock.Verify(m => m.GetAsync(), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task GetById_PositionId_ReturnsPositionWithRequestedId(
           [Frozen] Position position,
           [Frozen] PositionDTO positionDTO,
           [Frozen] Mock<IPositionService> positionServiceMock,
           [Frozen] Mock<IMapper> mapper
            )
        {
            // Arrange           
            positionServiceMock.Setup(p => p.GetAsync(position.Id))
                .ReturnsAsync(position);
            mapper.Setup(m => m.Map<PositionDTO>(position))
                .Returns(positionDTO);

            var positionController = new PositionController(positionServiceMock.Object, mapper.Object);

            // Act
            var actualResult = await positionController.Get(position.Id);

            // Assert    
            var result = actualResult.Result as OkObjectResult;
            Assert.Equal(positionDTO, result.Value);
            Assert.True(actualResult.Result is OkObjectResult);
            positionServiceMock.Verify(m => m.GetAsync(position.Id), Times.Once);
        }


        [Fact]
        public async Task GetById_PositionId_ReturnsNotFound()
        {
            // Arrange     
            var positionServiceMock =new Mock<IPositionService>();
            var mapper = new Mock<IMapper>();
            Position position = new Position { Id = 1 };
            PositionDTO positionDTO = new PositionDTO { Id = 1 };            
            positionServiceMock.Setup(p => p.GetAsync(position.Id))
                .ReturnsAsync(null as Position);
            mapper.Setup(m => m.Map<PositionDTO>(position))
                .Returns(positionDTO);

            var positionController = new PositionController(positionServiceMock.Object, mapper.Object);

            // Act
            var actualResult = await positionController.Get(position.Id);

            // Assert                          
            Assert.True(actualResult.Result is NotFoundResult);
            positionServiceMock.Verify(m => m.GetAsync(position.Id), Times.Once);
        }
    

        [Theory, AutoMoqData]
        public async Task Post_PositionDTO_ReturnsPositionDTO(
           [Frozen] Position position,
           [Frozen] PositionDTO positionDTO,
           [Frozen] Mock<IPositionService> positionServiceMock,
           [Frozen] Mock<IMapper> mapper)
        {
            // Arrange            
            positionServiceMock.Setup(m => m.Add(position))
                .ReturnsAsync(position);            
            mapper.Setup(m => m.Map<Position>(positionDTO))
                .Returns(position);
            var positionController = new PositionController(positionServiceMock.Object, mapper.Object);

            // Act
            var actualResult = await positionController.Post(positionDTO);

            // Assert 
            var result = actualResult.Result as OkObjectResult;
            Assert.Equal(positionDTO, result.Value);
            positionServiceMock.Verify(m => m.Add(position), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task Put_PositionDTO_ReturnsNoContent(
           [Frozen] Position position,
           [Frozen] PositionDTO positionDTO,
           [Frozen] Mock<IPositionService> positionServiceMock,
           [Frozen] Mock<IMapper> mapper)
        {
            // Arrange            
            positionServiceMock.Setup(m => m.Update(position,position.Id))
                .ReturnsAsync(true);
            mapper.Setup(m => m.Map<Position>(positionDTO))
                .Returns(position);
            var positionController = new PositionController(positionServiceMock.Object, mapper.Object);

            // Act
            var actualResult = await positionController.Put(positionDTO,position.Id);

            // Assert             
            Assert.True(actualResult.Result is NoContentResult);
            positionServiceMock.Verify(m => m.Update(position, position.Id), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task Put_PositionDTO_ReturnsNotFound(
           [Frozen] Position position,
           [Frozen] PositionDTO positionDTO,
           [Frozen] Mock<IPositionService> positionServiceMock,
           [Frozen] Mock<IMapper> mapper)
        {
            // Arrange            
            positionServiceMock.Setup(m => m.Update(position, position.Id))
                .ReturnsAsync(false);
            mapper.Setup(m => m.Map<Position>(positionDTO))
                .Returns(position);
            var positionController = new PositionController(positionServiceMock.Object, mapper.Object);

            // Act
            var actualResult = await positionController.Put(positionDTO, position.Id);

            // Assert             
            Assert.True(actualResult.Result is NotFoundResult);
            Assert.NotNull(actualResult);
        }

        [Theory, AutoMoqData]
        public async Task Delete_PositionId_ReturnsNoContent(
           [Frozen] int id,           
           [Frozen] Mock<IPositionService> positionServiceMock,
           [Frozen] Mock<IMapper> mapper)
        {
            // Arrange            
            positionServiceMock.Setup(m => m.Remove(id))
                .ReturnsAsync(true);            
            var positionController = new PositionController(positionServiceMock.Object, mapper.Object);

            // Act
            var actualResult = await positionController.Remove(id);

            // Assert             
            Assert.True(actualResult is NoContentResult);
            positionServiceMock.Verify(m => m.Remove(id), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task Delete_PositionId_ReturnsFound(
           [Frozen] int id,
           [Frozen] Mock<IPositionService> positionServiceMock,
           [Frozen] Mock<IMapper> mapper)
        {
            // Arrange            
            positionServiceMock.Setup(m => m.Remove(id))
                .ReturnsAsync(false);
            var positionController = new PositionController(positionServiceMock.Object, mapper.Object);

            // Act
            var actualResult = await positionController.Remove(id);

            // Assert             
            Assert.True(actualResult is NotFoundResult);
            positionServiceMock.Verify(m => m.Remove(id), Times.Once);
        }
    }
}
