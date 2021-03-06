using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using Moq;
using VetClinic.API.Tests;
using VetClinic.BLL.Services.Realizations;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;
using Xunit;

namespace VetClinic.BLL.Tests.Services
{
    public class PositionServiceTest
    {
        private readonly Mock<IRepositoryWrapper> repositoryMock;
        private readonly PositionService positionService;
        public PositionServiceTest()
        {
            var fixture = new Fixture();
            repositoryMock = fixture.Freeze<Mock<IRepositoryWrapper>>();
            positionService = new PositionService(repositoryMock.Object);
        }


        [Theory, AutoMoqData]
        public async Task GetAll_EqualCount([Frozen] List<Position> positions)
        {
            // Arrange
            repositoryMock.Setup(x => x.PositionRepository
            .GetAsync(null, null, null, null, null, false))
                .ReturnsAsync(positions);

            // Act
            var actual = await positionService.GetPositionAsync();

            // Assert
            Assert.Equal(positions.Count, actual.Count);
            repositoryMock.Verify(m => m.PositionRepository.GetAsync(null, null, null, null, null, false), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task GetById_PositionId_ReturnsPositionWithRequestedId([Frozen] Position position)
        {
            // Arrange
            int id = position.Id;
            repositoryMock.Setup(x => x.PositionRepository
            .GetFirstOrDefaultAsync(p => p.Id == id, null, false))
                .ReturnsAsync(position);

            // Act
            var actual = await positionService.GetPositionByIdAsync(id);

            // Assert
            Assert.Equal(position.Id, actual.Id);
            repositoryMock.Verify(m => m.PositionRepository.GetFirstOrDefaultAsync(p => p.Id == id, null, false), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task Add_Position_ReturnsAdedPosition([Frozen] Position position)
        {
            // Arrange            
            repositoryMock.Setup(x => x.PositionRepository
            .Add(position));

            // Act
            var result = await positionService.AddPositionAsync(position);

            // Assert 
            Assert.Equal(result.PositionName, position.PositionName);
            Assert.Equal(result.Salary, position.Salary);

        }


        [Theory, AutoMoqData]
        public async Task Remove_Position_ReturnsTrue([Frozen] Position position)
        {
            // Arrange
            int id = position.Id;
            repositoryMock.Setup(x => x.PositionRepository
            .GetFirstOrDefaultAsync(p => p.Id == id, null, false))
                .ReturnsAsync(position);

            // Act
            var actual = await positionService.RemovePositionAsync(position.Id);

            // Assert      
            Assert.True(actual);
            repositoryMock.Verify(m => m.PositionRepository.Remove(position), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task Removee_PositionId_ReturnsFalce([Frozen] int id)
        {
            // Arrange 
            repositoryMock.Setup(x => x.PositionRepository
           .GetFirstOrDefaultAsync(p => p.Id == id, null, false))
               .ReturnsAsync(value: null);

            // Act
            var actual = await positionService.RemovePositionAsync(id);

            // Assert      
            Assert.False(actual);
            repositoryMock.Verify(m => m.PositionRepository.Remove(null), Times.Never);
        }


        [Theory, AutoMoqData]
        public async Task Update_NullPosition_ReturnsFalce(Position position)
        {
            // Arrange             
            position = null;

            // Act
            var actual = await positionService.UpdatePositionAsync(position, 1);

            // Assert      
            Assert.False(actual);
            repositoryMock.Verify(m => m.PositionRepository.Update(null), Times.Never);
        }


        [Theory, AutoMoqData]
        public async Task Update_Position_ReturnsTrue([Frozen] Position position)
        {
            // Arrange      
            int id = position.Id;
            repositoryMock.Setup(m => m.PositionRepository
            .IsAnyAsync(p => p.Id == id))
                .ReturnsAsync(true);

            // Act
            var actual = await positionService.UpdatePositionAsync(position, id);

            // Assert      
            Assert.True(actual);
            repositoryMock.Verify(m => m.PositionRepository.Update(position), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task Update_PositionUnableId_ReturnsFalce([Frozen] Position position)
        {
            // Arrange      
            int id = position.Id;
            repositoryMock.Setup(m => m.PositionRepository
            .IsAnyAsync(p => p.Id == id))
                .ReturnsAsync(false);

            // Act
            var actual = await positionService.UpdatePositionAsync(position, id);

            // Assert      
            Assert.False(actual);
            repositoryMock.Verify(m => m.PositionRepository.Update(position), Times.Never);
        }
    }
}
