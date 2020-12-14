using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace VetClinic.API.Tests
{
    public class PositionControllerTests
    {
        private readonly HttpClient httpClient = new HttpClient();
        //[Fact]           
        //public async Task PosotionControllerGetRequestExpected()
        //{
        //    var responce = await httpClient.GetAsync("https://localhost:44300/api/position/1");            
        //    Assert.Equal(HttpStatusCode.OK, responce.StatusCode);               
        //}

        [Fact]
        public async Task PosotionControllerGetRequestExpected1()
        {
            var mock = new Mock<AnimalsRepository>();
            mock.Setup(s => s.AddAnimal()).ThorwsExcwetpio();
            var service = new AnimalsService(mock);
            Assert.Throws<NullReferenceException>(() => service.AddAnimal(null));
        }
    }

    public class AnimalsRepository
    {
        public List<string> GetAnimals()
        {
            return new List<string>()
            {
                "111",
                "222",
                "333"
            };
        }

        public void AddAnimal(string animal)
        {

            if (animal is null)
                throw new NullReferenceException("Animal is null");

            if (string.IsNullOrEmpty(animal))
                throw new ArgumentException("Animal cannot be empty");

            // add animal
        }
    }

    public class AnimalsService
    {
        private AnimalsRepository animalsRepository = new AnimalsRepository();

        public List<string> GetAnimals()
        {
            return new List<string>()
            {
                "111",
                "222",
                "333"
            };
        }

        public void AddAnimal(string animal)
        {

            if (animal is null)
                throw new NullReferenceException("Animal is null");

            if (string.IsNullOrEmpty(animal))
                throw new ArgumentException("Animal cannot be empty");

            animalsRepository.AddAnimal(animal);
        }
    }
}
