using AutoFixture;

namespace AutoResponse.WebApi2.IntegrationTests.Helpers
{
    internal class AutoInjectBuilderCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new AutoInjectBuilder());
        }
    }
}