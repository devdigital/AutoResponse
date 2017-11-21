namespace AutoResponse.WebApi2.IntegrationTests.Helpers
{
    using Ploeh.AutoFixture;

    internal class AutoInjectBuilderCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new AutoInjectBuilder());
        }
    }
}