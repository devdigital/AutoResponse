namespace AutoResponse.WebApi2.IntegrationTests.Helpers
{
    using AutoResponse.WebApi2.IntegrationTests.Tests;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit2;

    public class AutoCustomData : AutoDataAttribute
    {
        public AutoCustomData()
            : base(new Fixture().Customize(new AutoInjectBuilderCustomization()))
        {
            
        }
    }
}