namespace AutoResponse.WebApi2.IntegrationTests.Tests
{
    using System.Net;
    using System.Threading.Tasks;

    using AutoResponse.WebApi2.IntegrationTests.Helpers;

    using Ploeh.AutoFixture.Xunit2;

    using Xunit;

    public class ExceptionMapperTests
    {
        [Theory]
        [AutoData]
        public async Task NotFoundExceptionShouldReturn404(SampleServerFactory serverFactory)
        {
            using (var server = serverFactory.Create())
            {
                var response = await server.HttpClient.GetAsync("/api/values/2");
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}
