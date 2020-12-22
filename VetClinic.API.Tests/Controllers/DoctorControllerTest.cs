using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.API.Controllers;
using VetClinic.API.DTO.Doctor;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using Xunit;

namespace VetClinic.API.Tests.Controllers
{
    public class DoctorControllerTest
    {


        private readonly Mock<IDoctorService> doctorServiceMock ;
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
            [Frozen] List<ReadDoctorDto> doctorDto)
        {
            // Arrange
            doctorServiceMock.Setup(m => m.GetDoctorAsync())
                .ReturnsAsync(doctor);
            mapper.Setup(m => m.Map<ICollection<ReadDoctorDto>>(doctor))
                .Returns(doctorDto);

            // Act
            var actualResult = await doctorController.Index();

            // Assert      
            var result = actualResult.Result as OkObjectResult;

            Assert.Equal(doctorDto, result.Value);
            Assert.True(actualResult.Result is OkObjectResult);
            doctorServiceMock.Verify(m => m.GetDoctorAsync(), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task GetById_DoctorId_ReturnsDoctorWithRequestedId(
           [Frozen] Doctor doctor,
           [Frozen] ReadDoctorDto doctorDto)
        {
            // Arrange           
            doctorServiceMock.Setup(p => p.GetDoctorAsync(doctor.Id))
                .ReturnsAsync(doctor);
            mapper.Setup(m => m.Map<ReadDoctorDto>(doctor))
                .Returns(doctorDto);

            // Act
            var actualResult = await doctorController.Show(doctor.Id);

            // Assert    
            var result = actualResult.Result as OkObjectResult;
            Assert.Equal(doctorDto, result.Value);
            Assert.True(actualResult.Result is OkObjectResult);
            doctorServiceMock.Verify(m => m.GetDoctorAsync(doctor.Id), Times.Once);
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
            doctorServiceMock1.Setup(p => p.GetDoctorAsync(docotr.Id))
                .ReturnsAsync(null as Doctor);
            

            var doctorController1 = new DoctorController(doctorServiceMock1.Object, mapper1.Object);

            // Act
            var actualResult = await doctorController1.Show(docotr.Id);

            // Assert                          
            Assert.True(actualResult.Result is NotFoundResult);
            doctorServiceMock1.Verify(m => m.GetDoctorAsync(docotr.Id), Times.Once);

        }


        [Theory, AutoMoqData]
        public async Task Post_DocotrDTO_ReturnsDocotorDTO(
           [Frozen] Doctor doctor,
           [Frozen] ReadDoctorDto readDoctorDto,
           [Frozen] CreateDoctorDto createDoctorDto)
        {
            // Arrange            
            doctorServiceMock.Setup(m => m.AddDoctorAsync(doctor,doctor.User))
                .ReturnsAsync(doctor);
            mapper.Setup(m => m.Map<Doctor>(createDoctorDto))
                .Returns(doctor);
            mapper.Setup(m => m.Map<ReadDoctorDto>(doctor))
               .Returns(readDoctorDto);

            // Act
            var actualResult = await doctorController.Create(createDoctorDto);

            // Assert 
            var result = actualResult.Result as CreatedAtActionResult;
            Assert.Equal(readDoctorDto, result.Value);
            doctorServiceMock.Verify(m => m.AddDoctorAsync(doctor,doctor.User), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task Put_DoctorDTO_ReturnsNoContent(
           [Frozen] Doctor doctor,
           [Frozen] ReadDoctorDto doctorDto)
        {
            // Arrange            
            doctorServiceMock.Setup(m => m.UpdateDoctorAsync(doctor, doctor.User, doctor.Id))
                .ReturnsAsync(true);
            mapper.Setup(m => m.Map<Doctor>(doctorDto))
                .Returns(doctor);

            // Act
            var actualResult = await doctorController.Update(doctorDto, doctor.Id);

            // Assert             
            Assert.True(actualResult.Result is NoContentResult);
            doctorServiceMock.Verify(m => m.UpdateDoctorAsync(doctor, doctor.User, doctor.Id), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task Put_DoctorDTO_ReturnsNotFound(
           [Frozen] Doctor doctor,
           [Frozen] ReadDoctorDto doctorDto)
        {
            // Arrange            
            doctorServiceMock.Setup(m => m.UpdateDoctorAsync(doctor, doctor.User , doctor.Id))
                .ReturnsAsync(false);
            mapper.Setup(m => m.Map<Doctor>(doctorDto))
                .Returns(doctor);

            // Act
            var actualResult = await doctorController.Update(doctorDto, doctor.Id);

            // Assert             
            Assert.True(actualResult.Result is NotFoundResult);
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
            var actualResult = await doctorController.Destroy(id);

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
            var actualResult = await doctorController.Destroy(id);

            // Assert             
            Assert.True(actualResult is NotFoundResult);
            doctorServiceMock.Verify(m => m.RemoveDoctorAsync(id), Times.Once);
        }

    }
}
