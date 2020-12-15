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
namespace VetClinic.API.Tests.ControllerTests
{
    public class ProcedureControllerTests
    {
        [Fact]
        public void CanChangeProcedureName()
        {
            Procedure procedure = new Procedure { ProcedureName = "First" };
            procedure.ProcedureName = "second";
            Assert.Equal("second", procedure.ProcedureName);
        }

        public void CanAddProcedureToRepository()
        {
            //var service = new Mock<ProcedureService>();
            //ProcedureProfile mapper = new Mock<Mapping.ProcedureProfile>();
            //var controller = new ProcedureController(service, mapper);
            //IEnumerable<int> sdf = new List<int> { 3, 5 };
            //List<int> asf = new List<int>(sdf);
        }

    }
}
