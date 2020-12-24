using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VetClinic.BLL.Exceptions;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.BLL.Services.Realizations;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;
using Xunit;

namespace VetClinic.BLL.Tests.Services
{
    public class AppointmentServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IRepositoryWrapper> _repositoryWrapper;
        private readonly IAppointmentService _appointmentService;

        public AppointmentServiceTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _repositoryWrapper = _fixture.Freeze<Mock<IRepositoryWrapper>>();
            _appointmentService = _fixture.Create<AppointmentService>();
        }

        [Fact]
        public async Task GetAllAppointmentsAsync_GetAppointments_ReturnsAllAppointments()
        {
            // Arrange
            var appointments = _fixture.CreateMany<Appointment>().ToList();
            
            _repositoryWrapper
                .Setup(rw => rw.AppointmentRepository.GetAsync(It.IsAny<Expression<Func<Appointment, bool>>>(),
                    It.IsAny<Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>>>(),
                    It.IsAny<Func<IQueryable<Appointment>, IOrderedQueryable<Appointment>>>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(appointments);

            // Act 
            var actual = await _appointmentService.GetAllAppointmentsAsync();

            // Assert
            Assert.Equal(appointments, actual);
        }

        [Fact]
        public async Task GetAppointmentByIdAsync_AppointmentExists_ReturnsAppointment()
        {
            // Arrange
            var appointment = _fixture.Create<Appointment>();
            int id = appointment.Id;

            _repositoryWrapper
                .Setup(rw => rw.AppointmentRepository.GetFirstOrDefaultAsync(
                    a => a.Id == id,
                    It.IsAny<Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>>>(),
                    It.IsAny<bool>()
                ))
                .ReturnsAsync(() => appointment);

            // Act
            var actual = await _appointmentService.GetAppointmentByIdAsync(id);

            // Assert
            Assert.Equal(appointment, actual);
        }

        [Fact]
        public async Task GetAppointmentByIdAsync_AppointmentDoesntExist_ThrowsException()
        {
            // Arrange
            int id = -1;
            
            _repositoryWrapper
                .Setup(rw => rw.AppointmentRepository.GetFirstOrDefaultAsync(
                    a => a.Id == id,
                    It.IsAny<Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>>>(),
                    It.IsAny<bool>()
                ))
                .ReturnsAsync(() => null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<VetClinicException>(() => _appointmentService.GetAppointmentByIdAsync(id));
            Assert.Equal($"Appointment with {id} id doesn't exist", ex.Message);
        }


        [Fact]
        public async Task CreateAppointmentAsync_CreateAppointment_ReturnsCreatedAppointment()
        {
            // Arrange
            var appointment = _fixture.Create<Appointment>();
            int id = appointment.Id;

            _repositoryWrapper
                .Setup(rw => rw.AnimalRepository.IsAnyAsync(a => a.Id == appointment.AnimalId))
                .ReturnsAsync(true);
            _repositoryWrapper
                .Setup(rw => rw.ServiceRepository.IsAnyAsync(s => s.Id == appointment.ServiceId))
                .ReturnsAsync(true);
            _repositoryWrapper
                .Setup(rw => rw.AppointmentRepository.GetFirstOrDefaultAsync(
                    a => a.Id == id,
                    It.IsAny<Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>>>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(appointment);

            // Act
            var actual = await _appointmentService.CreateAppointmentAsync(appointment);

            // Assert
            _repositoryWrapper.Verify(rw => rw.AppointmentRepository.Add(appointment), Times.Once);
            _repositoryWrapper.Verify(rw => rw.SaveAsync(), Times.Once);

            Assert.Equal(appointment, actual);
        }

        [Fact]
        public async Task CreateAppointmentAsync_AppointmentModelIsInvalid_ThrowsException()
        {
            // Arrange
            var appointment = _fixture.Create<Appointment>();
            int id = appointment.Id;

            _repositoryWrapper
                .Setup(rw => rw.AnimalRepository.IsAnyAsync(a => a.Id == appointment.AnimalId))
                .ReturnsAsync(false);
            _repositoryWrapper
                .Setup(rw => rw.ServiceRepository.IsAnyAsync(s => s.Id == appointment.ServiceId))
                .ReturnsAsync(false);
            _repositoryWrapper
                .Setup(rw => rw.AppointmentRepository.GetFirstOrDefaultAsync(
                    a => a.Id == id,
                    It.IsAny<Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>>>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(appointment);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<VetClinicException>(() => _appointmentService.CreateAppointmentAsync(appointment));
            Assert.Equal($"Model is invalid", ex.Message);
        }


        [Fact]
        public async Task UpdateAppointmentAsync_AppointmentExists_ReturnsUpdatedAppointment()
        {
            // Arrange
            var appointment = _fixture.Create<Appointment>();
            int id = appointment.Id;

            _repositoryWrapper
                .Setup(rw => rw.AppointmentRepository.GetFirstOrDefaultAsync(
                    a => a.Id == id,
                    It.IsAny<Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>>>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(appointment);
            _repositoryWrapper
                .Setup(rw => rw.AnimalRepository.IsAnyAsync(a => a.Id == appointment.AnimalId))
                .ReturnsAsync(true);
            _repositoryWrapper
                .Setup(rw => rw.ServiceRepository.IsAnyAsync(s => s.Id == appointment.ServiceId))
                .ReturnsAsync(true);
            _repositoryWrapper
                .Setup(rw => rw.StatusRepository.IsAnyAsync(s => s.Id == appointment.StatusId))
                .ReturnsAsync(true);
            _repositoryWrapper
                .Setup(rw => rw.DoctorRepository.IsAnyAsync(d => d.Id == appointment.DoctorId))
                .ReturnsAsync(true);
            
            // Act 
            var actual = await _appointmentService.UpdateAppointmentAsync(id, appointment);

            // Assert
            _repositoryWrapper.Verify(rw => rw.AppointmentRepository.Update(appointment), Times.Once);
            _repositoryWrapper.Verify(rw => rw.SaveAsync(), Times.Once);
            Assert.Equal(appointment, actual);
        }

        [Fact]
        public async Task UpdateAppointmentAsync_AppointmentDoesntExist_ThrowsException()
        {
            // Arrange
            var appointment = _fixture.Create<Appointment>();
            int id = -1;

            _repositoryWrapper.Setup(rw => rw.AppointmentRepository.GetFirstOrDefaultAsync(
                a => a.Id == id,
                It.IsAny<Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>>>(),
                It.IsAny<bool>())).ReturnsAsync(() => null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<VetClinicException>(() =>
                _appointmentService.UpdateAppointmentAsync(id, appointment));
            Assert.Equal($"Appointment with {id} id doesn't exist", ex.Message);
        }

        [Fact]
        public async Task UpdateAppointmentAsync_AppointmentModelIsInvalid_ThrowsException()
        {
            // Arrange
            var appointment = _fixture.Create<Appointment>();
            int id = appointment.Id;

            _repositoryWrapper.Setup(rw => rw.AnimalRepository.IsAnyAsync(a => a.Id == appointment.AnimalId))
                .ReturnsAsync(false);
            _repositoryWrapper.Setup(rw => rw.ServiceRepository.IsAnyAsync(s => s.Id == appointment.ServiceId))
                .ReturnsAsync(false);
            _repositoryWrapper.Setup(rw => rw.StatusRepository.IsAnyAsync(s => s.Id == appointment.StatusId))
                .ReturnsAsync(false);
            _repositoryWrapper.Setup(rw => rw.DoctorRepository.IsAnyAsync(d => d.Id == appointment.DoctorId))
                .ReturnsAsync(false);
            _repositoryWrapper.Setup(rw => rw.AppointmentRepository.GetFirstOrDefaultAsync(
                    a => a.Id == id,
                    It.IsAny<Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>>>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(appointment);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<VetClinicException>(() =>
                _appointmentService.UpdateAppointmentAsync(id, appointment));
            Assert.Equal($"Model is invalid", ex.Message);
        }


        [Fact]
        public async Task DeleteAppointmentAsync_AppointmentExists_ReturnsDeletedAppointment()
        {
            // Arrange
            var appointment = _fixture.Create<Appointment>();
            int id = appointment.Id;

            _repositoryWrapper.Setup(rw => rw.AppointmentRepository.GetFirstOrDefaultAsync(
                a => a.Id == id,
                It.IsAny<Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>>>(),
                It.IsAny<bool>())).ReturnsAsync(appointment);

            // Act
            var actual = await _appointmentService.DeleteAppointmentAsync(id);

            // Assert
            _repositoryWrapper.Verify(rw => rw.AppointmentRepository.GetFirstOrDefaultAsync(
                    a => a.Id == id,
                    It.IsAny<Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>>>(),
                    It.IsAny<bool>()),
                Times.Once);
            _repositoryWrapper.Verify(rw => rw.AppointmentRepository.Remove(appointment), Times.Once);
            _repositoryWrapper.Verify(rw => rw.SaveAsync(), Times.Once);
            Assert.Equal(appointment, actual);
        }

        [Fact]
        public async Task DeleteAppointmentAsync_AppointmentDoesntExist_ThrowsException()
        {
            // Arrange
            var appointment = _fixture.Create<Appointment>();
            int id = -1;

            _repositoryWrapper.Setup(rw => rw.AppointmentRepository.GetFirstOrDefaultAsync(
                a => a.Id == id,
                It.IsAny<Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>>>(),
                It.IsAny<bool>())).ReturnsAsync(() => null);

            // Act & Assert
            _repositoryWrapper.Verify(rw => rw.AppointmentRepository.Remove(appointment), Times.Never);
            _repositoryWrapper.Verify(rw => rw.SaveAsync(), Times.Never);
            var ex = await Assert.ThrowsAsync<VetClinicException>(() => _appointmentService.DeleteAppointmentAsync(id));
            Assert.Equal($"Appointment with {id} id doesn't exist", ex.Message);
        }
    }
}