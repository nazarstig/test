using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.API.Controllers;
using VetClinic.API.DTO.Position.PositionDTO;
using VetClinic.API.DTO.Queries;
using VetClinic.API.DTO.Responses;
using VetClinic.BLL.Domain;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using Xunit;

namespace VetClinic.API.Tests.Controllers
{
    public class PositionControllerTests
    {
        private readonly Mock<IPositionService> positionServiceMock;
        private readonly Mock<IMapper> mapper;
        private readonly PositionController positionController;
        public PositionControllerTests()
        {
            var fixture = new Fixture();
            positionServiceMock = fixture.Freeze<Mock<IPositionService>>();
            mapper = fixture.Freeze<Mock<IMapper>>();
            positionController = new PositionController(positionServiceMock.Object, mapper.Object);

        }


        [Theory, AutoMoqData]
        public async Task GetAll_Positions_ReturnsTrue(
            [Frozen] List<Position> positions,
            [Frozen] List<PositionDto> positionsDTO,
            [Frozen] PaginationQuery paginationQuery,
            [Frozen] PaginationFilter paginationFilter)
        {
            // Arrange
            positionServiceMock.Setup(m => m.GetPositionAsync(paginationFilter))
                .ReturnsAsync(positions);
            mapper.Setup(m => m.Map<ICollection<PositionDto>>(positions))
                .Returns(positionsDTO);
            mapper.Setup(m => m.Map<PaginationFilter>(paginationQuery))
                .Returns(paginationFilter);            

            // Act
            var actualResult = await positionController.GetAsync(paginationQuery);

            // Assert      
            var result = actualResult as OkObjectResult;
            var resultData = result.Value as PagedResponse<PositionDto>;

            Assert.Equal(positionsDTO, resultData.Data);
            Assert.True(actualResult is OkObjectResult);
            positionServiceMock.Verify(m => m.GetPositionAsync(paginationFilter), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task GetById_PositionId_ReturnsPositionWithRequestedId(
           [Frozen] Position position,
           [Frozen] PositionDto positionDTO)
        {
            // Arrange           
            positionServiceMock.Setup(p => p.GetPositionByIdAsync(position.Id))
                .ReturnsAsync(position);
            mapper.Setup(m => m.Map<PositionDto>(position))
                .Returns(positionDTO);

            // Act
            var actualResult = await positionController.GetAsync(position.Id);

            // Assert    
            var result = actualResult as OkObjectResult;
            var resultData = result.Value as Response<PositionDto>;

            Assert.Equal(positionDTO, resultData.Data);
            Assert.True(actualResult is OkObjectResult);
            positionServiceMock.Verify(m => m.GetPositionByIdAsync(position.Id), Times.Once);
        }


        [Fact]
        public async Task GetById_PositionId_ReturnsNotFound()
        {
            // Arrange     
            var positionServiceMock1 = new Mock<IPositionService>();
            var mapper1 = new Mock<IMapper>();
            Position position = new Position { Id = 1 };
            PositionDto positionDTO = new PositionDto { Id = 1 };

            positionServiceMock1.Setup(p => p.GetPositionByIdAsync(position.Id))
                .ReturnsAsync(null as Position);
            mapper1.Setup(m => m.Map<PositionDto>(position))
                .Returns(positionDTO);

            var positionController1 = new PositionController(positionServiceMock1.Object, mapper1.Object);

            // Act
            var actualResult = await positionController1.GetAsync(position.Id);

            // Assert                          
            Assert.True(actualResult is NotFoundResult);
            positionServiceMock1.Verify(m => m.GetPositionByIdAsync(position.Id), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task Post_PositionDTO_ReturnsPositionDTO(
           [Frozen] Position position,
           [Frozen] PositionDto positionDTO)
        {
            // Arrange            
            positionServiceMock.Setup(m => m.AddPositionAsync(position))
                .ReturnsAsync(position);
            mapper.Setup(m => m.Map<Position>(positionDTO))
                .Returns(position);
            mapper.Setup(m => m.Map<PositionDto>(position))
               .Returns(positionDTO);

            // Act
            await positionController.PostAsync(positionDTO);

            // Assert                        
            positionServiceMock.Verify(m => m.AddPositionAsync(position), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task Put_PositionDTO_ReturnsNoContent(
           [Frozen] Position position,
           [Frozen] PositionDto positionDTO)
        {
            // Arrange            
            positionServiceMock.Setup(m => m.UpdatePositionAsync(position, position.Id))
                .ReturnsAsync(true);
            mapper.Setup(m => m.Map<Position>(positionDTO))
                .Returns(position);

            // Act
            var actualResult = await positionController.PutAsync(positionDTO, position.Id);

            // Assert             
            Assert.True(actualResult is NoContentResult);
            positionServiceMock.Verify(m => m.UpdatePositionAsync(position, position.Id), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task Put_PositionDTO_ReturnsNotFound(
           [Frozen] Position position,
           [Frozen] PositionDto positionDTO)
        {
            // Arrange            
            positionServiceMock.Setup(m => m.UpdatePositionAsync(position, position.Id))
                .ReturnsAsync(false);
            mapper.Setup(m => m.Map<Position>(positionDTO))
                .Returns(position);

            // Act
            var actualResult = await positionController.PutAsync(positionDTO, position.Id);

            // Assert             
            Assert.True(actualResult is NotFoundResult);
            Assert.NotNull(actualResult);
        }


        [Theory, AutoMoqData]
        public async Task Delete_PositionId_ReturnsNoContent(
           [Frozen] int id)
        {
            // Arrange            
            positionServiceMock.Setup(m => m.RemovePositionAsync(id))
                .ReturnsAsync(true);

            // Act
            var actualResult = await positionController.DeleteAsync(id);

            // Assert             
            Assert.True(actualResult is NoContentResult);
            positionServiceMock.Verify(m => m.RemovePositionAsync(id), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task Delete_PositionId_ReturnsNotFound(
           [Frozen] int id)
        {
            // Arrange            
            positionServiceMock.Setup(m => m.RemovePositionAsync(id))
                .ReturnsAsync(false);

            // Act
            var actualResult = await positionController.DeleteAsync(id);

            // Assert             
            Assert.True(actualResult is NotFoundResult);
            positionServiceMock.Verify(m => m.RemovePositionAsync(id), Times.Once);
        }
    }
}
