using AutoFixture.Xunit2;
using AutoResponse.Core.Exceptions;
using AutoResponse.Core.Models;

namespace AutoResponse.WebApi2.IntegrationTests.Tests
{
    using System.Net;
    using System.Threading.Tasks;

    using AutoResponse.Sample.Domain.Services;
    using AutoResponse.WebApi2.IntegrationTests.Helpers;

    using Moq;

    using Xunit;

    public class OwinExceptionTests
    {
        [Theory]
        [AutoData]
        public async Task UnauthenticatedExceptionShouldReturn401(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService)
        {
            exceptionService.Setup(s => s.Execute()).Throws(new UnauthenticatedException("Unauthenticated"));
            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }

        [Theory]
        [AutoData]
        public async Task EntityValidationExceptionShouldReturn422(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService)
        {
            exceptionService.Setup(s => s.Execute()).Throws(new EntityValidationException(new ValidationErrorDetails("Validation error")));
            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                Assert.Equal((HttpStatusCode)422, response.StatusCode);
            }
        }

        [Theory]
        [AutoData]
        public async Task EntityNotFoundExceptionShouldReturn404(
             SampleServerFactory serverFactory,
             Mock<IExceptionService> exceptionService,
             string entityType,
             string entityId)
        {
            exceptionService.Setup(s => s.Execute()).Throws(new EntityNotFoundException(entityType, entityId));
            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Theory]
        [AutoData]
        public async Task EntityCreatePermissionShouldReturn403(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            string userId,
            string entityType)
        {
            exceptionService.Setup(s => s.Execute()).Throws(new EntityCreatePermissionException(userId, entityType));
            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            }
        }

        [Theory]
        [AutoData]
        public async Task EntityPermissionShouldReturn403(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            string userId,
            string entityType,
            string entityId)
        {
            exceptionService.Setup(s => s.Execute()).Throws(new EntityPermissionException(userId, entityType, entityId));
            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            }
        }    
    }
}