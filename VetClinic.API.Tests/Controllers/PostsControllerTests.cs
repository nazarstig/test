using AutoFixture.Xunit2;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using VetClinic.API.Controllers;
using VetClinic.API.DTO.Post;
using VetClinic.API.DTO.Queries;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using Xunit;

namespace VetClinic.API.Tests.Controllers
{
    public class PostsControllerTests
    {
        Mock<IMapper> _mapper;

        public PostsControllerTests()
        {

            _mapper = new Mock<IMapper>();
        }

        [Theory, AutoMoqData]
        public async Task GetAsync_WhenCalled_ReturnsOkObjectResult(
            [Frozen] Mock<IPostService> _postService,
            [Frozen] Mock<PaginationQuery> paginationQuery)
        {
            //Arrange
            _mapper.Setup(x => x.Map<ReadPostDto>(It.IsAny<Post>()));
            var Sut = new PostsController(_postService.Object, _mapper.Object);

            //Act
            var actual = await Sut.GetAsync(paginationQuery.Object);

            //Assert
            Assert.NotNull(actual);
            Assert.IsType<OkObjectResult>(actual);
        }

        [Theory, AutoMoqData]
        public async Task GetAsync_PostId_ReturnsOkObjectResult(
            [Frozen] Mock<IPostService> _postService)
        {
            //Arrange
            int id = 2;
            _mapper.Setup(x => x.Map<ReadPostDto>(It.IsAny<Post>()));
            var Sut = new PostsController(_postService.Object, _mapper.Object);

            //Act
            var actual = await Sut.GetAsync(id);

            //Assert
            Assert.NotNull(actual);
            Assert.IsType<OkObjectResult>(actual);
        }

        [Theory, AutoMoqData]
        public async Task PostAsync_CreatePostDto_ReturnsCreateAnimalDto(
            [Frozen] Mock<IPostService> _postService,
            [Frozen] Mock<CreatePostDto> postDto)
        {
            //Arrange
            _mapper.Setup(x => x.Map<CreatePostDto>(It.IsAny<Post>()));
            var Sut = new PostsController(_postService.Object, _mapper.Object);

            //Act
            var actual = await Sut.PostAsync(postDto.Object);

            //Assert
            _postService.Verify(x => x.CreatePost(It.IsAny<Post>()));
            Assert.NotNull(actual);
            Assert.IsType<CreatedResult>(actual);
        }

        [Theory, AutoMoqData]
        public async Task PutAsync_UpdatePostDto_PutSuccessReturnsNoContent(
            [Frozen] Mock<IPostService> _postService,
            [Frozen] Mock<UpdatePostDto> postDto)
        {
            //Arrange
            int id = 1;
            _mapper.Setup(x => x.Map<UpdatePostDto>(It.IsAny<Post>()));
            var Sut = new PostsController(_postService.Object, _mapper.Object);

            //Act
            var actual = await Sut.PutAsync(id, postDto.Object);

            //Assert
            _postService.Verify(x => x.UpdatePost(id, It.IsAny<Post>()));
            Assert.NotNull(actual);
            Assert.IsType<NoContentResult>(actual);
        }

        [Theory, AutoMoqData]
        public async Task DeleteAsync_PostId_DeleteSuccessReturnsNoContent(
            [Frozen] Mock<IPostService> _postService)
        {
            //Arrange
            int id = 1;
            var Sut = new PostsController(_postService.Object, _mapper.Object);

            //Act
            var actual = await Sut.DeleteAsync(id);

            //Assert
            _postService.Verify(x => x.RemovePost(It.IsAny<Post>()));
            Assert.NotNull(actual);
            Assert.IsType<NoContentResult>(actual);
        }
    }
}
