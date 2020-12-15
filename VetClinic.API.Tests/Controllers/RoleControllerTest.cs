using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using System.Text.Json;
using System.Threading.Tasks;
using VetClinic.API.Controllers;
using Xunit;
using VetClinic.API.DTO;

namespace VetClinic.API.Tests.Controllers
{
    public class RoleControllerTest 
    {
        [Fact]
        public async Task Index_NoParams_ReturnOk()
        {
            //arrange
            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            var roleManagerMock = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);

            var controller = new RoleController(roleManagerMock.Object);


            //act
            var result = controller.Index();
            var contentResult = result as ObjectResult;

            //assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.NotEmpty(contentResult.Value.ToString());
        }

        [Fact]
        public async Task Get_Id_ReturnOk()
        {
            //arrange
            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            var roleManagerMock = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);
            roleManagerMock
                .Setup(c => c.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new IdentityRole());

            var controller = new RoleController(roleManagerMock.Object);


            //act
            var result = await controller.Show("LOL");
            var contentResult = result as ObjectResult;

            //assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.NotEmpty(contentResult.Value.ToString());
        }



        [Fact]
        public async Task Create_RoleDTO_ReturnCreated()
        {
            //arrange
            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            var roleManagerMock = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);
            roleManagerMock
                .Setup(c => c.CreateAsync(It.IsAny<IdentityRole>()))
                .ReturnsAsync(IdentityResult.Success);

            var controller = new RoleController(roleManagerMock.Object);


            //act
            var result = await controller.Create(new RoleDto());
            var contentResult = result as ObjectResult;

            //assert
            Assert.NotNull(result);
            Assert.IsType<CreatedResult>(result);
            Assert.NotEmpty(contentResult.Value.ToString());
        }

        [Fact]
        public async Task Update_IdDto_ReturnNoContent()
        {
            //arrange
            var role = new IdentityRole();
            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            var roleManagerMock = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);
            roleManagerMock.Setup(c => c.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(role);

            roleManagerMock.Setup(c => c.UpdateAsync(It.IsAny<IdentityRole>()))
                .ReturnsAsync(IdentityResult.Success);

            var controller = new RoleController(roleManagerMock.Object);

            //act
            var result = await controller.Update(It.IsAny<string>(), new RoleDto());

            //assign
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Destroy_Id_ReturnOk()
        {
            //arrange
            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            var roleManagerMock = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);
            roleManagerMock.Setup(c => c.DeleteAsync(It.IsAny<IdentityRole>()))
                .ReturnsAsync(IdentityResult.Success);

            var controller = new RoleController(roleManagerMock.Object);

            //act
            var result = await controller.Destroy(It.IsAny<string>());

            //assign
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
