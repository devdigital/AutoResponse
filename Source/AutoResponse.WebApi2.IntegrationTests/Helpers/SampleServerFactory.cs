namespace AutoResponse.WebApi2.IntegrationTests.Helpers
{
    using WebApiTestServer;

    public class SampleServerFactory : TestServerFactory<SampleServerFactory>
    {
        public SampleServerFactory() : base(new SampleTestStartup())
        {
        }
    }
}