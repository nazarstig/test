using AutoFixture.Xunit2;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.API.Controllers;
using VetClinic.API.DTO.Responses;
using VetClinic.API.DTO.Role;
using Xunit;

namespace VetClinic.API.Tests.Controllers
{
    public class RolesControllerTest
    {
        [Theory, AutoMoqData]
        public void Index_NoParams_ReturnOk(IQueryable<IdentityRole> roles,
            [Frozen] Mock<RoleManager<IdentityRole>> roleManagerMock,
            [Frozen] Mock<IMapper> mapper)
        {
            //Arrange
            roleManagerMock.SetupGet(c => c.Roles).Returns(roles);

            var sut = new RolesController(roleManagerMock.Object, mapper.Object);

            //Act
            var result = sut.Get();
            var contentResult = result as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<Response<IEnumerable<RoleDto>>>(contentResult.Value);
        }

        [Theory, AutoMoqData]
        public async Task Show_Id_ReturnOk(IdentityRole role, RoleDto dto,
            [Frozen] Mock<RoleManager<IdentityRole>> roleManagerMock,
            [Frozen] Mock<IMapper> mapper)
        {
            //Arrange
            roleManagerMock
                .Setup(c => c.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(role);

            mapper.Setup(m => m.Map<IdentityRole, RoleDto>(role)).Returns(dto);

            var sut = new RolesController(roleManagerMock.Object, mapper.Object);

            //Act
            var result = await sut.GetAsync("test");
            var contentResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<Response<RoleDto>>(contentResult.Value);
            Assert.Equal(dto, ((Response<RoleDto>)contentResult.Value).Data);
        }

        [Theory, AutoMoqData]
        public async Task Show_NoSuchId_ReturnNotFound([Frozen] Mock<RoleManager<IdentityRole>> roleManagerMock,
            [Frozen] Mock<IMapper> mapper)
        {
            //Arrange
            roleManagerMock
                .Setup(c => c.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(null as IdentityRole);

            var sut = new RolesController(roleManagerMock.Object, mapper.Object);

            //Act
            var result = await sut.GetAsync("test");

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory, AutoMoqData]
        public async Task Create_RoleDTO_ReturnCreated(CreateRoleDto dto,
            [Frozen] Mock<RoleManager<IdentityRole>> roleManagerMock,
            [Frozen] Mock<IMapper> mapper)
        {
            //Arrange
            roleManagerMock
                .Setup(c => c.CreateAsync(It.IsAny<IdentityRole>()))
                .ReturnsAsync(IdentityResult.Success);

            var sut = new RolesController(roleManagerMock.Object, mapper.Object);

            //Act
            var result = await sut.PostAsync(dto);
            var contentResult = result as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<CreatedResult>(result);
            Assert.IsAssignableFrom<Response<CreateRoleDto>>(contentResult.Value);
            Assert.Equal(dto.Name, ((Response<CreateRoleDto>)contentResult.Value).Data.Name);
        }

        [Theory, AutoMoqData]
        public async Task Update_NoSuchId_ReturnNotFound(IdentityRole role, CreateRoleDto dto,
            [Frozen] Mock<RoleManager<IdentityRole>> roleManagerMock,
            [Frozen] Mock<IMapper> mapper)
        {
            //Arrange
            roleManagerMock.Setup(c => c.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((IdentityRole)null);

            mapper.Setup(m => m.Map<CreateRoleDto, IdentityRole>(It.IsAny<CreateRoleDto>())).Returns(role);

            var sut = new RolesController(roleManagerMock.Object, mapper.Object);

            //Act
            var result = await sut.PutAsync(It.IsAny<string>(), dto);

            //Assign
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory, AutoMoqData]
        public async Task Update_IdDto_ReturnNoContent(IdentityRole role, CreateRoleDto dto,
            [Frozen] Mock<RoleManager<IdentityRole>> roleManagerMock,
            [Frozen] Mock<IMapper> mapper)
        {
            //Arrange
            roleManagerMock.Setup(c => c.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(role);

            roleManagerMock.Setup(c => c.UpdateAsync(It.IsAny<IdentityRole>()))
                .ReturnsAsync(IdentityResult.Success);

            mapper.Setup(m => m.Map<CreateRoleDto, IdentityRole>(It.IsAny<CreateRoleDto>())).Returns(role);

            var sut = new RolesController(roleManagerMock.Object, mapper.Object);

            //Act
            var result = await sut.PutAsync(It.IsAny<string>(), dto);

            //Assign
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }

        [Theory, AutoMoqData]
        public async Task Destroy_Id_ReturnOk([Frozen] Mock<RoleManager<IdentityRole>> roleManagerMock,
            [Frozen] Mock<IMapper> mapper)
        {
            //Arrange
            roleManagerMock.Setup(c => c.DeleteAsync(It.IsAny<IdentityRole>()))
                .ReturnsAsync(IdentityResult.Success);

            var sut = new RolesController(roleManagerMock.Object, mapper.Object);

            //Act
            var result = await sut.DeleteAsync(It.IsAny<string>());

            //Assign
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }
    }
}