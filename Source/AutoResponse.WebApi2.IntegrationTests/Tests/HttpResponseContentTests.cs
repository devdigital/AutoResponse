// <copyright file="HttpResponseContentTests.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.IntegrationTests.Tests
{
    using System.Threading.Tasks;
    using AutoFixture.Xunit2;
    using AutoResponse.Core.Dtos;
    using AutoResponse.Core.Exceptions;
    using AutoResponse.Sample.Domain.Services;
    using AutoResponse.WebApi2.IntegrationTests.Helpers;
    using Moq;
    using WebApiTestServer;
    using Xunit;

    // ReSharper disable StyleCop.SA1600
    #pragma warning disable SA1600
    #pragma warning disable 1591

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