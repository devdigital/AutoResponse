namespace AutoResponse.WebApi2.IntegrationTests.Tests
{
    using System.Threading.Tasks;

    using AutoResponse.Client;
    using AutoResponse.Core.Enums;
    using AutoResponse.Core.Exceptions;
    using AutoResponse.Core.Models;
    using AutoResponse.Sample.Domain.Services;
    using AutoResponse.WebApi2.IntegrationTests.Helpers;
    using AutoResponse.WebApi2.IntegrationTests.Models;

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
                await Assert.ThrowsAsync<EntityValidationException>(() => response.HandleErrors());
            }
        }
    }
}