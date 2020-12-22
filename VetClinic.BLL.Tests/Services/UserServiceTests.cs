using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Threading.Tasks;
using VetClinic.API.Tests;
using VetClinic.BLL.Services.Realizations;
using VetClinic.DAL.Entities;
using Xunit;

namespace VetClinic.BLL.Tests.Services
{
    public class UserServiceTests
    {
        [Theory, AutoMoqData]
        public async Task CreateUser_UserExsists_ReturnFalse(User user,
            IdentityRole[] roles,
            RoleManager<IdentityRole> roleManager,
            [Frozen] Mock<UserManager<User>> userManager)
        {
            //arrange
            userManager.Setup(c => c.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            var sut = new UserService(userManager.Object, roleManager);

            //act
            var (result, id) = await sut.CreateUserAsync(user, roles);

            //assert
            Assert.False(result);
            Assert.Equal(string.Empty, id);
        }
        
        [Theory, AutoMoqData]
        public async Task CreateUser_CreateFailure_ReturnFalse(User user,
            IdentityRole[] roles,
            RoleManager<IdentityRole> roleManager,
            [Frozen] Mock<UserManager<User>> userManager)
        {
            //arrange
            userManager.Setup(c => c.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            userManager.Setup(c => c.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            var sut = new UserService(userManager.Object, roleManager);

            //act
            var (result, id) = await sut.CreateUserAsync(user, roles);

            //assert
            Assert.False(result);
            Assert.Equal(string.Empty, id);
        }

        [Theory, AutoMoqData]
        public async Task CreateUser_CreateSuccess_ReturnTrue(User user,
            IdentityRole roles,
            [Frozen] Mock<RoleManager<IdentityRole>> roleManager,
            [Frozen] Mock<UserManager<User>> userManager)
        {
            //arrange
            userManager.SetupSequence(c => c.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null)
                .ReturnsAsync(user);

            userManager.Setup(c => c.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

            userManager.Setup(c => c.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            roleManager.Setup(c => c.RoleExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var sut = new UserService(userManager.Object, roleManager.Object);

            //act
            var (result, id) = await sut.CreateUserAsync(user, roles);

            //assert
            Assert.True(result);
            Assert.NotEqual(string.Empty, id);
        }

        [Theory, AutoMoqData]
        public async Task UpdateUser_UserNotExsists_ReturnFalse(User user,
            IdentityRole[] roles,
            RoleManager<IdentityRole> roleManager,
            [Frozen] Mock<UserManager<User>> userManager)
        {
            //arrange
            userManager.Setup(c => c.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var sut = new UserService(userManager.Object, roleManager);

            //act
            var result = await sut.UpdateUserAsync("42", user, roles);

            //assert
            Assert.False(result);
        }

        [Theory, AutoMoqData]
        public async Task DeleteUser_Success_ReturnTrue(User user,
            RoleManager<IdentityRole> roleManager,
            [Frozen] Mock<UserManager<User>> userManager)
        {
            //arrange
            userManager.Setup(c => c.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            userManager.Setup(c => c.DeleteAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Success);

            var sut = new UserService(userManager.Object, roleManager);

            //act
            var result = await sut.DeleteUserAsync("42");

            //assert
            Assert.True(result);
        }

        [Theory, AutoMoqData]
        public async Task DeleteUser_Fail_ReturnFalse(User user,
           RoleManager<IdentityRole> roleManager,
           [Frozen] Mock<UserManager<User>> userManager)
        {
            //arrange
            userManager.Setup(c => c.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            userManager.Setup(c => c.DeleteAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Failed());

            var sut = new UserService(userManager.Object, roleManager);

            //act
            var result = await sut.DeleteUserAsync("42");

            //assert
            Assert.False(result);
        }
        
    }
}
