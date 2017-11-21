using AutoFixture.Xunit2;
using AutoResponse.Client;

namespace AutoResponse.WebApi2.IntegrationTests.Tests.ClientTests
{
    using System.Threading.Tasks;

    using AutoResponse.Client;
    using AutoResponse.Core.Exceptions;
    using AutoResponse.Sample.Domain.Services;
    using AutoResponse.WebApi2.IntegrationTests.Helpers;

    using Moq;

    using Xunit;

    public class ClientResourceNotFoundTests
    {
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
    }
}