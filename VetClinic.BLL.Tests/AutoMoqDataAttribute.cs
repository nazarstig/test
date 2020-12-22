using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace VetClinic.BLL.Tests
{
    class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(() => { 
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            return fixture;
        })
        {

        }
    }
}
