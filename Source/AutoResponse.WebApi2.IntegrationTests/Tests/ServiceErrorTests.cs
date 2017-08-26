using System.Threading.Tasks;
using AutoResponse.Client;
using AutoResponse.Client.Models;
using AutoResponse.Core.Exceptions;
using AutoResponse.Sample.Domain.Services;
using AutoResponse.WebApi2.IntegrationTests.Helpers;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using WebApiTestServer;
using Xunit;

namespace AutoResponse.WebApi2.IntegrationTests.Tests
{
    public class ServiceErrorTests
    {
        [Theory]
        [AutoData]
        public async Task ServiceErrorExceptionReturnsExpectedMessage(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            string message)
        {
            exceptionService.Setup(s => s.Execute()).Throws(new ServiceErrorException(message));
            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                var apiModel = response.As<ErrorDetailsApiModel>();
                Assert.Equal(message, apiModel.Message);
            }
        }


    }
}