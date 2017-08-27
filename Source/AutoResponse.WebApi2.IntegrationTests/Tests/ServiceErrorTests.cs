using System;
using System.Threading.Tasks;
using AutoResponse.Client.Models;
using AutoResponse.Core.Exceptions;
using AutoResponse.Sample.Domain.Repositories;
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
            string message,
            string exceptionMessage)
        {
            exceptionService.Setup(s => s.Execute()).Throws(new ServiceErrorException(message, new Exception(exceptionMessage)));
            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                var apiModel = response.As<ErrorDetailsApiModel>();
                Assert.Equal(message, apiModel.Message);
            }
        }

        [Theory]
        [AutoData]
        public async Task WithIncludeDetailsIncludesExceptionMessage(
            SampleServerFactory serverFactory,
            Mock<IValuesRepository> valuesRepository,
            string message,
            NotImplementedException exception,
            int entityId)
        {
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>()))
                .Throws(new ServiceErrorException(message, exception));

            using (var server = serverFactory
                .WithIncludeFullDetails()
                .With<IValuesRepository>(valuesRepository.Object)
                .Create())
            {
                var response = await server.HttpClient.GetAsync($"/api/values/{entityId}");
                var apiModel = response.As<ErrorWithExceptionDetailsApiModel>();
                Assert.Equal(exception.Message, apiModel.ExceptionMessage);
            }
        }

        [Theory]
        [AutoData]
        public async Task WithoutIncludeDetailsDoesNotIncludeExceptionMessage(
            SampleServerFactory serverFactory,
            Mock<IValuesRepository> valuesRepository,
            ServiceErrorException exception,
            int entityId)
        {
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>()))
                .Throws(exception);

            using (var server = serverFactory
                .With<IValuesRepository>(valuesRepository.Object)
                .Create())
            {
                var response = await server.HttpClient.GetAsync($"/api/values/{entityId}");
                var apiModel = response.As<ErrorWithExceptionDetailsApiModel>();
                Assert.Null(apiModel.ExceptionMessage);
            }
        }

        [Theory]
        [AutoData]
        public async Task WithIncludeDetailsIncludesExceptionType(
            SampleServerFactory serverFactory,
            Mock<IValuesRepository> valuesRepository,
            string message,
            NotImplementedException exception,
            int entityId)
        {
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>()))
                .Throws(new ServiceErrorException(message, exception));

            using (var server = serverFactory
                .WithIncludeFullDetails()
                .With<IValuesRepository>(valuesRepository.Object)
                .Create())
            {
                var response = await server.HttpClient.GetAsync($"/api/values/{entityId}");
                var apiModel = response.As<ErrorWithExceptionDetailsApiModel>();
                Assert.Equal(exception.GetType().FullName, apiModel.ExceptionType);
            }
        }

        [Theory]
        [AutoData]
        public async Task WithoutIncludeDetailsDoesNotIncludeExceptionType(
            SampleServerFactory serverFactory,
            Mock<IValuesRepository> valuesRepository,
            ServiceErrorException exception,
            int entityId)
        {
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>()))
                .Throws(exception);

            using (var server = serverFactory
                .With<IValuesRepository>(valuesRepository.Object)
                .Create())
            {
                var response = await server.HttpClient.GetAsync($"/api/values/{entityId}");
                var apiModel = response.As<ErrorWithExceptionDetailsApiModel>();
                Assert.Null(apiModel.ExceptionType);
            }
        }

        [Theory]
        [AutoData]
        public async Task WithIncludeDetailsIncludesExceptionString(
            SampleServerFactory serverFactory,
            Mock<IValuesRepository> valuesRepository,
            string message,
            NotImplementedException exception,
            int entityId)
        {
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>()))
                .Throws(new ServiceErrorException(message, exception));

            using (var server = serverFactory
                .WithIncludeFullDetails()
                .With<IValuesRepository>(valuesRepository.Object)
                .Create())
            {
                var response = await server.HttpClient.GetAsync($"/api/values/{entityId}");
                var apiModel = response.As<ErrorWithExceptionDetailsApiModel>();
                Assert.Equal(exception.ToString(), apiModel.ExceptionString);
            }
        }

        [Theory]
        [AutoData]
        public async Task WithoutIncludeDetailsDoesNotIncludeExceptionString(
            SampleServerFactory serverFactory,
            Mock<IValuesRepository> valuesRepository,
            ServiceErrorException exception,
            int entityId)
        {
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>()))
                .Throws(exception);

            using (var server = serverFactory
                .With<IValuesRepository>(valuesRepository.Object)
                .Create())
            {
                var response = await server.HttpClient.GetAsync($"/api/values/{entityId}");
                var apiModel = response.As<ErrorWithExceptionDetailsApiModel>();
                Assert.Null(apiModel.ExceptionString);
            }
        }
    }
}