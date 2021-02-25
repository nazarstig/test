using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using VetClinic.API.Mapping;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.BLL.Services.Realizations;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;
using Xunit;

namespace VetClinic.BLL.Tests.Services
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
            _procedure = new Procedure { Id = 9, ProcedureName = "nail cutting", Description = "for cats only", Price = 100M};
        }

        [Fact]
        public async Task GetProcedure_ReturnsResult()
        {
            //Arrange
            int id = 134;
            _repositoryWrapper.Setup(r => r.ProcedureRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Procedure, bool>>>(),
                It.IsAny<Func<IQueryable<Procedure>, IIncludableQueryable<Procedure, object>>>(), 
                It.IsAny<bool>()
                )).ReturnsAsync(_procedure);
           
            //Action
            var result = await _procedureService.GetProcedure(id);

            //Assert
            Assert.Equal(result.Price, _procedure.Price);
        }

        [Fact]
        public async Task GetAllProcedures_ReturnsResult()
        {
            //Arrange
            _repositoryWrapper.Setup(r => r.ProcedureRepository.GetAsync(
               It.IsAny<Expression<Func<Procedure, bool>>>(),
               It.IsAny<Func<IQueryable<Procedure>, IIncludableQueryable<Procedure, object>>>(),
               It.IsAny<Func<IQueryable<Procedure>, IOrderedQueryable<Procedure>>>(),
               It.IsAny<int?>(),
               It.IsAny<int?>(),
               It.IsAny<bool>()
               )).ReturnsAsync(ProceduresList());

            //Action
            var result = await _procedureService.GetAllProcedures();

            //Assert
            Assert.Equal(result.Count, ProceduresList().Count);
        }

        [Fact]
        public async Task AddProcedure_Invoked()
        {
            //Arrange
            _procedureRepository.Setup(repo => repo.Add(_procedure));

            //Action
            await _procedureService.AddProcedure(_procedure);

            //Assert
            _procedureRepository.Verify(r => r.Add(It.IsAny<Procedure>()), Times.Once);
        }

        [Fact]
        public async Task PutProcedure_ReturnsResult()
        {
            //Arrange
            _procedureRepository.Setup(repo => repo.Update(_procedure));
            _repositoryWrapper.Setup(r => r.ProcedureRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Procedure, bool>>>(),
             It.IsAny<Func<IQueryable<Procedure>, IIncludableQueryable<Procedure, object>>>(),
             It.IsAny<bool>()
             )).ReturnsAsync(_procedure);

            //Action
            var result = await _procedureService.PutProcedure(_procedure.Id, _procedure);
            
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task PutProcedure_UpdatesObject()
        {
            //arrange
            Procedure procedureUpdated = new Procedure { ProcedureName = "nail cleaning", Description = "for cats only", Price = 50M};
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
            //Arrange
            _procedureRepository.Setup(repo => repo.Remove(_procedure));

            //Action
            var result = await _procedureService.DeleteProcedure(9);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteProcedure_Succeded()
        {
            //Arrange
            _repositoryWrapper.Setup(r => r.ProcedureRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Procedure, bool>>>(),
               It.IsAny<Func<IQueryable<Procedure>, IIncludableQueryable<Procedure, object>>>(),
               It.IsAny<bool>()
               )).ReturnsAsync(_procedure);
            _procedureRepository.Setup(repo => repo.Remove(_procedure));

            //Action
            var res = await _procedureService.DeleteProcedure(_procedure.Id);

            //Assert
            Assert.Equal(res, true);
        }


        private List<Procedure> ProceduresList()
        {
            return new List<Procedure>
            {
                new Procedure{Id=1, ProcedureName="SPA procedures", Description="Best for your pet", Price=1000},
                new Procedure{Id=2, ProcedureName="Operation", Description="Paw fracture", Price=2000},
                new Procedure{Id=3, ProcedureName="Examination of animal", Description="Pet inspection", Price=50}
            };
        }
    }
}
