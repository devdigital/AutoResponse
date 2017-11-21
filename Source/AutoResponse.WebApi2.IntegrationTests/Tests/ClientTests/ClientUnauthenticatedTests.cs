namespace AutoResponse.WebApi2.IntegrationTests.Tests.ClientTests
{
    using System.Threading.Tasks;

    using AutoResponse.Client;
    using AutoResponse.Core.Exceptions;
    using AutoResponse.Sample.Domain.Services;
    using AutoResponse.WebApi2.IntegrationTests.Helpers;

    using Moq;

    using Ploeh.AutoFixture.Xunit2;

    using Xunit;

    public class ClientUnauthenticatedTests
    {
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
    }
}
