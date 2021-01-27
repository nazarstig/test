using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.API.Controllers;
using VetClinic.API.DTO.Doctor;
using VetClinic.API.DTO.Queries;
using VetClinic.API.DTO.Responses;
using VetClinic.BLL.Domain;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using Xunit;

namespace VetClinic.API.Tests.Controllers
{
    public class DoctorControllerTest
    {
        private readonly Mock<IDoctorService> doctorServiceMock;
        private readonly Mock<IMapper> mapper;
        private readonly DoctorController doctorController;
        public DoctorControllerTest()
        {
            var fixture = new Fixture();
            doctorServiceMock = fixture.Freeze<Mock<IDoctorService>>();
            mapper = fixture.Freeze<Mock<IMapper>>();
            doctorController = new DoctorController(doctorServiceMock.Object, mapper.Object);
        }


        [Theory, AutoMoqData]
        public async Task GetAll_Doctors_ReturnsTrue(
            [Frozen] List<Doctor> doctor,
            [Frozen] List<ReadDoctorDto> doctorDto,
            [Frozen] PaginationQuery paginationQuery,
            [Frozen] PaginationFilter paginationFilter,
            [Frozen] DoctorsFiltrationQuery query,
            [Frozen] DoctorsFilter filter
            )
        {
            // Arrange
            doctorServiceMock.Setup(m => m.GetDoctorAsync(filter, paginationFilter))
                .ReturnsAsync(doctor);
            mapper.Setup(m => m.Map<ICollection<ReadDoctorDto>>(doctor))
                .Returns(doctorDto);
            mapper.Setup(m => m.Map<PaginationFilter>(paginationQuery))
                .Returns(paginationFilter);
            mapper.Setup(m => m.Map<DoctorsFilter>(query))
               .Returns(filter);

            // Act
            var actualResult = await doctorController.GetAsync(query, paginationQuery);

            // Assert      
            var result = actualResult as OkObjectResult;            
            
            Assert.True(actualResult is OkObjectResult);
            doctorServiceMock.Verify(m => m.GetDoctorAsync(filter, paginationFilter), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task GetById_DoctorId_ReturnsDoctorWithRequestedId(
           [Frozen] Doctor doctor,
           [Frozen] ReadDoctorDto doctorDto)
        {
            // Arrange           
            doctorServiceMock.Setup(p => p.GetDoctorByIdAsync(doctor.Id))
                .ReturnsAsync(doctor);
            mapper.Setup(m => m.Map<ReadDoctorDto>(doctor))
                .Returns(doctorDto);

            // Act
            var actualResult = await doctorController.GetAsync(doctor.Id);

            // Assert    
            var result = actualResult as OkObjectResult;
            var resultData = result.Value as Response<ReadDoctorDto>;

            Assert.Equal(doctorDto, resultData.Data);
            Assert.True(actualResult is OkObjectResult);
            doctorServiceMock.Verify(m => m.GetDoctorByIdAsync(doctor.Id), Times.Once);
        }


        [Fact]
        public async Task GetById_DoctorId_ReturnsNotFound_withNewMock()
        {
            // Arrange     
            var doctorServiceMock1 = new Mock<IDoctorService>();
            var mapper1 = new Mock<IMapper>();
            Doctor docotr = new Doctor { Id = 1 };

            mapper1.Setup(m => m.Map<ReadDoctorDto>(docotr))
                .Returns(null as ReadDoctorDto);
            doctorServiceMock1.Setup(p => p.GetDoctorByIdAsync(docotr.Id))
                .ReturnsAsync(null as Doctor);

            var doctorController1 = new DoctorController(doctorServiceMock1.Object, mapper1.Object);

            // Act
            var actualResult = await doctorController1.GetAsync(docotr.Id);

            // Assert                          
            Assert.True(actualResult is NotFoundResult);
            doctorServiceMock1.Verify(m => m.GetDoctorByIdAsync(docotr.Id), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task Post_DocotrDTO_ReturnsDocotorDTO(
           [Frozen] Doctor doctor,
           [Frozen] User user,
           [Frozen] ReadDoctorDto readDoctorDto,
           [Frozen] CreateDoctorDto createDoctorDto)
        {
            // Arrange            
            doctorServiceMock.Setup(m => m.AddDoctorAsync(doctor, user))
                .ReturnsAsync(doctor);
            mapper.Setup(m => m.Map<Doctor>(createDoctorDto))
                .Returns(doctor);
            mapper.Setup(m => m.Map<User>(createDoctorDto))
                .Returns(user);
            mapper.Setup(m => m.Map<ReadDoctorDto>(doctor))
               .Returns(readDoctorDto);

            // Act
            await doctorController.PostAsync(createDoctorDto);

            // Assert                       
            doctorServiceMock.Verify(m => m.AddDoctorAsync(doctor, user), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task Put_DoctorDTO_ReturnsNoContent(
           [Frozen] Doctor doctor,
           [Frozen] User user,
           [Frozen] UpdateDoctorDto doctorDto)
        {
            // Arrange            
            doctorServiceMock.Setup(m => m.UpdateDoctorAsync(doctor, user, doctor.Id))
                .ReturnsAsync(true);
            mapper.Setup(m => m.Map<Doctor>(doctorDto))
                .Returns(doctor);
            mapper.Setup(m => m.Map<User>(doctorDto))
                .Returns(user);

            // Act
            var actualResult = await doctorController.PutAsync(doctorDto, doctor.Id);

            // Assert             
            Assert.True(actualResult is NoContentResult);
            doctorServiceMock.Verify(m => m.UpdateDoctorAsync(doctor, user, doctor.Id), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task Put_DoctorDTO_ReturnsNotFound(
           [Frozen] Doctor doctor,
           [Frozen] UpdateDoctorDto doctorDto)
        {
            // Arrange            
            doctorServiceMock.Setup(m => m.UpdateDoctorAsync(doctor, doctor.User, doctor.Id))
                .ReturnsAsync(false);
            mapper.Setup(m => m.Map<Doctor>(doctorDto))
                .Returns(doctor);

            // Act
            var actualResult = await doctorController.PutAsync(doctorDto, doctor.Id);

            // Assert             
            Assert.True(actualResult is NotFoundResult);
            Assert.NotNull(actualResult);
        }


        [Theory, AutoMoqData]
        public async Task Delete_DocotrId_ReturnsNoContent(
           [Frozen] int id)
        {
            // Arrange            
            doctorServiceMock.Setup(m => m.RemoveDoctorAsync(id))
                .ReturnsAsync(true);

            // Act
            var actualResult = await doctorController.DeleteAsync(id);

            // Assert             
            Assert.True(actualResult is NoContentResult);
            doctorServiceMock.Verify(m => m.RemoveDoctorAsync(id), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task Delete_DoctorId_ReturnsNotFound(
           [Frozen] int id)
        {
            // Arrange            
            doctorServiceMock.Setup(m => m.RemoveDoctorAsync(id))
                .ReturnsAsync(false);

            // Act
            var actualResult = await doctorController.DeleteAsync(id);

            // Assert             
            Assert.True(actualResult is NotFoundResult);
            doctorServiceMock.Verify(m => m.RemoveDoctorAsync(id), Times.Once);
        }
    }
}
