namespace AutoResponse.WebApi2.IntegrationTests.Tests
{
    using System.Net;
    using System.Threading.Tasks;

    using AutoResponse.Client;
    using AutoResponse.Core.Errors;
    using AutoResponse.Core.Exceptions;
    using AutoResponse.Sample.Domain.Models;
    using AutoResponse.Sample.Domain.Repositories;
    using AutoResponse.WebApi2.IntegrationTests.Helpers;

    using Moq;

    using Ploeh.AutoFixture.Xunit2;

    using WebApiTestServer;

    using Xunit;

    public class WebApiExceptionTests
    {
        [Theory]
        [AutoData]
        public async Task UnauthenticatedExceptionShouldReturn401(
           SampleServerFactory serverFactory,
           Mock<IValuesRepository> valuesRepository,
           string entityType,
           int entityId)
        {
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>()))
                .Throws(new UnauthenticatedException("The user is not authenticated"));

            using (var server = serverFactory.With<IValuesRepository>(valuesRepository.Object).Create())
            {
                var response = await server.HttpClient.GetAsync($"/api/values/{entityId}");
                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }

        [Theory]
        [AutoData]
        public async Task NotFoundExceptionShouldReturn404(
           SampleServerFactory serverFactory,
           Mock<IValuesRepository> valuesRepository,
           string entityType,
           int entityId)
        {
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>()))
                .Throws(new EntityNotFoundException(entityType, entityId.ToString()));

            using (var server = serverFactory.With<IValuesRepository>(valuesRepository.Object).Create())
            {
                var response = await server.HttpClient.GetAsync($"/api/values/{entityId}");
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Theory]
        [AutoData]
        public async Task NotFoundExceptionGenericShouldReturn404(
            SampleServerFactory serverFactory, 
            Mock<IValuesRepository> valuesRepository,
            int entityId)
        {
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>()))
                .Throws(new EntityNotFoundException<Value>(entityId.ToString()));

            using (var server = serverFactory.With<IValuesRepository>(valuesRepository.Object).Create())
            {
                var response = await server.HttpClient.GetAsync($"/api/values/{entityId}");
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Theory]
        [AutoCustomData]
        public async Task ValidationExceptionShouldReturn422(
            SampleServerFactory serverFactory,
            Mock<IValuesRepository> valuesRepository,
            EntityValidationErrorDetails errorDetails,
            int entityId)
        {
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>()))
                .Throws(new EntityValidationException(errorDetails));

            using (var server = serverFactory.With<IValuesRepository>(valuesRepository.Object).Create())
            {
                var response = await server.HttpClient.GetAsync($"/api/values/{entityId}");
                Assert.Equal((HttpStatusCode)422, response.StatusCode);
            }
        }

        [Theory]
        [AutoCustomData]
        public async Task PermissionExceptionShouldReturn403(
            SampleServerFactory serverFactory,
            Mock<IValuesRepository> valuesRepository,
            string userId,
            string entityType,
            int entityId)
        {
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>()))
                .Throws(new EntityPermissionException(userId, entityType, entityId.ToString()));

            using (var server = serverFactory.With<IValuesRepository>(valuesRepository.Object).Create())
            {
                var response = await server.HttpClient.GetAsync($"/api/values/{entityId}");
                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            }
        }

        [Theory]
        [AutoCustomData]
        public async Task PermissionExceptionWithMessageShouldReturnMessage(
            SampleServerFactory serverFactory,
            Mock<IValuesRepository> valuesRepository,
            int entityId,
            string message)
        {
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>()))
                .Throws(new EntityPermissionException(message));

            using (var server = serverFactory.With<IValuesRepository>(valuesRepository.Object).Create())
            {
                var response = await server.HttpClient.GetAsync($"/api/values/{entityId}");
                var apiModel = response.As<ErrorApiModel>();
                Assert.Equal(message, apiModel.Message);
            }
        }

        [Theory]
        [AutoCustomData]
        public async Task PermissionExceptionGenericShouldReturn403(
            SampleServerFactory serverFactory,
            Mock<IValuesRepository> valuesRepository,
            string userId,
            int entityId)
        {
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>()))
                .Throws(new EntityPermissionException<Value>(userId, entityId.ToString()));

            using (var server = serverFactory.With<IValuesRepository>(valuesRepository.Object).Create())
            {
                var response = await server.HttpClient.GetAsync($"/api/values/{entityId}");
                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            }
        }

        [Theory]
        [AutoCustomData]
        public async Task CreatePermissionExceptionShouldReturn403(
            SampleServerFactory serverFactory,
            Mock<IValuesRepository> valuesRepository,
            string userId,
            string entityType,
            int entityId)
        {
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>()))
                .Throws(new EntityCreatePermissionException(userId, entityType, entityId.ToString()));

            using (var server = serverFactory.With<IValuesRepository>(valuesRepository.Object).Create())
            {
                var response = await server.HttpClient.GetAsync($"/api/values/{entityId}");
                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            }
        }

        [Theory]
        [AutoCustomData]
        public async Task CreatePermissionExceptionGenericShouldReturn403(
            SampleServerFactory serverFactory,
            Mock<IValuesRepository> valuesRepository,
            string userId,
            int entityId)
        {
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>()))
                .Throws(new EntityCreatePermissionException<Value>(userId, entityId.ToString()));

            using (var server = serverFactory.With<IValuesRepository>(valuesRepository.Object).Create())
            {
                var response = await server.HttpClient.GetAsync($"/api/values/{entityId}");
                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            }
        }
    }
}
