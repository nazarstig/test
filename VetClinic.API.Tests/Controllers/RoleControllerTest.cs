using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.API.Controllers;
using VetClinic.API.DTO.Role;
using Xunit;

namespace VetClinic.API.Tests.Controllers
{
    public class RoleControllerTest
    {
        public RoleControllerTest()
        {
            Fixture = new Fixture().Customize(new AutoMoqCustomization());
            RoleStore = Fixture.Create<IRoleStore<IdentityRole>>();
            Mapper = new Mock<IMapper>();
        }
        public IFixture Fixture { get; }
        public IRoleStore<IdentityRole> RoleStore { get; }
        public Mock<IMapper> Mapper { get; }

        [Fact]
        public void Index_NoParams_ReturnOk()
        {
            //arrange
            var roleManagerMock = new Mock<RoleManager<IdentityRole>>(RoleStore, null, null, null, null);

            var controller = new RoleController(roleManagerMock.Object, Mapper.Object);

            //act
            var result = controller.Index();
            var contentResult = result as ObjectResult;

            //assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<IEnumerable<RoleDto>>(contentResult.Value);
            Assert.NotEmpty(contentResult.Value.ToString());
        }

        [Fact]
        public async Task Show_Id_ReturnOk()
        {
            //arrange
            IdentityRole role = new IdentityRole() { Name = "Vasya" };
            RoleDto dto = new RoleDto() { Id = "fan", Name = "Vasya" };

            var roleManagerMock = new Mock<RoleManager<IdentityRole>>(RoleStore, null, null, null, null);
            roleManagerMock
                .Setup(c => c.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(role);

            Mapper.Setup(m => m.Map<IdentityRole, RoleDto>(role)).Returns(dto);

            var controller = new RoleController(roleManagerMock.Object, Mapper.Object);

            //act
            var result = await controller.Show("LOL");
            var contentResult = result as OkObjectResult;

            //assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<RoleDto>(contentResult.Value);
            Assert.Equal(dto, contentResult.Value);
        }


        [Fact]
        public async Task Show_NoSuchId_ReturnNotFound()
        {
            //arrange
            var roleManagerMock = new Mock<RoleManager<IdentityRole>>(RoleStore, null, null, null, null);
            roleManagerMock
                .Setup(c => c.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(null as IdentityRole);

            var controller = new RoleController(roleManagerMock.Object, Mapper.Object);

            //act
            var result = await controller.Show("LOL");

            //assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_RoleDTO_ReturnCreated()
        {
            //arrange
            var dto = new CreateRoleDto() { Name = "Vasya" };

            var roleManagerMock = new Mock<RoleManager<IdentityRole>>(RoleStore, null, null, null, null);
            roleManagerMock
                .Setup(c => c.CreateAsync(It.IsAny<IdentityRole>()))
                .ReturnsAsync(IdentityResult.Success);

            var controller = new RoleController(roleManagerMock.Object, Mapper.Object);

            //act
            var result = await controller.Create(dto);
            var contentResult = result as ObjectResult;

            //assert
            Assert.NotNull(result);
            Assert.IsType<CreatedResult>(result);
            Assert.IsAssignableFrom<CreateRoleDto>(contentResult.Value);
            Assert.Equal(dto.Name, ((CreateRoleDto)contentResult.Value).Name);
        }

        [Fact]
        public async Task Update_IdDto_ReturnNoContent()
        {
            //arrange
            var role = new IdentityRole() { Id = "S", Name = "Charlamane" };
            CreateRoleDto dto = new CreateRoleDto() { Name = "Vasya" };

            var roleManagerMock = new Mock<RoleManager<IdentityRole>>(RoleStore, null, null, null, null);
            roleManagerMock.Setup(c => c.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(role);

            roleManagerMock.Setup(c => c.UpdateAsync(It.IsAny<IdentityRole>()))
                .ReturnsAsync(IdentityResult.Success);

            Mapper.Setup(m => m.Map<CreateRoleDto, IdentityRole>(It.IsAny<CreateRoleDto>())).Returns(role);

            var controller = new RoleController(roleManagerMock.Object, Mapper.Object);

            //act
            var result = await controller.Update(It.IsAny<string>(), dto);

            //assign
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Destroy_Id_ReturnOk()
        {
            //arrange
            var roleManagerMock = new Mock<RoleManager<IdentityRole>>(RoleStore, null, null, null, null);
            roleManagerMock.Setup(c => c.DeleteAsync(It.IsAny<IdentityRole>()))
                .ReturnsAsync(IdentityResult.Success);

            var controller = new RoleController(roleManagerMock.Object, Mapper.Object);

            //act
            var result = await controller.Destroy(It.IsAny<string>());

            //assign
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}