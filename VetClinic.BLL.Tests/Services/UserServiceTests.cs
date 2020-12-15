using AutoFixture;
using AutoFixture.AutoMoq;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Threading.Tasks;
using VetClinic.BLL.Services.Realizations;
using VetClinic.DAL.Entities;
using Xunit;

namespace VetClinic.BLL.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task CreateUser_UserExsists_ReturnFalse()
        {
            //arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var user = new User() { };
            var roles = fixture.CreateMany<IdentityRole>();
            
            var store = new Mock<IUserStore<User>>();
            var mgr = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Setup(c => c.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new User());

            var userService = new UserService(mgr.Object, fixture.Create<RoleManager<IdentityRole>>(), fixture.Create<IValidator<User>>());
            
            //act
            var (result, id) = await userService.CreateUser(user, roles);

            //assert
            Assert.False(result);
            Assert.Equal(string.Empty, id);
        }

        [Fact]
        public async Task CreateUser_UserNotValid_ReturnFalse()
        {
            //arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var user = new User() { };
            var roles = fixture.CreateMany<IdentityRole>();

            var store = new Mock<IUserStore<User>>();
            var mgr = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Setup(c => c.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var validatorMock = new Mock<IValidator<User>>();
            validatorMock.Setup(c => c.Validate(It.IsAny<User>()))
                .Returns(new ValidationResult(fixture.CreateMany<ValidationFailure>()));

            var userService = new UserService(mgr.Object, fixture.Create<RoleManager<IdentityRole>>(), validatorMock.Object);

            //act
            var (result, id) = await userService.CreateUser(user, roles);

           
            //assert
            Assert.False(result);
            Assert.Equal(string.Empty, id);
        }

        [Fact]
        public async Task CreateUser_CreateFailure_ReturnFalse()
        {
            //arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var user = new User() { };
            var roles = fixture.CreateMany<IdentityRole>();

            var store = new Mock<IUserStore<User>>();
            var mgr = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            mgr.Setup(c => c.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            mgr.Setup(c => c.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            var validatorMock = new Mock<IValidator<User>>();
            validatorMock.Setup(c => c.Validate(It.IsAny<User>()))
                .Returns(new ValidationResult());
            
            var userService = new UserService(mgr.Object, fixture.Create<RoleManager<IdentityRole>>(), validatorMock.Object);


            //act
            var (result, id) = await userService.CreateUser(user, roles);

         
            //assert
            Assert.False(result);
            Assert.Equal(string.Empty, id);
        }

        [Fact]
        public async Task CreateUser_CreateSuccess_ReturnTrue()
        {
            //arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var user = new User() { };
            var roles = fixture.CreateMany<IdentityRole>();

            var store = new Mock<IUserStore<User>>();
            var mgr = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            mgr.SetupSequence(c => c.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null)
                .ReturnsAsync(new User() { Id = "test"});


            mgr.Setup(c => c.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            var roleMock = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);

            var validatorMock = new Mock<IValidator<User>>();
            validatorMock.Setup(c => c.Validate(It.IsAny<User>()))
                .Returns(new ValidationResult());

            var userService = new UserService(mgr.Object, roleMock.Object, validatorMock.Object);

            //act
            var (result, id) = await userService.CreateUser(user, roles);

            //assert
            Assert.True(result);
            Assert.NotEqual(string.Empty, id);
        }



        [Fact]
        public async Task UpdateUser_UserNotExsists_ReturnFalse()
        {
            //arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var user = new User() { };
            var roles = fixture.CreateMany<IdentityRole>();

            var store = new Mock<IUserStore<User>>();
            var mgr = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Setup(c => c.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var userService = new UserService(mgr.Object, fixture.Create<RoleManager<IdentityRole>>(), fixture.Create<IValidator<User>>());

            //act
            var result = await userService.UpdateUser(user, roles);

            //assert
            Assert.False(result);
        }


        [Fact]
        public async Task UpdateUser_UserNotValid_ReturnFalse()
        {
            //arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var user = new User() { };
            var roles = fixture.CreateMany<IdentityRole>();

            var store = new Mock<IUserStore<User>>();
            var mgr = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Setup(c => c.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new User());

            var validatorMock = new Mock<IValidator<User>>();
            validatorMock.Setup(c => c.Validate(It.IsAny<User>()))
                .Returns(new ValidationResult(fixture.CreateMany<ValidationFailure>()));

            var userService = new UserService(mgr.Object, fixture.Create<RoleManager<IdentityRole>>(), validatorMock.Object);

            //act
            var result = await userService.UpdateUser(user, roles);

            //assert
            Assert.False(result);
        }
    }
}
