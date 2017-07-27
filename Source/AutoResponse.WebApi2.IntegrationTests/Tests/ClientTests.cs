namespace AutoResponse.WebApi2.IntegrationTests.Tests
{
    using System.Threading.Tasks;

    using AutoResponse.Client;
    using AutoResponse.Core.Exceptions;
    using AutoResponse.Sample.Domain.Services;
    using AutoResponse.WebApi2.IntegrationTests.Helpers;

    using Moq;

    using Ploeh.AutoFixture.Xunit2;

    using Xunit;

    public class ClientTests
    {
        [Theory]
        [AutoData]
        public async Task ResourceValidationResponseShouldThrowValidationException(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            EntityValidationException exception)
        {
            exceptionService.Setup(s => s.Execute()).Throws(exception);
            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                await Assert.ThrowsAsync<EntityValidationException>(() => response.HandleErrors());
            }
        }

        [Theory]
        [AutoData]
        public async Task ResourceNotFoundResponseShouldThrowNotFoundException(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            EntityNotFoundException exception)
        {
            exceptionService.Setup(s => s.Execute()).Throws(exception);
            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                await Assert.ThrowsAsync<EntityNotFoundException>(() => response.HandleErrors());
            }
        }

        [Theory]
        [AutoData]
        public async Task ResourcePermissionResponseShouldThrowPermissionException(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            EntityPermissionException exception)
        {
            exceptionService.Setup(s => s.Execute()).Throws(exception);
            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                await Assert.ThrowsAsync<EntityPermissionException>(() => response.HandleErrors());
            }
        }

        [Theory]
        [AutoData]
        public async Task UnauthenticatedResponseShouldThrowUnauthenticatedException(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            UnauthenticatedException exception)
        {
            exceptionService.Setup(s => s.Execute()).Throws(exception);
            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                await Assert.ThrowsAsync<UnauthenticatedException>(() => response.HandleErrors());
            }
        }

        [Theory]
        [AutoData]
        public async Task ServiceErrorResponseShouldThrowServiceErrorException(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            ServiceErrorException exception)
        {
            exceptionService.Setup(s => s.Execute()).Throws(exception);
            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                await Assert.ThrowsAsync<ServiceErrorException>(() => response.HandleErrors());
            }
        }
    }
}