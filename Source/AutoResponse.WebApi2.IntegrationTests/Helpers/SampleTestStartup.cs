namespace AutoResponse.WebApi2.IntegrationTests.Helpers
{
    using AutoResponse.Sample.WebApi2;

    using Owin;

    using WebApiTestServer;

    using Registrations = AutoResponse.Sample.WebApi2.Registrations;

    public class SampleTestStartup : ITestStartup
    {
        public void Bootstrap(IAppBuilder app, WebApiTestServer.Registrations registrations)
        {
            var domainRegistrations = new Registrations(
                registrations.TypeRegistrations,
                registrations.InstanceRegistrations);

            new Bootstrapper(app, domainRegistrations).Run();
        }
    }
}