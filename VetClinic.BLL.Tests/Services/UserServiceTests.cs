using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.BLL.Services.Realizations;
using VetClinic.DAL.Entities;
using Xunit;

namespace VetClinic.BLL.Tests.Services
{
    public class UserServiceTests
    {
        public User AppUser { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
        public IFixture Fixture { get; set; }
        public Mock<IUserStore<User>> UserStore { get; set; }

        public UserServiceTests()
        {
            Fixture = new Fixture().Customize(new AutoMoqCustomization());
            AppUser = new User() { };
            Roles = Fixture.CreateMany<IdentityRole>();
            UserStore = new Mock<IUserStore<User>>();
        }

        [Fact]
        public async Task CreateUser_UserExsists_ReturnFalse()
        {
            //arrange
            var mgr = new Mock<UserManager<User>>(UserStore.Object, null, null, null, null, null, null, null, null);
            mgr.Setup(c => c.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new User());

            var userService = new UserService(mgr.Object, Fixture.Create<RoleManager<IdentityRole>>());
            
            //act
            var (result, id) = await userService.CreateUser(AppUser, Roles);

            //assert
            Assert.False(result);
            Assert.Equal(string.Empty, id);
        }
       
        [Fact]
        public async Task CreateUser_CreateFailure_ReturnFalse()
        {
            //arrange
            var mgr = new Mock<UserManager<User>>(UserStore.Object, null, null, null, null, null, null, null, null);

            mgr.Setup(c => c.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            mgr.Setup(c => c.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            var userService = new UserService(mgr.Object, Fixture.Create<RoleManager<IdentityRole>>());

            //act
            var (result, id) = await userService.CreateUser(AppUser, Roles);

            //assert
            Assert.False(result);
            Assert.Equal(string.Empty, id);
        }

        [Fact]
        public async Task CreateUser_CreateSuccess_ReturnTrue()
        {
            //arrange
            var mgr = new Mock<UserManager<User>>(UserStore.Object, null, null, null, null, null, null, null, null);

            mgr.SetupSequence(c => c.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null)
                .ReturnsAsync(new User());

            mgr.Setup(c => c.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            var roleMock = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);

            var userService = new UserService(mgr.Object, roleMock.Object);

            //act
            var (result, id) = await userService.CreateUser(AppUser, Roles);

            //assert
            Assert.True(result);
            Assert.NotEqual(string.Empty, id);
        }

        [Fact]
        public async Task UpdateUser_UserNotExsists_ReturnFalse()
        {
            //arrange
            var mgr = new Mock<UserManager<User>>(UserStore.Object, null, null, null, null, null, null, null, null);
            mgr.Setup(c => c.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var userService = new UserService(mgr.Object, Fixture.Create<RoleManager<IdentityRole>>());

            //act
            var result = await userService.UpdateUser("2",AppUser, Roles);

            //assert
            Assert.False(result);
        }
    }
}
