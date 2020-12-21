using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using VetClinic.BLL.Services;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;
using VetClinic.DAL.Repositories.Realizations;
using VetClinic.DAL;
using VetClinic.BLL.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using VetClinic.API.Mapping;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query;

namespace VetClinic.BLL.Tests.ServiceTests.ProcedureServiceTests
{

    public class ProcedureServiceTests
    {
        IProcedureService _procedureService;
        Mock<IRepositoryWrapper> _repositoryWrapper;
        Mock<IProcedureRepository> _procedureRepository;
        Mock<DbContext> _appContext;
        IMapper _mapper;

        public ProcedureServiceTests()
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
            _procedureService = new ProcedureService(_repositoryWrapper.Object);
        }

        [Fact]
        public async void GetOperationTest()
        {
            //var applicationContext = new Mock<ApplicationContext>();
            //var repositoryWrapper = new Mock<IRepositoryWrapper>();
            Procedure procedure = new Procedure { Id = 9, ProcedureName = "nail cutting", Description = "for cats only", Price = 100M, IsSelectable = false };
            
            _repositoryWrapper.Setup(r => r.ProcedureRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Procedure, bool>>>(),
                It.IsAny<Func<IQueryable<Procedure>, IIncludableQueryable<Procedure, object>>>(), 
                It.IsAny<bool>()
                )).ReturnsAsync(procedure);
           
            var result = await _procedureService.GetProcedure(134);
            Assert.Equal(result.Price, procedure.Price);
        }

        [Fact]
        public async void GetAllProceduresOperationTest()
        {
            _repositoryWrapper.Setup(r => r.ProcedureRepository.GetAsync(
               It.IsAny<Expression<Func<Procedure, bool>>>(),
               It.IsAny<Func<IQueryable<Procedure>, IIncludableQueryable<Procedure, object>>>(),
               It.IsAny<Func<IQueryable<Procedure>, IOrderedQueryable<Procedure>>>(),
               It.IsAny<bool>()
               )).ReturnsAsync(ProceduresList());

            var result = await _procedureService.GetAllProcedures();
            Assert.Equal(result.Count, ProceduresList().Count);
        }

        [Fact]
        public async void AddOperationTest()
        {
            //arrange
            Procedure procedure = new Procedure { Id = 9, ProcedureName = "nail cutting", Description = "for cats only", Price = 100M, IsSelectable = false };
            _procedureRepository.Setup(repo => repo.Add(procedure));
            await _procedureService.AddProcedure(procedure);
            _procedureRepository.Verify(r => r.Add(It.IsAny<Procedure>()), Times.Once);
        }

        [Fact]
        public async void PutOperationTest()
        {
            Procedure procedure = new Procedure { Id = 9, ProcedureName = "nail cutting", Description = "for cats only", Price = 100M, IsSelectable = false };
            _procedureRepository.Setup(repo => repo.Update(procedure));
     
            var res = await _procedureService.PutProcedure(procedure);
            var result = await _procedureService.PutProcedure(procedure);
            Assert.NotNull(result);
        }

        [Fact]
        public async void DeleteOperationTest()
        {
            Procedure procedure = new Procedure { Id = 9, ProcedureName = "nail cutting", Description = "for cats only", Price = 100M, IsSelectable = false };
            _procedureRepository.Setup(repo => repo.Remove(procedure));
            var result = await _procedureService.DeleteProcedure(9);
            Assert.NotNull(result);
        }

        private List<Procedure> ProceduresList()
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
