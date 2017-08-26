using System;
using System.Threading.Tasks;
using AutoResponse.Client;
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
    public class UnhandledExceptionTests
    {
        [Theory]
        [AutoData]
        public async Task UnhandledExceptionDoesNotExposeMessage(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            NotImplementedException exception)
        {
            exceptionService.Setup(s => s.Execute()).Throws(exception);
            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                var apiModel = response.As<ErrorApiModel>();
                Assert.Equal("A service error has occurred.", apiModel.Message);
            }
        }

        [Theory]
        [AutoData]
        public async Task UnhandledExceptionWithIncludeDetailsIncludesExceptionMessage(
            SampleServerFactory serverFactory,
            Mock<IValuesRepository> valuesRepository,
            NotImplementedException exception,
            int entityId)
        {
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>()))
                .Throws(exception);

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
        public async Task UnhandledExceptionWithoutIncludeDetailsDoesNotIncludeExceptionMessage(
            SampleServerFactory serverFactory,
            Mock<IValuesRepository> valuesRepository,
            NotImplementedException exception,
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
        public async Task UnhandledExceptionWithIncludeDetailsIncludesExceptionType(
            SampleServerFactory serverFactory,
            Mock<IValuesRepository> valuesRepository,
            NotImplementedException exception,
            int entityId)
        {
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>()))
                .Throws(exception);

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
        public async Task UnhandledExceptionWithoutIncludeDetailsDoesNotIncludeExceptionType(
            SampleServerFactory serverFactory,
            Mock<IValuesRepository> valuesRepository,
            NotImplementedException exception,
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
        public async Task UnhandledExceptionWithIncludeDetailsIncludesExceptionString(
            SampleServerFactory serverFactory,
            Mock<IValuesRepository> valuesRepository,
            NotImplementedException exception,
            int entityId)
        {
            valuesRepository.Setup(r => r.GetValue(It.IsAny<int>()))
                .Throws(exception);

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
        public async Task UnhandledExceptionWithoutIncludeDetailsDoesNotIncludeExceptionString(
            SampleServerFactory serverFactory,
            Mock<IValuesRepository> valuesRepository,
            NotImplementedException exception,
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
