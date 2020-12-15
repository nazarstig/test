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

namespace VetClinic.API.Tests.Controllers
{
    public class RoleControllerTest 
    {
        [Fact]
        public async Task Get_NoParams_ReturnAllRolesInJson()
        {
            //arrange
            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            var roleManagerMock = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);

            var controller = new RoleController(roleManagerMock.Object);


            //act
            var result = controller.Get();
            var contentResult = result as ObjectResult;

            //assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.NotEmpty(contentResult.Value.ToString());
        }

        [Fact]
        public async Task Get_Id_ReturnRoleInJson()
        {
            //arrange
            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            var roleManagerMock = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);
            roleManagerMock
                .Setup(c => c.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new IdentityRole());

            var controller = new RoleController(roleManagerMock.Object);


            //act
            var result = await controller.Get("LOL");
            var contentResult = result as ObjectResult;

            //assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.NotEmpty(contentResult.Value.ToString());
        }



        [Fact]
        public async Task Post_Id_ReturnCreated()
        {
            //arrange
            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            var roleManagerMock = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);
            roleManagerMock
                .Setup(c => c.CreateAsync(It.IsAny<IdentityRole>()))
                .ReturnsAsync(IdentityResult.Success);

            var controller = new RoleController(roleManagerMock.Object);


            //act
            var result = await controller.Create(new DTO.RoleDto());
            var contentResult = result as ObjectResult;

            //assert
            Assert.NotNull(result);
            Assert.IsType<CreatedResult>(result);
            Assert.NotEmpty(contentResult.Value.ToString());
        }
    }
}
