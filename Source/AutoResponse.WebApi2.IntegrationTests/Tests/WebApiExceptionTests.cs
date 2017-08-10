namespace AutoResponse.WebApi2.IntegrationTests.Tests
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    using AutoResponse.Client.Models;
    using AutoResponse.Core.Exceptions;
    using AutoResponse.Core.Models;
    using AutoResponse.Sample.Domain.Models;
    using AutoResponse.Sample.Domain.Repositories;
    using AutoResponse.Sample.Domain.Services;
    using AutoResponse.WebApi2.IntegrationTests.Helpers;

    using Moq;

    using Newtonsoft.Json.Linq;

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
            ValidationErrorDetails errorDetails,
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
        public async Task PermissionExceptionShouldReturnExpectedMessage(
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
                var apiModel = response.As<ErrorApiModel>();
                Assert.NotNull(apiModel.Message);
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

        [Theory]
        [AutoCustomData]
        public async Task ExceptionWithIncludeDetailsShouldReturnDetails(
            SampleServerFactory serverFactory,
            Mock<IValuesRepository> valuesRepository,
            InvalidOperationException exception,
            Mock<ISettingsService> settingsService,
            int entityId)
        {
            settingsService.Setup(s => s.GetIncludeFullDetails()).Returns(true);
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>())).Throws(exception);

            using (var server = serverFactory
                .With<ISettingsService>(settingsService.Object)
                .With<IValuesRepository>(valuesRepository.Object)                
                .Create())
            {
                var response = await server.HttpClient.GetAsync($"/api/values/{entityId}");
                var content = await response.Content.ReadAsStringAsync();
                var jobject = JObject.Parse(content);
                Assert.NotNull(jobject.Property("exceptionMessage"));
            }
        }

        [Theory]
        [AutoCustomData]
        public async Task ExceptionWithNotIncludeDetailsShouldNotReturnDetails(
            SampleServerFactory serverFactory,
            Mock<IValuesRepository> valuesRepository,
            InvalidOperationException exception,
            Mock<ISettingsService> settingsService,
            int entityId)
        {
            settingsService.Setup(s => s.GetIncludeFullDetails()).Returns(false);
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>())).Throws(exception);

            using (var server = serverFactory
                .With<ISettingsService>(settingsService.Object)
                .With<IValuesRepository>(valuesRepository.Object)
                .Create())
            {
                var response = await server.HttpClient.GetAsync($"/api/values/{entityId}");
                var content = await response.Content.ReadAsStringAsync();
                var jobject = JObject.Parse(content);
                Assert.Null(jobject.Property("exceptionMessage"));
            }
        }
    }
}
