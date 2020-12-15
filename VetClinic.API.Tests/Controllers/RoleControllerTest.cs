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
            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Name = "pucci"
                },
                new IdentityRole()
                {
                    Name = "gucci"
                }
            };
            //var rolesJson = JsonSerializer.Serialize(roles);
                
            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            var roleManagerMock = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);

            var controller = new RoleController(roleManagerMock.Object);


            //act
            var result = await controller.Get();
            var contentResult = result as ObjectResult;
            var content = JsonSerializer.Deserialize<List<IdentityRole>>(contentResult.Value.ToString());

            //assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
           // Assert.Equal(roles, contentResult.Value);
            content.Should().Contain(content => content.Name == "admn");
        }
    }
}
