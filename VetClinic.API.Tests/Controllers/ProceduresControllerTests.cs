using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoFixture;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VetClinic.API.Controllers;
using VetClinic.API.DTO.ProcedureDTO;
using VetClinic.API.Mapping;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;
using Xunit;
using VetClinic.API.DTO.Responses;

namespace VetClinic.API.Tests.Controllers
{
    public class ProceduresControllerTests
    {
        ProceduresController _proceduresController;
        Mock<IProcedureService> _procedureService;
        Mock<IMapper> _mapper;
        Procedure _procedure;
        int _id;

        public ProceduresControllerTests()
        {
            var fixture = new Fixture();
            _mapper = fixture.Freeze<Mock<IMapper>>();
            
            _procedureService = fixture.Freeze<Mock<IProcedureService>>();

            _proceduresController = new ProceduresController(_procedureService.Object, _mapper.Object);
            
            _id = 3;
        }

        [Fact]
        public void CanChangeProcedureName()
        {
            //Arrange
            Procedure procedure = new Procedure { ProcedureName = "First" };

            //Action
            procedure.ProcedureName = "second";

            //Assert
            Assert.Equal("second", procedure.ProcedureName);
        }

        [Theory, AutoMoqData]
        public async Task Post_Succeded(
            [Frozen] CreateProcedureDto createProcedureDto,
            [Frozen] Procedure procedure,
            [Frozen] ReadProcedureDto readProcedureDto
            )
        {
            //Arrange
            _mapper.Setup(m => m.Map<CreateProcedureDto, Procedure>(createProcedureDto)).Returns(procedure);
            _mapper.Setup(m => m.Map<Procedure, ReadProcedureDto>(procedure)).Returns(readProcedureDto);
            _procedureService.Setup(p => p.AddProcedure(_procedure));

            //Action
            var result = await _proceduresController.PostAsync(createProcedureDto);

            //Assert
            var createdObj = Assert.IsType<CreatedResult>(result);
            Assert.True(result is CreatedResult);
            Assert.Equal(readProcedureDto, ((Response<ReadProcedureDto>) createdObj.Value).Data);
        }

        //[Fact]
        [Theory, AutoMoqData]
        public async Task GetAll_ReturnsResult(
            [Frozen] ICollection<Procedure> collection)
        {
            //Arrange
            _procedureService.Setup(p => p.GetAllProcedures()).ReturnsAsync(collection);

            //Action
            var result = await _proceduresController.GetAsync(); 
            
            //Assert
            Assert.True(result.Result is OkObjectResult);
        }

        [Theory, AutoMoqData]
        public async Task Get_Succeded(
            [Frozen] Procedure procedure)
        {
            //Arrange
            _procedureService.Setup(p => p.GetProcedure(_id)).ReturnsAsync(procedure);

            //Action
            var result = await _proceduresController.GetAsync(_id);

            //Assert
            Assert.True(result.Result is OkObjectResult);
        }

        [Fact]
        public async Task Get_Failed()
        {
            //Arrange
            _procedureService.Setup(p => p.GetProcedure(_id)).ReturnsAsync((Procedure)null);

            //Action
            var result = await _proceduresController.GetAsync(_id);

            //Assert
            Assert.True(result.Result is NotFoundResult);
        }

        [Fact]
        public async Task Delete_Failed()
        {
            //Arrange
            _procedureService.Setup(p => p.DeleteProcedure(_id)).ReturnsAsync(false);

            //Action
            var result = await _proceduresController.DeleteAsync(_id);

            //Assert
            Assert.True(result is NotFoundResult);
            _procedureService.Verify(p => p.DeleteProcedure(_id), Times.Exactly(1));
        }

        [Fact]
        public async Task Delete_Succeded()
        {
            //Arrange
            _procedureService.Setup(p => p.DeleteProcedure(_id)).ReturnsAsync(true);

            //Action
            var result = await _proceduresController.DeleteAsync(_id);

            //Assert
            Assert.IsType<NoContentResult>(result);
            _procedureService.Verify(p => p.DeleteProcedure(_id), Times.Exactly(1));
        }

        [Theory, AutoMoqData]
        public async Task Put_Failed(
            [Frozen] UpdateProcedureDto dto,
            [Frozen] Procedure procedure)
        {
            //Arrange
            _mapper.Setup(m => m.Map<UpdateProcedureDto, Procedure>(dto)).Returns(procedure);
            _procedureService.Setup(p => p.PutProcedure(_id, procedure)).ReturnsAsync(false);

            //Action
            var result = await _proceduresController.PutAsync(_id, dto);

            //Assert
            Assert.True(result is NotFoundResult);
        }

        [Theory, AutoMoqData]
        public async Task Put_Succeded(
            [Frozen] UpdateProcedureDto dto,
            [Frozen] Procedure procedure)
        {

            //Arrange
            _mapper.Setup(m => m.Map<UpdateProcedureDto, Procedure>(dto)).Returns(procedure);
            _procedureService.Setup(p => p.PutProcedure(_id, procedure)).ReturnsAsync(true);

            //Action
            var result = await _proceduresController.PutAsync(_id, dto);

            //Assert
            Assert.True(result is NoContentResult);
        }
    }
}
