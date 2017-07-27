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
        [Theory(Skip = "Not implemented")]
        [AutoData]
        public async Task EntityValidationExceptionShouldReturnKebabCaseEntityType(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            string userId,
            string entityId,
            string message)
        {
            exceptionService.Setup(s => s.Execute())
                .Throws(
                    new EntityValidationException(
                        new ValidationErrorDetails(
                            message,
                            new ValidationError<User>(u => u.Id, ValidationErrorCode.Invalid))));

            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                Assert.Throws<EntityValidationException>(() => response.HandleErrors());
            }
        }
    }
}