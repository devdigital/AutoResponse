using AutoFixture;
using AutoFixture.Xunit2;

namespace AutoResponse.WebApi2.IntegrationTests.Helpers
{
    public class AutoCustomData : AutoDataAttribute
    {
        public AutoCustomData()
            : base(() => new Fixture().Customize(new AutoInjectBuilderCustomization()))
        {
            
        }
    }
}