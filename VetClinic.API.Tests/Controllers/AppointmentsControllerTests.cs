using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VetClinic.API.Controllers;
using VetClinic.API.DTO.Appointments;
using VetClinic.BLL.Exceptions;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using Xunit;

namespace VetClinic.API.Tests.Controllers
{
    public class AppointmentsControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IAppointmentService> _appointmentService;
        private readonly Mock<IMapper> _autoMapper;
        private readonly AppointmentsController _appointmentsController;

        public AppointmentsControllerTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _appointmentService = _fixture.Freeze<Mock<IAppointmentService>>();
            _autoMapper = _fixture.Freeze<Mock<IMapper>>();
            _appointmentsController = _fixture.Build<AppointmentsController>().OmitAutoProperties().Create();
        }

        [Fact]
        public async Task GetAsync_GetAllAppointments_ReturnsAllAppointments()
        {
            // Arrange
            var appointments = _fixture.CreateMany<Appointment>(5).ToList();
            var appointmentsDto = _fixture.CreateMany<AppointmentDto>(5).ToList();

            _appointmentService
                .Setup(a => a.GetAllAppointmentsAsync(null, null))
                .ReturnsAsync(appointments);
            _autoMapper
                .Setup(m => m.Map<IEnumerable<AppointmentDto>>(appointments))
                .Returns(appointmentsDto);

            // Act
            var actual = await _appointmentsController.GetAsync(null, null);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actual);
            Assert.Equal(appointmentsDto, okObjectResult.Value);
        }


        [Fact]
        public async Task GetAsync_AppointmentExists_ReturnsAppointment()
        {
            // Arrange
            var appointment = _fixture.Create<Appointment>();
            var appointmentDto = _fixture.Create<AppointmentDto>();
            int id = appointment.Id;

            _appointmentService
                .Setup(a => a.GetAppointmentByIdAsync(id))
                .ReturnsAsync(appointment);
            _autoMapper
                .Setup(m => m.Map<AppointmentDto>(appointment))
                .Returns(appointmentDto);

            // Act
            var actual = await _appointmentsController.GetAsync(id);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actual);
            Assert.Equal(appointmentDto, okObjectResult.Value);
        }

        [Fact]
        public async Task GetAsync_AppointmentDoesntExist_ThrowsException()
        {
            // Arrange
            int id = -1;

            _appointmentService
                .Setup(a => a.GetAppointmentByIdAsync(id))
                .ThrowsAsync(new VetClinicException(HttpStatusCode.BadRequest, $"Appointment with {id} id doesn't exist"));

            // Act & Assert
            var ex = await Assert.ThrowsAsync<VetClinicException>(() => _appointmentsController.GetAsync(id));
            Assert.Equal($"Appointment with {id} id doesn't exist", ex.Message);
        }


        [Fact]
        public async Task PostAsync_AppointmentIsCreated_ReturnsAppointment()
        {
            // Arrange
            var appointment = _fixture.Create<Appointment>();
            var createdAppointment = _fixture.Create<Appointment>();
            var readAppointmentDto = _fixture.Create<AppointmentDto>();
            var createAppointmentDto = _fixture.Create<CreateAppointmentDto>();

            _autoMapper
                .Setup(m => m.Map<Appointment>(createAppointmentDto))
                .Returns(appointment);
            _appointmentService
                .Setup(a => a.CreateAppointmentAsync(appointment))
                .ReturnsAsync(createdAppointment);
            _autoMapper
                .Setup(m => m.Map<AppointmentDto>(createdAppointment))
                .Returns(readAppointmentDto);

            // Act
            var actual = await _appointmentsController.PostAsync(createAppointmentDto);

            // Assert
            _autoMapper.Verify(m => m.Map<Appointment>(createAppointmentDto), Times.Once);
            _appointmentService.Verify(a => a.CreateAppointmentAsync(appointment), Times.Once);
            _autoMapper.Verify(m => m.Map<AppointmentDto>(createdAppointment), Times.Once);
            Assert.IsType<CreatedAtRouteResult>(actual);
        }

        [Fact]
        public async Task PostAsync_AppointmentModelIsNotValid_ThrowsException()
        {
            // Arrange
            var appointment = _fixture.Create<Appointment>();
            var createAppointmentDto = _fixture.Create<CreateAppointmentDto>();

            _autoMapper
                .Setup(m => m.Map<Appointment>(createAppointmentDto))
                .Returns(appointment);
            _appointmentService.Setup(ap => ap.CreateAppointmentAsync(appointment))
                .ThrowsAsync(new VetClinicException(HttpStatusCode.BadRequest, "Model is invalid"));

            // Act & Assert
            var ex = await Assert.ThrowsAsync<VetClinicException>(() => _appointmentsController.PostAsync(createAppointmentDto));
            Assert.Equal($"Model is invalid", ex.Message);
        }


        [Fact]
        public async Task PutAsync_AppointmentIsUpdated_ReturnsNoContent()
        {
            // Arrange
            var updateAppointmentDto = _fixture.Create<UpdateAppointmentDto>();
            var appointment = _fixture.Create<Appointment>();
            int id = 10;

            _autoMapper
                .Setup(m => m.Map<Appointment>(updateAppointmentDto))
                .Returns(appointment);
            _appointmentService
                .Setup(a => a.UpdateAppointmentAsync(id, appointment))
                .ReturnsAsync(appointment);

            // Act
            var actual = await _appointmentsController.PutAsync(id, updateAppointmentDto);

            // Assert
            _autoMapper.Verify(m => m.Map<Appointment>(updateAppointmentDto), Times.Once);
            _appointmentService.Verify(a => a.UpdateAppointmentAsync(id, appointment), Times.Once);
            Assert.IsType<NoContentResult>(actual);
        }

        [Fact]
        public async Task PutAsync_AppointmentDoesntExist_ThrowsException()
        {
            // Arrange
            var updateAppointmentDto = _fixture.Create<UpdateAppointmentDto>();
            var appointment = _fixture.Create<Appointment>();
            int id = -1;

            _autoMapper
                .Setup(m => m.Map<Appointment>(updateAppointmentDto))
                .Returns(appointment);
            _appointmentService
                .Setup(a => a.UpdateAppointmentAsync(id, appointment))
                .ThrowsAsync(new VetClinicException(HttpStatusCode.BadRequest, $"Appointment with {id} id doesn't exist"));

            // Act & Assert
            var ex = await Assert.ThrowsAsync<VetClinicException>(
                () => _appointmentsController.PutAsync(id, updateAppointmentDto));
            Assert.Equal($"Appointment with {id} id doesn't exist", ex.Message);
        }

        [Fact]
        public async Task PutAsync_AppointmentModelIsNotValid_ThrowsException()
        {
            // Arrange
            var appointment = _fixture.Create<Appointment>();
            var updateAppointmentDto = _fixture.Create<UpdateAppointmentDto>();
            int id = 10;

            _autoMapper
                .Setup(m => m.Map<Appointment>(updateAppointmentDto))
                .Returns(appointment);
            _appointmentService.Setup(ap => ap.UpdateAppointmentAsync(id, appointment))
                .ThrowsAsync(new VetClinicException(HttpStatusCode.BadRequest, "Model is invalid"));

            // Act & Assert
            var ex = await Assert.ThrowsAsync<VetClinicException>(
                () => _appointmentsController.PutAsync(id, updateAppointmentDto));
            Assert.Equal($"Model is invalid", ex.Message);
        }


        [Fact]
        public async Task DeleteAsync_AppointmentIsDeleted_ReturnsNoContent()
        {
            // Arrange 
            var appointment = _fixture.Create<Appointment>();
            int id = 10;

            _appointmentService
                .Setup(a => a.DeleteAppointmentAsync(id))
                .ReturnsAsync(appointment);

            // Act
            var actual = await _appointmentsController.DeleteAsync(id);

            // Assert
            _appointmentService.Verify(a => a.DeleteAppointmentAsync(id), Times.Once);
            Assert.IsType<NoContentResult>(actual);
        }

        [Fact]
        public async Task DeleteAsync_AppointmentDoesntExist_ThrowsException()
        {
            // Arrange
            int id = -1;

            _appointmentService
                .Setup(a => a.DeleteAppointmentAsync(id))
                .ThrowsAsync(new VetClinicException(HttpStatusCode.BadRequest, $"Appointment with {id} id doesn't exist"));

            // Act & Assert
            var ex = await Assert.ThrowsAsync<VetClinicException>(() => _appointmentsController.DeleteAsync(id));
            Assert.Equal($"Appointment with {id} id doesn't exist", ex.Message);
        }
    }
}