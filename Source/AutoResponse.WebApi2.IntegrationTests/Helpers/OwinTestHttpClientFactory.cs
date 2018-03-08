using System.Net.Http;
using Flurl.Http.Configuration;
using Microsoft.Owin.Testing;

namespace AutoResponse.WebApi2.IntegrationTests.Helpers
{
    // Adapted from https://stackoverflow.com/a/33128757/248164
    public class OwinTestHttpClientFactory : DefaultHttpClientFactory
    {
        private readonly TestServer testServer;

        public OwinTestHttpClientFactory(TestServer server)
        {
            this.testServer = server;
        }

        public override HttpMessageHandler CreateMessageHandler()
        {
            return this.testServer.Handler;
        }
    }
}