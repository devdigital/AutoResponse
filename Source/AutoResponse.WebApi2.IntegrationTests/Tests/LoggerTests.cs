// <copyright file="LoggerTests.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.IntegrationTests.Tests
{
    using System;
    using System.Threading.Tasks;
    using AutoFixture.Xunit2;
    using AutoResponse.Core.Exceptions;
    using AutoResponse.Core.Logging;
    using AutoResponse.Sample.Domain.Services;
    using AutoResponse.WebApi2.IntegrationTests.Helpers;
    using Moq;
    using Xunit;

    // ReSharper disable StyleCop.SA1600
    #pragma warning disable SA1600
    #pragma warning disable 1591

    public class LoggerTests
    {
        [Theory]
        [AutoData]
        public async Task OwinExceptionShouldInvokeLogger(
            SampleServerFactory serverFactory,
            Mock<IExceptionService> exceptionService,
            EntityValidationException exception,
            Mock<IAutoResponseLogger> logger)
        {
            logger.Setup(l => l.LogException(It.IsAny<Exception>())).Returns(Task.FromResult(0));
            exceptionService.Setup(s => s.Execute()).Throws(exception);

            using (var server = serverFactory
                .With<IExceptionService>(exceptionService.Object)
                .With<IAutoResponseLogger>(logger.Object)
                .Create())
            {
                await server.HttpClient.GetAsync("/");
                logger.Verify(l => l.LogException(It.IsAny<Exception>()), Times.Once());
            }
        }
    }
}