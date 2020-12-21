using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using VetClinic.API.Controllers;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;
using VetClinic.DAL.Repositories.Realizations;
using VetClinic.API.Mapping;
using VetClinic.BLL.Services;
using Xunit;
using VetClinic.BLL.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VetClinic.API.DTO.ProcedureDTO;

namespace VetClinic.API.Tests.ControllerTests
{
    public class ProcedureControllerTests
    {
        ProcedureController _procedureController;
        Mock<IProcedureService> _procedureService;
        Mock<IRepositoryWrapper> _repositoryWrapper;
        Mock<IProcedureRepository> _procedureRepository;
        Mock<DbContext> _appContext;
        IMapper _mapper;
        Procedure _procedure;

        public ProcedureControllerTests()
        {
            _repositoryWrapper = new Mock<IRepositoryWrapper>();
            _procedureRepository = new Mock<IProcedureRepository>();
            _repositoryWrapper.Setup(r => r.ProcedureRepository).Returns(_procedureRepository.Object);
            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new ProcedureProfile());
            }
            );

            _mapper = mapperConfig.CreateMapper();
            _procedureService = new Mock<IProcedureService>();
            _procedureController = new ProcedureController(_procedureService.Object, _mapper);

            _procedure = new Procedure
            {
                Id = 9,
                ProcedureName = "nail cutting",
                Description = "for cats only",
                Price = 100M,
                IsSelectable = false
            };
            // _procedureService.Setup()            
        }

        [Fact]
        public void CanChangeProcedureName()
        {
            Procedure procedure = new Procedure { ProcedureName = "First" };
            procedure.ProcedureName = "second";
            Assert.Equal("second", procedure.ProcedureName);
        }

        [Fact]
        public async Task IndexOperationCheck()
        {
            ICollection<Procedure> collection = ProceduresList();
            _procedureService.Setup(p => p.GetAllProcedures()).ReturnsAsync(collection);
            var result = await _procedureController.Index();           
            Assert.True(result.Result is OkObjectResult);
        }

        [Fact]
        public async Task ShowOperationCheck_Succeded()
        {
          
            _procedureService.Setup(p => p.GetProcedure(9)).ReturnsAsync(_procedure);
            var result = await _procedureController.Show(9);
            Assert.True(result.Result is OkObjectResult);
        }

        public async Task ShowOperationCheck_Failed()
        {
            _procedureService.Setup(p => p.GetProcedure(9)).ReturnsAsync(_procedure);
            var result = await _procedureController.Show(6);
            Assert.True(result.Result is NotFoundResult);
        }

        [Fact]
        public async Task DestroyOperationCheck_Failed()
        {
            _procedureService.Setup(p => p.DeleteProcedure(3)).ReturnsAsync(false);
            var result = await _procedureController.Destroy(3);
            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public async Task DestroyOperation_Succeded()
        {
            _procedureService.Setup(p => p.DeleteProcedure(3)).ReturnsAsync(true);
            var result = await _procedureController.Destroy(3);
            Assert.True(result is NoContentResult);
        }

        [Fact]
        public async Task UpdateOperationCheck_Failed()
        {
            UpdateProcedureDTO dto = new UpdateProcedureDTO { };
            _procedureService.Setup(p => p.PutProcedure(_procedure)).ReturnsAsync(false);
            var result = await _procedureController.Update(dto);
            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public async Task UpdateOperation_Succeded()
        {
            UpdateProcedureDTO dto = new UpdateProcedureDTO { };
            _procedureService.Setup(p => p.DeleteProcedure(3)).ReturnsAsync(true);
            var result = await _procedureController.Destroy(3);
            Assert.True(result is NoContentResult);
        }

        [Fact]
        public async Task CreateOperationTest()
        {
            CreateProcedureDTO dto = new CreateProcedureDTO { };
            _procedureService.Setup(p => p.AddProcedure(_procedure));
            var result = await _procedureController.Create(dto);
            Assert.True(result is CreatedAtActionResult);
        }

        private ICollection<Procedure> ProceduresList()
        {
            return new List<Procedure>
            {
                    new Procedure{Id=1, IsSelectable=true, ProcedureName="SPA procedures", Description="Best for your pet", Price=1000},
                    new Procedure{Id=2, IsSelectable=false, ProcedureName="Operation", Description="Paw fracture", Price=2000},
                    new Procedure{Id=3, IsSelectable=true, ProcedureName="Examination of animal", Description="Pet inspection", Price=50}
            };
        }

    }
}
