using System.Threading.Tasks;
using AutoFixture.Xunit2;
using AutoResponse.Client;
using AutoResponse.WebApi2.IntegrationTests.Helpers;
using Flurl.Http;
using Flurl.Http.Testing;
using Xunit;

namespace AutoResponse.WebApi2.IntegrationTests.Tests
{
    // ReSharper disable StyleCop.SA1600
    #pragma warning disable SA1600
    #pragma warning disable 1591

    public class FlurlTests
    {
        [Theory]
        [AutoData]
        public async Task GetNotFoundHandlesError(string message)
        {
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWithJson(body: new { message }, status: 404);

                try
                {
                    var response = await "http://localhost"
                        .PostJsonAsync(new { message })
                        .ReceiveJson();
                }
                catch (FlurlHttpException exception)
                {
                    var response = exception.Call?.Response;
                    if (response == null)
                    {
                        throw;
                    }

                    await response.HandleErrors(
                        throwOnUnhandledResponses: false);
                }
            }
        }

        [Theory]
        [AutoData]
        public async Task GetNotFoundTestServerHandlesError(SampleServerFactory serverFactory)
        {
            using (var server = serverFactory.Create())
            {
                using (var flurlClient = new FlurlClient())
                {
                    flurlClient.Settings.HttpClientFactory =
                        new OwinTestHttpClientFactory(server);

                    flurlClient.BaseUrl = "http://localhost";

                    try
                    {
                        var json = await flurlClient.Request("foo").GetJsonAsync();
                    }
                    catch (FlurlHttpException exception)
                    {
                        var response = exception.Call?.Response;
                        if (response == null)
                        {
                            throw;
                        }

                        await response.HandleErrors(
                            throwOnUnhandledResponses: false);
                    }
                }                
            }
        }

    }
}