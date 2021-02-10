using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VetClinic.API.Tests;
using VetClinic.BLL.Domain;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.BLL.Services.Realizations;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;
using Xunit;

namespace VetClinic.BLL.Tests.Services
{
    public class DoctorServiceTest
    {
        [Theory, AutoMoqData]
        public async Task GetAll_EqualCount(List<Doctor> doctors,
            [Frozen] Mock<IUserService> _userServiceMock,
            [Frozen] Mock<IRepositoryWrapper> _repositoryMock,
            [Frozen] Mock<RoleManager<IdentityRole>> _roleManagerMock)
        {
            // Arrange
            _repositoryMock.Setup(x => x.DoctorRepository
             .GetAsync(null, It.IsAny<Func<IQueryable<Doctor>, IIncludableQueryable<Doctor, object>>>(), null, null, null, false))
               .ReturnsAsync(doctors);
            var _doctorService = new DoctorService(_repositoryMock.Object, _userServiceMock.Object, _roleManagerMock.Object);

            // Act
            var actual = await _doctorService.GetDoctorAsync();

            // Assert
            Assert.Equal(doctors.Count, actual.Count);
            _repositoryMock.Verify(m => m.DoctorRepository
                .GetAsync(null,
                It.IsAny<Func<IQueryable<Doctor>, IIncludableQueryable<Doctor, object>>>(),
                null, null, null, false),
                Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task GetById_DoctorId_ReturnDoctorWithRequestedId(Doctor doctor,
            [Frozen] Mock<IUserService> _userServiceMock,
            [Frozen] Mock<IRepositoryWrapper> _repositoryMock,
            [Frozen] Mock<RoleManager<IdentityRole>> _roleManagerMock)
        {
            // Arrange
            int id = doctor.Id;
            _repositoryMock.Setup(x => x.DoctorRepository
                .GetFirstOrDefaultAsync(c => c.Id == id,
                It.IsAny<Func<IQueryable<Doctor>, IIncludableQueryable<Doctor,
                object>>>(), false))
               .ReturnsAsync(doctor);
            var _doctorService = new DoctorService(_repositoryMock.Object, _userServiceMock.Object, _roleManagerMock.Object);

            // Act
            var actual = await _doctorService.GetDoctorByIdAsync(id);

            // Assert
            Assert.Equal(doctor.Id, actual.Id);
            _repositoryMock.Verify(m => m.DoctorRepository
                .GetFirstOrDefaultAsync(c => c.Id == id,
                    It.IsAny<Func<IQueryable<Doctor>, IIncludableQueryable<Doctor, object>>>(),
                    false),
                    Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task GetById_DoctorId_ReturnNull(Doctor doctor,
            [Frozen] Mock<IUserService> _userServiceMock,
            [Frozen] Mock<IRepositoryWrapper> _repositoryMock,
            [Frozen] Mock<RoleManager<IdentityRole>> _roleManagerMock)
        {
            // Arrange
            int id = doctor.Id;
            _repositoryMock.Setup(x => x.DoctorRepository
                .GetFirstOrDefaultAsync(c => c.Id == id,
                It.IsAny<Func<IQueryable<Doctor>, IIncludableQueryable<Doctor,
                object>>>(), false))
               .ReturnsAsync(null as Doctor);
            var _doctorService = new DoctorService(_repositoryMock.Object, _userServiceMock.Object, _roleManagerMock.Object);

            // Act
            var actual = await _doctorService.GetDoctorByIdAsync(id);

            // Assert
            Assert.Null(actual);
            _repositoryMock.Verify(m => m.DoctorRepository
                .GetFirstOrDefaultAsync(c => c.Id == id,
                    It.IsAny<Func<IQueryable<Doctor>, IIncludableQueryable<Doctor, object>>>(),
                    false),
                    Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task Add_Doctor_ReturnAdedDoctor(Doctor doctor,
            User user,
            [Frozen] Mock<IUserService> _userServiceMock,
            [Frozen] Mock<IRepositoryWrapper> _repositoryMock,
            [Frozen] Mock<RoleManager<IdentityRole>> _roleManagerMock,
            [Frozen] IdentityRole role)
        {
            // Arrange
            _repositoryMock.Setup(x => x.DoctorRepository
                .Add(doctor));
            _userServiceMock.Setup(x => x.CreateUserAsync(user, role))
                .ReturnsAsync((true, doctor.UserId));
            _repositoryMock.Setup(d => d.DoctorRepository.GetFirstOrDefaultAsync(d => d.Id == doctor.Id,
                It.IsAny<Func<IQueryable<Doctor>, IIncludableQueryable<Doctor,
                object>>>(), false))
                .ReturnsAsync(doctor);

            var _doctorService = new DoctorService(_repositoryMock.Object, _userServiceMock.Object, _roleManagerMock.Object);

            // Act    
            var actual = await _doctorService.AddDoctorAsync(doctor, user);

            // Assert            
            Assert.Equal(doctor, actual);
        }


        [Theory, AutoMoqData]
        public async Task RemoveDoctor_DoctorId_ReturnTrue(Doctor doctor,
            [Frozen] Mock<IUserService> _userServiceMock,
            [Frozen] Mock<IRepositoryWrapper> _repositoryMock,
            [Frozen] Mock<RoleManager<IdentityRole>> _roleManagerMock)
        {
            // Arrange
            int doctorid = doctor.Id;
            string userId = doctor.UserId;

            _repositoryMock.Setup(x => x.DoctorRepository
            .GetFirstOrDefaultAsync(p => p.Id == doctorid, null, false))
                .ReturnsAsync(doctor);
            _userServiceMock.Setup(x => x.DeleteUserAsync(userId))
                .ReturnsAsync(true);

            var _doctorService = new DoctorService(_repositoryMock.Object, _userServiceMock.Object, _roleManagerMock.Object);

            // Act    
            var actual = await _doctorService.RemoveDoctorAsync(doctorid);

            // Assert            
            Assert.True(actual);
            _repositoryMock.Verify(m => m.DoctorRepository.Remove(doctor), Times.Once);
            _userServiceMock.Verify(m => m.DeleteUserAsync(userId), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task RemoveDoctor_DoctorId_ReturnFalce(Doctor doctor,
            [Frozen] Mock<IUserService> _userServiceMock,
            [Frozen] Mock<IRepositoryWrapper> _repositoryMock,
            [Frozen] Mock<RoleManager<IdentityRole>> _roleManagerMock)
        {
            // Arrange
            int doctorid = doctor.Id;
            string userId = doctor.UserId;

            _repositoryMock.Setup(x => x.DoctorRepository
            .GetFirstOrDefaultAsync(p => p.Id == doctorid, null, false))
                .ReturnsAsync(null as Doctor);
            _userServiceMock.Setup(x => x.DeleteUserAsync(userId))
                .ReturnsAsync(false);

            var _doctorService = new DoctorService(_repositoryMock.Object, _userServiceMock.Object, _roleManagerMock.Object);

            // Act    
            var actual = await _doctorService.RemoveDoctorAsync(doctorid);

            // Assert            
            Assert.False(actual);
            _repositoryMock.Verify(m => m.DoctorRepository.Remove(doctor), Times.Never);
            _userServiceMock.Verify(m => m.DeleteUserAsync(userId), Times.Never);
        }


        [Theory, AutoMoqData]
        public async Task Update_DoctorUserDoctorId_ReturnFalce(
            IdentityRole role,
            [Frozen] Mock<IUserService> _userServiceMock,
            [Frozen] Mock<IRepositoryWrapper> _repositoryMock,
            [Frozen] Mock<RoleManager<IdentityRole>> _roleManagerMock)
        {
            // Arrange
            Doctor doctor = null;
            User user = null;

            var _doctorService = new DoctorService(_repositoryMock.Object, _userServiceMock.Object, _roleManagerMock.Object);

            // Act    
            var actual = await _doctorService.UpdateDoctorAsync(doctor, user, It.IsAny<int>());

            // Assert            
            Assert.False(actual);
            _repositoryMock.Verify(m => m.DoctorRepository.Update(doctor), Times.Never);           
        }


        [Theory, AutoMoqData]
        public async Task Update_InputDoctorIsNull_ReturnFalce(Doctor doctor,
            User user,
            [Frozen] Mock<IUserService> _userServiceMock,
            [Frozen] Mock<IRepositoryWrapper> _repositoryMock,
            [Frozen] Mock<RoleManager<IdentityRole>> _roleManagerMock)
        {
            // Arrange
            int doctorid = doctor.Id;
            Doctor inputDoctor = null;
            _repositoryMock.Setup(x => x.DoctorRepository
            .GetFirstOrDefaultAsync(
                p => p.Id == doctorid,
                It.IsAny<Func<IQueryable<Doctor>, IIncludableQueryable<Doctor, object>>>(),
                false))
                .ReturnsAsync(doctor);


            var _doctorService = new DoctorService(_repositoryMock.Object, _userServiceMock.Object, _roleManagerMock.Object);

            // Act    
            var actual = await _doctorService.UpdateDoctorAsync(inputDoctor, user, doctorid);

            // Assert            
            Assert.False(actual);
        }


        [Theory, AutoMoqData]
        public async Task Update_InputDoctorUserDocotrId_ReturnTrue(Doctor doctor,
            User user,
            IdentityRole role,
            [Frozen] Mock<IUserService> _userServiceMock,
            [Frozen] Mock<IRepositoryWrapper> _repositoryMock,
            [Frozen] Mock<RoleManager<IdentityRole>> _roleManagerMock)
        {
            // Arrange
            int doctorid = doctor.Id;

            _repositoryMock.Setup(x => x.DoctorRepository
            .GetFirstOrDefaultAsync(
                p => p.Id == doctorid,
                It.IsAny<Func<IQueryable<Doctor>, IIncludableQueryable<Doctor, object>>>(),
                false))
                .ReturnsAsync(doctor);

            _repositoryMock.Setup(c => c.DoctorRepository.Update(doctor));
            _userServiceMock.Setup(c => c.UpdateUserAsync(doctor.UserId, doctor.User, role))
                .ReturnsAsync(true);

            var _doctorService = new DoctorService(_repositoryMock.Object, _userServiceMock.Object, _roleManagerMock.Object);

            // Act    
            var actual = await _doctorService.UpdateDoctorAsync(doctor, user, doctorid);

            // Assert            
            Assert.True(actual);
            _repositoryMock.Verify(c => c.DoctorRepository.Update(doctor), Times.Once);
            _repositoryMock.Verify(c => c.SaveAsync(), Times.Once);
        }

        [Theory, AutoMoqData]
        public async Task GetTotalCount_EqualCount([Frozen] List<Doctor> doctors,
            DoctorsFilter filter,
            [Frozen] Mock<IUserService> _userServiceMock,
            [Frozen] Mock<IRepositoryWrapper> _repositoryMock,
            [Frozen] Mock<RoleManager<IdentityRole>> _roleManagerMock)
        {
            // Arrange
            _repositoryMock.Setup(x => x.DoctorRepository
             .CountAsync(It.IsAny<Expression<Func<Doctor, bool>>>()))
                .ReturnsAsync(doctors.Count);
            var _doctorService = new DoctorService(_repositoryMock.Object, _userServiceMock.Object, _roleManagerMock.Object);

            // Act
            var actual = await _doctorService.GetTotalCount(filter);

            // Assert
            Assert.Equal(doctors.Count, actual);
            _repositoryMock.Verify(m => m.DoctorRepository
                .CountAsync(It.IsAny<Expression<Func<Doctor, bool>>>()),
                Times.Once);
        }

        [Theory, AutoMoqData]
        public async Task IsAnyDoctorExist_EqualTrue(           
            [Frozen] Mock<IUserService> _userServiceMock,
            [Frozen] Mock<IRepositoryWrapper> _repositoryMock,
            [Frozen] Mock<RoleManager<IdentityRole>> _roleManagerMock)
        {
            // Arrange
            _repositoryMock.Setup(x => x.DoctorRepository
             .IsAnyAsync(It.IsAny<Expression<Func<Doctor, bool>>>()))
                .ReturnsAsync(true);
            var _doctorService = new DoctorService(_repositoryMock.Object, _userServiceMock.Object, _roleManagerMock.Object);

            // Act
            var actual = await _doctorService.IsAnyDoctorAsync(It.IsAny<int>());

            // Assert
            Assert.True(actual);
            _repositoryMock.Verify(m => m.DoctorRepository
                .IsAnyAsync(It.IsAny<Expression<Func<Doctor, bool>>>()),
                Times.Once);
        }

        [Theory, AutoMoqData]
        public async Task IsAnyDoctorExist_EqualFalse(
            [Frozen] Mock<IUserService> _userServiceMock,
            [Frozen] Mock<IRepositoryWrapper> _repositoryMock,
            [Frozen] Mock<RoleManager<IdentityRole>> _roleManagerMock)
        {
            // Arrange
            _repositoryMock.Setup(x => x.DoctorRepository
             .IsAnyAsync(It.IsAny<Expression<Func<Doctor, bool>>>()))
                .ReturnsAsync(false);
            var _doctorService = new DoctorService(_repositoryMock.Object, _userServiceMock.Object, _roleManagerMock.Object);

            // Act
            var actual = await _doctorService.IsAnyDoctorAsync(It.IsAny<int>());

            // Assert
            Assert.False(actual);
            _repositoryMock.Verify(m => m.DoctorRepository
                .IsAnyAsync(It.IsAny<Expression<Func<Doctor, bool>>>()),
                Times.Once);
        }
    }
}
