namespace AutoResponse.WebApi2.IntegrationTests.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using AutoResponse.Client.Models;
    using AutoResponse.Core.Enums;
    using AutoResponse.Core.Exceptions;
    using AutoResponse.Core.Models;
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
            exceptionService.Setup(s => s.Execute()).Throws(new EntityValidationException(new ValidationErrorDetails(
                message, new ValidationError<User>(u => u.Id, ValidationErrorCode.Invalid))));

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
            string message)
        {
            exceptionService.Setup(s => s.Execute()).Throws(new EntityValidationException(new ValidationErrorDetails(
                message, new ValidationError<User>(u => u.UserName, ValidationErrorCode.Invalid))));

            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                var apiModel = response.As<ErrorDetailsApiModel<ValidationErrorApiModel>>();
                var error = apiModel.Errors.First();
                Assert.Equal("user-name", error.Field);
            }
        }

        [Theory]
        [AutoData]
        public async Task EntityWithDtoPostFixShouldReturnKebabCaseWithPostFixRemoved(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            string userId,
            string entityId,
            string message)
        {
            exceptionService.Setup(s => s.Execute()).Throws(new EntityValidationException(new ValidationErrorDetails(
                message, new ValidationError<UserDto>(u => u.Id, ValidationErrorCode.Invalid))));

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
        public async Task EntityWithApiModelPostFixShouldReturnKebabCaseWithPostFixRemoved(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            string userId,
            string entityId,
            string message)
        {
            exceptionService.Setup(s => s.Execute()).Throws(new EntityValidationException(new ValidationErrorDetails(
                message, new ValidationError<UserApiModel>(u => u.Id, ValidationErrorCode.Invalid))));

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
        public async Task EntityWithPostFixNameShouldReturnKebabCaseEntityType(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            string userId,
            string entityId,
            string message)
        {
            exceptionService.Setup(s => s.Execute()).Throws(new EntityValidationException(new ValidationErrorDetails(
                message, new ValidationError<ApiModel>(u => u.Id, ValidationErrorCode.Invalid))));

            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                var apiModel = response.As<ErrorDetailsApiModel<ValidationErrorApiModel>>();
                var error = apiModel.Errors.First();
                Assert.Equal("api-model", error.Resource);
            }
        }
    }
}