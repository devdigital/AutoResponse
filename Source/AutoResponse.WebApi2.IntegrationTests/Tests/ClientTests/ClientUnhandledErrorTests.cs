using System;
using System.Net;
using System.Threading.Tasks;
using AutoResponse.Client;
using AutoResponse.Core.Exceptions;
using AutoResponse.Sample.Domain.Repositories;
using AutoResponse.Sample.Domain.Services;
using AutoResponse.WebApi2.IntegrationTests.Builders;
using AutoResponse.WebApi2.IntegrationTests.Helpers;
using FluentAssertions;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace AutoResponse.WebApi2.IntegrationTests.Tests.ClientTests
{
    public class ClientUnhandledErrorTests
    {
        [Theory]
        [AutoData]
        public async Task ErrorDetailsOffShouldThrowExceptionWithMessageProperty(
            SampleServerFactory serverFactory,
            Mock<IValuesRepository> valuesRepository,
            string code,
            string message,
            Exception innerException,
            int entityId)
        {
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>()))
                .Throws(new ServiceErrorException(code, message, innerException));

            using (var server = serverFactory
                .With<IValuesRepository>(valuesRepository.Object)
                .Create())
            {
                var response = await server.HttpClient.GetAsync($"/api/values/{entityId}");

                try
                {
                    await response.HandleErrors();
                }
                catch (Exception exception)
                {
                    Assert.Equal(message, exception.Message);
                }
            }
        }

        [Theory]
        [AutoData]
        public async Task ErrorDetailsOnShouldThrowExceptionWithExceptionMessageProperty(
            SampleServerFactory serverFactory,
            Mock<IValuesRepository> valuesRepository,
            string code,
            string message,
            Exception innerException,
            int entityId)
        {
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>()))
                .Throws(new ServiceErrorException(code, message, innerException));
            
            using (var server = serverFactory
                .WithIncludeFullDetails()
                .With<IValuesRepository>(valuesRepository.Object)
                .Create())
            {
                var response = await server.HttpClient.GetAsync($"/api/values/{entityId}");

                try
                {
                    await response.HandleErrors();
                }
                catch (Exception exception)
                {
                    Assert.Equal(innerException.Message, exception.Message);
                }
            }
        }

        [Theory]
        [AutoData]
        public async Task NoMessageResultsInDefaultExceptionMessage(
            HttpResponseMessageBuilder responseMessageBuilder)
        {
            var response = responseMessageBuilder
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .Build();

            try
            {
                await response.HandleErrors();
            }
            catch (Exception exception)
            {
                Assert.Equal(
                    $"There was an API error response with status code {HttpStatusCode.InternalServerError}.",
                    exception.Message);
            }
        }
    }
}