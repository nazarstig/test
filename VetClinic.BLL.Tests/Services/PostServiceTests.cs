using AutoFixture.Xunit2;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.API.Tests;
using VetClinic.BLL.Services.Realizations;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;
using Xunit;

namespace VetClinic.BLL.Tests.Services
{
    public class PostServiceTests
    {
        [Theory, AutoMoqData]
        public async Task CreatePost_PostCreated_CreateSuccess(
            [Frozen] Mock<IRepositoryWrapper> mockRepositoryWrapper,
            [Frozen] Mock<Post> post)
        {
            //Arrange
            mockRepositoryWrapper.Setup(x => x.PostRepository.Add(It.IsAny<Post>()));
            var Sut = new PostService(mockRepositoryWrapper.Object);

            //Act
            await Sut.CreatePost(post.Object);

            //Assert
            mockRepositoryWrapper.Verify(x => x.PostRepository.Add(It.IsAny<Post>()));
            mockRepositoryWrapper.Verify(x => x.SaveAsync());
        }

        [Theory, AutoMoqData]
        public async Task GetAll_GetSuccess(
           [Frozen] Mock<IRepositoryWrapper> mockRepositoryWrapper,
           [Frozen] ICollection<Post> posts)
        {
            //Arrange
            mockRepositoryWrapper.Setup(x => x.PostRepository
            .GetAsync(null, It.IsAny<Func<IQueryable<Post>, IIncludableQueryable<Post, object>>>(), null, null, null, false))
               .ReturnsAsync(posts);
            var Sut = new PostService(mockRepositoryWrapper.Object);

            //Act
            var actual = await Sut.GetAllAsync();

            //Assert
            Assert.IsType<List<Post>>(actual);
            mockRepositoryWrapper.Verify(x => x.PostRepository.GetAsync(null, It.IsAny<Func<IQueryable<Post>, IIncludableQueryable<Post, object>>>(), null, null, null, false));
        }

        [Theory, AutoMoqData]
        public async Task RemovePost_PostRemoved_RemoveSuccess(
           [Frozen] Mock<IRepositoryWrapper> mockRepositoryWrapper,
           [Frozen] Mock<Post> post)
        {
            //Arrange
            mockRepositoryWrapper.Setup(x => x.PostRepository.Remove(It.IsAny<Post>()));
            var Sut = new PostService(mockRepositoryWrapper.Object);

            //Act
            await Sut.RemovePost(post.Object);

            //Assert
            mockRepositoryWrapper.Verify(x => x.PostRepository.Remove(It.IsAny<Post>()));
            mockRepositoryWrapper.Verify(x => x.SaveAsync());
        }
    }
}
