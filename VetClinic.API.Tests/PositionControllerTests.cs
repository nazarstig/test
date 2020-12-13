using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace VetClinic.API.Tests
{
    public class PositionControllerTests
    {
        private readonly HttpClient httpClient = new HttpClient();
        [Fact]           
        public async Task PosotionControllerGetRequestExpected()
        {
            var responce = await httpClient.GetAsync("https://localhost:44300/api/position/1");            
            Assert.Equal(HttpStatusCode.OK, responce.StatusCode);               
        }
    }
}
