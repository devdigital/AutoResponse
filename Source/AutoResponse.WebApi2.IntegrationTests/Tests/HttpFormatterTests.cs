namespace AutoResponse.WebApi2.IntegrationTests.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoResponse.Client;
    using AutoResponse.Core.Errors;
    using AutoResponse.Core.Exceptions;
    using AutoResponse.Sample.Domain.Services;
    using AutoResponse.WebApi2.IntegrationTests.Helpers;
    using AutoResponse.WebApi2.IntegrationTests.Models;

    using Moq;

    using Ploeh.AutoFixture.Xunit2;

    using WebApiTestServer;

    using Xunit;

    public class HttpFormatterTests
    {
        [Theory]
        [AutoData]
        public async Task EntityValidationExceptionShouldReturnKebabCaseEntityType(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            string userId,
            string entityId,
            string message)
        {
            exceptionService.Setup(s => s.Execute()).Throws(new EntityValidationException(new EntityValidationErrorDetails(
                message, new EntityValidationError<User, Guid>(u => u.Id, EntityValidationErrorCode.Invalid))));

            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                var apiModel = response.As<ErrorDetailsApiModel<ValidationErrorApiModel>>();
                var error = apiModel.Errors.First();
                Assert.Equal("user", error.Resource);
            }
        }

        [Theory]
        [AutoData]
        public async Task EntityValidationExceptionShouldReturnKebabCaseEntityProperty(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            string userId,
            string entityId,
            string message)
        {
            exceptionService.Setup(s => s.Execute()).Throws(new EntityValidationException(new EntityValidationErrorDetails(
                message, new EntityValidationError<User, string>(u => u.UserName, EntityValidationErrorCode.Invalid))));

            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                var apiModel = response.As<ErrorDetailsApiModel<ValidationErrorApiModel>>();
                var error = apiModel.Errors.First();
                Assert.Equal("user-name", error.Field);
            }
        }
    }
}