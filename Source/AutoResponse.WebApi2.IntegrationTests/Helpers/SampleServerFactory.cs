namespace AutoResponse.WebApi2.IntegrationTests.Helpers
{
    using WebApiTestServer;

    public class SampleServerFactory : TestServerFactory
    {
        public SampleServerFactory() : base(new SampleTestStartup())
        {
        }
    }
}