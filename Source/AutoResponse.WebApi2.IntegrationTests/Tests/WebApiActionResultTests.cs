namespace AutoResponse.WebApi2.IntegrationTests.Tests
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http.Results;

    using AutoResponse.Core.Models;
    using AutoResponse.Sample.WebApi2.Factories;
    using AutoResponse.WebApi2.IntegrationTests.Attributes;
    using AutoResponse.WebApi2.IntegrationTests.Helpers;
    using AutoResponse.WebApi2.Results;

    using Moq;

    using Ploeh.AutoFixture.Xunit2;

    using Xunit;

    public class WebApiActionResultTests
    {
        [Theory]
        [AutoMoqData]
        public async Task UnauthenticatedResultShouldReturn401(
            SampleServerFactory serverFactory,
            Mock<IHttpActionResultFactory> actionResultFactory,
            UnauthenticatedResult result)
        {
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
            ResourceNotFoundResult result)
        {
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
           string message)
        {
            actionResultFactory.Setup(f => f.Create(It.IsAny<HttpRequestMessage>())).Returns(new ResourceValidationResult(request, new ValidationErrorDetails(message)));

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
            ResourceCreatePermissionResult result)
        {
            actionResultFactory.Setup(f => f.Create(It.IsAny<HttpRequestMessage>())).Returns(result);

            using (var server = serverFactory.With<IHttpActionResultFactory>(actionResultFactory.Object).Create())
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
            ResourcePermissionResult result)
        {
            actionResultFactory.Setup(f => f.Create(It.IsAny<HttpRequestMessage>())).Returns(result);

            using (var server = serverFactory.With<IHttpActionResultFactory>(actionResultFactory.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/api/result");
                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            }
        }

    }
}