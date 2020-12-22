using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using VetClinic.BLL.Services;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;
using VetClinic.BLL.Services.Interfaces;
using AutoMapper;
using VetClinic.API.Mapping;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query;
using System.Threading.Tasks;

namespace VetClinic.BLL.Tests.ServiceTests.ProcedureServiceTests
{

    public class ProcedureServiceTests
    {
        IProcedureService _procedureService;
        Mock<IRepositoryWrapper> _repositoryWrapper;
        Mock<IProcedureRepository> _procedureRepository;
        IMapper _mapper;
        Procedure _procedure;

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
            _procedure = new Procedure { Id = 9, ProcedureName = "nail cutting", Description = "for cats only", Price = 100M, IsSelectable = false };
        }

        [Fact]
        public async Task GetProcedure_ReturnsResult()
        {   
            //arrange
            _repositoryWrapper.Setup(r => r.ProcedureRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Procedure, bool>>>(),
                It.IsAny<Func<IQueryable<Procedure>, IIncludableQueryable<Procedure, object>>>(), 
                It.IsAny<bool>()
                )).ReturnsAsync(_procedure);
           
            //action
            var result = await _procedureService.GetProcedure(134);

            //assert
            Assert.Equal(result.Price, _procedure.Price);
        }

        [Fact]
        public async Task GetAllProcedures_ReturnsResult()
        {
            //arrange
            _repositoryWrapper.Setup(r => r.ProcedureRepository.GetAsync(
               It.IsAny<Expression<Func<Procedure, bool>>>(),
               It.IsAny<Func<IQueryable<Procedure>, IIncludableQueryable<Procedure, object>>>(),
               It.IsAny<Func<IQueryable<Procedure>, IOrderedQueryable<Procedure>>>(),
               It.IsAny<bool>()
               )).ReturnsAsync(ProceduresList());

            //action
            var result = await _procedureService.GetAllProcedures();

            //assert
            Assert.Equal(result.Count, ProceduresList().Count);
        }

        [Fact]
        public async Task AddProcedure_Invoked()
        {
            //arrange
            _procedureRepository.Setup(repo => repo.Add(_procedure));

            //action
            await _procedureService.AddProcedure(_procedure);

            //assert
            _procedureRepository.Verify(r => r.Add(It.IsAny<Procedure>()), Times.Once);
        }

        [Fact]
        public async Task PutProcedure_ReturnsResult()
        {
            //arrange
            _procedureRepository.Setup(repo => repo.Update(_procedure));
            _repositoryWrapper.Setup(r => r.ProcedureRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Procedure, bool>>>(),
             It.IsAny<Func<IQueryable<Procedure>, IIncludableQueryable<Procedure, object>>>(),
             It.IsAny<bool>()
             )).ReturnsAsync(_procedure);

            //action
            var result = await _procedureService.PutProcedure(_procedure.Id, _procedure);
            
            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task PutProcedure_UpdatesObject()
        {
            //arrange
            Procedure procedureUpdated = new Procedure { ProcedureName = "nail cleaning", Description = "for cats only", Price = 50M, IsSelectable = false };
            _procedureRepository.Setup(repo => repo.Update(_procedure));
            _repositoryWrapper.Setup(r => r.ProcedureRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Procedure, bool>>>(),
             It.IsAny<Func<IQueryable<Procedure>, IIncludableQueryable<Procedure, object>>>(),
             It.IsAny<bool>()
             )).ReturnsAsync(_procedure);

            //action
            var res = await _procedureService.PutProcedure(9, procedureUpdated);

            //asset
            Assert.Equal(_procedure.ProcedureName, procedureUpdated.ProcedureName);

        }

        [Fact]
        public async Task DeleteProcedure_ReturnsResult()
        {
            //arrange
            _procedureRepository.Setup(repo => repo.Remove(_procedure));

            //action
            var result = await _procedureService.DeleteProcedure(9);

            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteProcedure_Succeded()
        {
            //arrange
            _repositoryWrapper.Setup(r => r.ProcedureRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Procedure, bool>>>(),
               It.IsAny<Func<IQueryable<Procedure>, IIncludableQueryable<Procedure, object>>>(),
               It.IsAny<bool>()
               )).ReturnsAsync(_procedure);
            _procedureRepository.Setup(repo => repo.Remove(_procedure));

            //action
            var res = await _procedureService.DeleteProcedure(_procedure.Id);

            //assert
            Assert.Equal(res, true);
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
