namespace AutoResponse.WebApi2.IntegrationTests.Tests
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Sample.WebApi2.Factories;
    using AutoResponse.WebApi2.IntegrationTests.Attributes;
    using AutoResponse.WebApi2.IntegrationTests.Helpers;
    using AutoResponse.WebApi2.IntegrationTests.Models;

    using Moq;

    using Xunit;

    public class WebApiActionResultTests
    {
        [Theory]
        [AutoMoqData]
        public async Task UnauthenticatedResultShouldReturn401(
            SampleServerFactory serverFactory,
            Mock<IHttpActionResultFactory> actionResultFactory,
            HttpRequestMessage request,
            UnauthenticatedApiEvent apiEvent)
        {
            var result = new TestAutoResponseResult(request, apiEvent);
            actionResultFactory.Setup(f => f.Create(It.IsAny<HttpRequestMessage>())).Returns(result);

            using (var server = serverFactory.With<IHttpActionResultFactory>(actionResultFactory.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/api/result");
                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }

        [Theory]
        [AutoMoqData]
        public async Task NotFoundResultShouldReturn404(
            SampleServerFactory serverFactory,
            Mock<IHttpActionResultFactory> actionResultFactory,
            HttpRequestMessage request,
            EntityNotFoundApiEvent apiEvent)
        {
            var result = new TestAutoResponseResult(request, apiEvent);
            actionResultFactory.Setup(f => f.Create(It.IsAny<HttpRequestMessage>())).Returns(result);

            using (var server = serverFactory.With<IHttpActionResultFactory>(actionResultFactory.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/api/result");
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Theory]
        [AutoMoqData]
        public async Task ValidationResultShouldReturn422(
           SampleServerFactory serverFactory,
           Mock<IHttpActionResultFactory> actionResultFactory,
           HttpRequestMessage request,     
           EntityValidationApiEvent apiEvent)
        {
            var result = new TestAutoResponseResult(request, apiEvent);
            actionResultFactory.Setup(f => f.Create(It.IsAny<HttpRequestMessage>())).Returns(result);

            using (var server = serverFactory.With<IHttpActionResultFactory>(actionResultFactory.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/api/result");
                Assert.Equal((HttpStatusCode)422, response.StatusCode);
            }
        }

        [Theory]
        [AutoMoqData]
        public async Task CreatePermissionResultShouldReturn403(
            SampleServerFactory serverFactory,
            Mock<IHttpActionResultFactory> actionResultFactory,
            HttpRequestMessage request,
            EntityCreatePermissionApiEvent apiEvent)
        {
            var result = new TestAutoResponseResult(request, apiEvent);
            actionResultFactory.Setup(f => f.Create(It.IsAny<HttpRequestMessage>())).Returns(result);

            using (var server = serverFactory
                .With<IHttpActionResultFactory>(actionResultFactory.Object)
                .Create())
            {
                var response = await server.HttpClient.GetAsync("/api/result");
                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            }
        }

        [Theory]
        [AutoMoqData]
        public async Task PermissionResultShouldReturn403(
            SampleServerFactory serverFactory,
            Mock<IHttpActionResultFactory> actionResultFactory,
            HttpRequestMessage request,
            EntityPermissionApiEvent apiEvent)
        {
            var result = new TestAutoResponseResult(request, apiEvent);
            actionResultFactory.Setup(f => f.Create(It.IsAny<HttpRequestMessage>())).Returns(result);

            using (var server = serverFactory
                .With<IHttpActionResultFactory>(actionResultFactory.Object)
                .Create())
            {
                var response = await server.HttpClient.GetAsync("/api/result");
                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            }
        }
    }
}