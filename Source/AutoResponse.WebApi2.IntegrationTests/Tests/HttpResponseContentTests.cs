namespace AutoResponse.WebApi2.IntegrationTests.Tests
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    using AutoResponse.Core.Dtos;
    using AutoResponse.Core.Exceptions;
    using AutoResponse.Sample.Domain.Exceptions;
    using AutoResponse.Sample.Domain.Services;
    using AutoResponse.WebApi2.IntegrationTests.Helpers;

    using Moq;

    using Ploeh.AutoFixture.Xunit2;

    using WebApiTestServer;

    using Xunit;

    public class HttpResponseContentTests
    {
        [Theory]
        [AutoData]
        public async Task EntityNotFoundExceptionShouldReturnResourceNotFoundApiModel(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            EntityNotFoundException exception)
        {
            exceptionService.Setup(s => s.Execute()).Throws(exception);
            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                var apiModel = response.As<ResourceNotFoundApiModel>();
                Assert.NotNull(apiModel);
            }
        }

        [Theory]
        [AutoData]
        public async Task EntityNotFoundExceptionShouldReturnExpectedCode(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            EntityNotFoundException exception)
        {
            exceptionService.Setup(s => s.Execute()).Throws(exception);
            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                var apiModel = response.As<ResourceNotFoundApiModel>();
                Assert.Equal("AR404", apiModel.Code);
            }
        }

        [Theory]
        [AutoData]
        public async Task EntityNotFoundExceptionShouldReturnResourceAsCamelCase(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            string code,
            string entityId)
        {
            exceptionService.Setup(s => s.Execute()).Throws(new EntityNotFoundException(code, "EntityType", entityId));
            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                var apiModel = response.As<ResourceNotFoundApiModel>();
                Assert.Equal("entityType", apiModel.Resource);
            }
        }

        [Theory]
        [AutoData]
        public async Task EntityNotFoundExceptionShouldReturnResourceIdAsCamelCase(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            string code,
            string entityType)
        {
            exceptionService.Setup(s => s.Execute()).Throws(new EntityNotFoundException(code, entityType, "EntityId"));
            using (var server = serverFactory.With<IExceptionService>(exceptionService.Object).Create())
            {
                var response = await server.HttpClient.GetAsync("/");
                var apiModel = response.As<ResourceNotFoundApiModel>();
                Assert.Equal("entityId", apiModel.ResourceId);
            }
        }
    }
}