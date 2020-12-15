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

namespace VetClinic.BLL.Tests.ServiceTests.ProcedureServiceTests
{

    public class ProcedureServiceTests
    {
        [Fact]
        public async void GetOperationTest()
        {
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            var applicationContext = new Mock<ApplicationContext>();
       
            Procedure procedure = new Procedure { Id = 9, ProcedureName = "nail cutting", Description = "for cats only", Price = 100M, IsSelectable = false };
            repositoryWrapper.Setup(repo => repo.ProcedureRepository.Add(procedure));            
            repositoryWrapper.Setup(repo => repo.ProcedureRepository);
            applicationContext.Setup(app => app.SaveChanges());           
            var service = new ProcedureService(repositoryWrapper.Object);
            var result = await service.GetProcedure(9);
            Assert.Equal(result.Price, procedure.Price);
        }
    }
}
