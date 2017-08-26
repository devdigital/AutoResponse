using AutoResponse.Sample.Domain.Services;
using Microsoft.Owin.Testing;

namespace AutoResponse.WebApi2.IntegrationTests.Helpers
{
    using WebApiTestServer;

    public class SampleServerFactory : TestServerFactory
    {
        private bool includeFullDetails;

        public SampleServerFactory() : base(new SampleTestStartup())
        {
        }

        public SampleServerFactory WithIncludeFullDetails()
        {
            this.includeFullDetails = true;
            return this;
        }

        public override TestServer Create()
        {
            this.With<ISettingsService>(new TestSettingsService(this.includeFullDetails));
            return base.Create();
        }
    }
}