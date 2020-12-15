using Autofac.Extras.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.BLL.Services.Realizations;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;
using Xunit;

namespace VetClinic.API.Tests
{
    public class PositionServiceTest
    {   [Fact]
        public void GetPositionValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryWrapper>()
                    .Setup( x =>  x.PositionRepository.GetAsync(null,null,null,false))
                    .Returns(Task.Run(()=>CreatePositionsList()));

                var cls = mock.Create<PositionService>();
                var expected =Task.Run(()=> CreatePositionsList());

                var actual = cls.GetAsync();

                Assert.True(actual != null);
                Assert.Equal(expected.Result.Count, actual.Result.Count);

                for (int i = 0; i < expected.Result.Count; i++)
                { 
                    Assert.Equal(expected.Result.ToList()[i].PositionName, actual.Result.ToList()[i].PositionName);
                    Assert.Equal(expected.Result.ToList()[i].Salary, actual.Result.ToList()[i].Salary);
                }
            }
        }

        [Fact]
        public void AddPositionValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var position = CreatePositionsList().ToList<Position>()[0];

                mock.Mock<IRepositoryWrapper>()
                    .Setup(x => x.PositionRepository.Add(position));                  

                var cls = mock.Create<PositionService>();
                cls.Add(position);

                mock.Mock<IRepositoryWrapper>()
                    .Verify(x => x.PositionRepository.Add(position), Times.Exactly(1));             

            }
        }

        private ICollection<Position> CreatePositionsList()
        {
            List<Position> output = new List<Position>
            {
            new Position{ PositionName="Golova", Salary=1110},
            new Position{ PositionName="Noga", Salary=1240},
            new Position{ PositionName="Ruka", Salary=1210},
            new Position{ PositionName="Golova", Salary=1420}
            };
            return output;
        }


    }

    
}
