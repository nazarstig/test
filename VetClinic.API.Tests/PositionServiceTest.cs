﻿using AutoFixture.Xunit2;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.BLL.Services.Realizations;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;

using Xunit;

namespace VetClinic.API.Tests
{
    public class PositionServiceTest
    {      
        [Theory, AutoMoqData]
        public async Task PositionService_GetEqualCount([Frozen]List<Position> positions,[Frozen] Mock<IRepositoryWrapper> repositoryMock )
        {
            // Arrange
            repositoryMock.Setup(x => x.PositionRepository
            .GetAsync(null, null, null, false))
                .ReturnsAsync(positions);
             var positionService = new PositionService(repositoryMock.Object);

            // Act
            var actual = await positionService.GetAsync();

            // Assert
            Assert.Equal(positions.Count, actual.Count);
            repositoryMock.Verify(m => m.PositionRepository.GetAsync(null,null,null,false), Times.Once);
            
        }

        [Theory, AutoMoqData]
        public async Task PositionService_GetPositionById_ReturnsPositionWithRequestedId([Frozen] Position position, [Frozen] Mock<IRepositoryWrapper> repositoryMock)
        {
            // Arrange
            int id = position.Id;
            repositoryMock.Setup(x => x.PositionRepository
            .GetFirstOrDefaultAsync(p=>p.Id==id, null, false))
                .ReturnsAsync(position);
            var positionService = new PositionService(repositoryMock.Object);

            // Act
            var actual = await positionService.GetAsync(id);

            // Assert
            Assert.Equal(position.Id, actual.Id);
            repositoryMock.Verify(m => m.PositionRepository.GetFirstOrDefaultAsync(p => p.Id == id, null, false), Times.Once);

        }

        [Theory, AutoMoqData]
        public async Task PositionService_AddPosition_ReturnsAdedPosition([Frozen] Position position, [Frozen] Mock<IRepositoryWrapper> repositoryMock)
        {
            // Arrange            
            repositoryMock.Setup(x => x.PositionRepository
            .Add(position));           
            var positionService = new PositionService(repositoryMock.Object);

            // Act
            var actual = await positionService.Add(position);

            // Assert
            Assert.Equal(position.Id, actual.Id);
            repositoryMock.Verify(m => m.PositionRepository.Add(position), Times.Once);
            repositoryMock.Verify(m => m.SaveAsync(), Times.Once);

        }

        [Theory, AutoMoqData]
        public async Task PositionService_RemovePosition_ReturnsTrue([Frozen] Position position, [Frozen] Mock<IRepositoryWrapper> repositoryMock)
        {
            // Arrange
            int id = position.Id;
            repositoryMock.Setup(x => x.PositionRepository
            .GetFirstOrDefaultAsync(p=>p.Id==id, null, false))
                .ReturnsAsync(position);
            var positionService = new PositionService(repositoryMock.Object);

            // Act
            var actual = await positionService.Remove(position.Id);

            // Assert      
            Assert.True(actual);
            repositoryMock.Verify(m => m.PositionRepository.Remove(position), Times.Once);            

        }

        [Theory, AutoMoqData]
        public async Task PositionService_RemovePosition_ReturnsFalce([Frozen] Mock<IRepositoryWrapper> repositoryMock)
        {
            // Arrange    
            int id = 1;
            repositoryMock.Setup(x => x.PositionRepository
            .GetFirstOrDefaultAsync(p => p.Id == id, null, false))
                .ReturnsAsync(value: null);
            var positionService = new PositionService(repositoryMock.Object);

            // Act
            var actual = await positionService.Remove(id);

            // Assert      
            Assert.False(actual);
            repositoryMock.Verify(m => m.PositionRepository.Remove(null), Times.Never);
        }

        [Theory, AutoMoqData]
        public async Task PositionService_UpdateNullPosition_ReturnsFalce([Frozen] Mock<IRepositoryWrapper> repositoryMock)
        {
            // Arrange             
            Position position = null;            
            var positionService = new PositionService(repositoryMock.Object);

            // Act
            var actual = await positionService.Update(position,1);

            // Assert      
            Assert.False(actual);
            repositoryMock.Verify(m => m.PositionRepository.Update(null), Times.Never);
        }

        [Theory, AutoMoqData]
        public async Task PositionService_UpdatePosition_ReturnsTrue([Frozen] Position position, [Frozen] Mock<IRepositoryWrapper> repositoryMock)
        {
            // Arrange      
            int id = position.Id;
            repositoryMock.Setup(m => m.PositionRepository
            .IsAnyAsync(p => p.Id == id))
                .ReturnsAsync(true);                      
            var positionService = new PositionService(repositoryMock.Object);

            // Act
            var actual = await positionService.Update(position, id);

            // Assert      
            Assert.True(actual);
            repositoryMock.Verify(m => m.PositionRepository.Update(position), Times.Once);
        }

        [Theory, AutoMoqData]
        public async Task PositionService_UpdatePositionUnableId_ReturnsFalce([Frozen] Position position, [Frozen] Mock<IRepositoryWrapper> repositoryMock)
        {
            // Arrange      
            int id = position.Id;
            repositoryMock.Setup(m => m.PositionRepository
            .IsAnyAsync(p => p.Id == id))
                .ReturnsAsync(false);
            var positionService = new PositionService(repositoryMock.Object);

            // Act
            var actual = await positionService.Update(position, id);

            // Assert      
            Assert.False(actual);
            repositoryMock.Verify(m => m.PositionRepository.Update(position), Times.Never);
        }

    }


}
