namespace AutoResponse.WebApi2.IntegrationTests.Tests
{
    using System.Net;
    using System.Threading.Tasks;

    using AutoResponse.Sample.Domain.Exceptions;
    using AutoResponse.Sample.Domain.Repositories;
    using AutoResponse.Sample.Domain.Services;
    using AutoResponse.WebApi2.IntegrationTests.Helpers;

    using Moq;

    using Ploeh.AutoFixture.Xunit2;

    using Xunit;

    public class DomainExceptionTests
    {
        [Theory]
        [AutoData]
        public async Task DomainValidationExceptionInOwinShouldReturn422(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            DomainValidationException exception)
        {
            exceptionService.Setup(s => s.Execute()).Throws(exception);
            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                Assert.Equal((HttpStatusCode)422, response.StatusCode);
            }
        }
        
        [Theory]
        [AutoData]
        public async Task DomainValidationExceptionInWebApiShouldReturn422(
             SampleServerFactory serverFactory,
             Mock<IValuesRepository> valuesRepository,
             DomainValidationException exception,
             int entityId)
        {
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>())).Throws(exception);

            using (var server = serverFactory.With<IValuesRepository>(valuesRepository.Object).Create())
            {
                var response = await server.HttpClient.GetAsync($"/api/values/{entityId}");
                Assert.Equal((HttpStatusCode)422, response.StatusCode);
            }
        }
    }
}