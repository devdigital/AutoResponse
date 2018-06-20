// <copyright file="DomainExceptionTests.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.IntegrationTests.Tests
{
    using System.Net;
    using System.Threading.Tasks;
    using AutoFixture.Xunit2;
    using AutoResponse.Sample.Domain.Exceptions;
    using AutoResponse.Sample.Domain.Repositories;
    using AutoResponse.Sample.Domain.Services;
    using AutoResponse.WebApi2.IntegrationTests.Helpers;
    using Moq;
    using Xunit;

    // ReSharper disable StyleCop.SA1600
    #pragma warning disable SA1600
    #pragma warning disable 1591

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