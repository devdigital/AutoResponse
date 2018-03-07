using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using AutoResponse.Client;
using AutoResponse.WebApi2.IntegrationTests.Helpers;
using Xunit;

namespace AutoResponse.WebApi2.IntegrationTests.Tests.ClientTests
{
    public class ClientUnknownResponseTests
    {
        [Theory]
        [AutoData]
        public async Task UnknownResponseExceptionContainsRequest(
            SampleServerFactory serverFactory)
        {
            using (var server = serverFactory.Create())
            {
                var response = await server.HttpClient.GetAsync("/foo");
                var exception = await Assert.ThrowsAsync<Exception>(() => response.HandleErrors());
                Assert.True(exception.Message.Contains("Request"));
            }
        }

        [Theory]
        [AutoData]
        public async Task UnknownResponseExceptionContainsResponse(
            SampleServerFactory serverFactory)
        {
            using (var server = serverFactory.Create())
            {
                var response = await server.HttpClient.GetAsync("/foo");
                var exception = await Assert.ThrowsAsync<Exception>(() => response.HandleErrors());
                Assert.True(exception.Message.Contains("Response"));
            }
        }
    }
}