using AutoFixture.Xunit2;

namespace AutoResponse.WebApi2.IntegrationTests.Tests.ClientTests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoResponse.Client;
    using AutoResponse.Core.Enums;
    using AutoResponse.Core.Exceptions;
    using AutoResponse.Core.Models;
    using AutoResponse.Sample.Domain.Services;
    using AutoResponse.WebApi2.IntegrationTests.Helpers;

    using FluentAssertions;

    using Moq;

    using Xunit;

    public class ClientResourceValidationTests
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
        public async Task ResourceValidationExceptionShouldReturnExpectedCode(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            string code,
            string message,
            string entityType,
            string entityId,
            string errorMessage)
        {
            var expectedErrors = new List<ValidationError> { new ValidationError(entityType, entityId, ValidationErrorCode.Invalid, errorMessage) };

            var thrownException = new EntityValidationException(new ValidationErrorDetails(message, expectedErrors));
            exceptionService.Setup(s => s.Execute()).Throws(thrownException);

            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");

                try
                {
                    await response.HandleErrors();
                }
                catch (EntityValidationException exception)
                {
                    Assert.Equal("AR422", exception.Event.Code);
                }
            }
        }

        [Theory]
        [AutoData]
        public async Task ResourceValidationExceptionShouldIncludeExpectedErrors(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            string code,
            string message,
            string errorMessage)
        {
            var expectedErrors = new List<ValidationError> { new ValidationError("EntityType", "EntityId", ValidationErrorCode.Invalid, errorMessage) };

            var thrownException = new EntityValidationException(code, new ValidationErrorDetails(message, expectedErrors));
            exceptionService.Setup(s => s.Execute()).Throws(thrownException);

            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                
                try
                {
                    await response.HandleErrors();
                }
                catch (EntityValidationException exception)
                {
                    var errors = exception.Event?.ErrorDetails?.Errors;
                    errors.ShouldBeEquivalentTo(expectedErrors);
                }
            }
        }
    }
}