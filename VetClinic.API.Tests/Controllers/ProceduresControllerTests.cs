﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VetClinic.API.Controllers;
using VetClinic.API.DTO.ProcedureDTO;
using VetClinic.API.Mapping;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;
using Xunit;

namespace VetClinic.API.Tests.Controllers
{
    public class ProceduresControllerTests
    {
        ProceduresController _proceduresController;
        Mock<IProcedureService> _procedureService;
        Mock<IRepositoryWrapper> _repositoryWrapper;
        Mock<IProcedureRepository> _procedureRepository;
        IMapper _mapper;
        Procedure _procedure;

        public ProceduresControllerTests()
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
            _proceduresController = new ProceduresController(_procedureService.Object, _mapper);

            _procedure = new Procedure
            {
                Id = 9,
                ProcedureName = "nail cutting",
                Description = "for cats only",
                Price = 100M,
            };            
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

        [Fact]
        public async Task GetAll_ReturnsResult()
        {
            //Arrange
            ICollection<Procedure> collection = ProceduresList();
            _procedureService.Setup(p => p.GetAllProcedures()).ReturnsAsync(collection);

            //Action
            var result = await _proceduresController.GetAsync(); 
            
            //Assert
            Assert.True(result.Result is OkObjectResult);
        }

        [Fact]
        public async Task Get_Succeded()
        {
            //Arrange
            _procedureService.Setup(p => p.GetProcedure(9)).ReturnsAsync(_procedure);

            //Action
            var result = await _proceduresController.GetAsync(9);

            //Assert
            Assert.True(result.Result is OkObjectResult);
        }

        public async Task Get_Failed()
        {
            //Arrange
            _procedureService.Setup(p => p.GetProcedure(9)).ReturnsAsync(_procedure);

            //Action
            var result = await _proceduresController.GetAsync(6);

            //Assert
            Assert.True(result.Result is NotFoundResult);
        }

        [Fact]
        public async Task Delete_Failed()
        {
            //Arrange
            _procedureService.Setup(p => p.DeleteProcedure(3)).ReturnsAsync(false);

            //Action
            var result = await _proceduresController.DeleteAsync(3);

            //Assert
            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public async Task Delete_Succeded()
        {
            //Arrange
            _procedureService.Setup(p => p.DeleteProcedure(3)).ReturnsAsync(true);

            //Action
            var result = await _proceduresController.DeleteAsync(3);

            //Assert
            Assert.True(result is NoContentResult);
        }

        [Fact]
        public async Task Put_Failed()
        {
            //Arrange
            UpdateProcedureDto dto = new UpdateProcedureDto { };
            _procedureService.Setup(p => p.PutProcedure(3, _procedure)).ReturnsAsync(false);

            //Action
            var result = await _proceduresController.PutAsync(3, dto);

            //Action
            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public async Task Put_Succeded()
        {
            //Arrange
            UpdateProcedureDto dto = new UpdateProcedureDto { };
            _procedureService.Setup(p => p.DeleteProcedure(3)).ReturnsAsync(true);

            //Action
            var result = await _proceduresController.DeleteAsync(3);

            //Assert
            Assert.True(result is NoContentResult);
        }

        // [Fact]
        // public async Task Post_Succeded()
        // {
        //     //Arrange
        //     CreateProcedureDto dto = new CreateProcedureDto { };
        //     _procedureService.Setup(p => p.AddProcedure(_procedure));
        //
        //     //Action
        //     var result = await _procedureController.PostAsync(dto);
        //
        //     //Assert
        //     Assert.True(result is CreatedAtActionResult);
        // }

        private ICollection<Procedure> ProceduresList()
        {
            return new List<Procedure>
            {
                    new Procedure{Id=1,  ProcedureName="SPA procedures", Description="Best for your pet", Price=1000},
                    new Procedure{Id=2,  ProcedureName="Operation", Description="Paw fracture", Price=2000},
                    new Procedure{Id=3,  ProcedureName="Examination of animal", Description="Pet inspection", Price=50}
            };
        }

    }
}