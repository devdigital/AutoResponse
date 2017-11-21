namespace AutoResponse.WebApi2.IntegrationTests.Tests
{
    using System;
    using System.Threading.Tasks;

    using AutoResponse.Core.Exceptions;
    using AutoResponse.Core.Logging;
    using AutoResponse.Sample.Domain.Services;
    using AutoResponse.WebApi2.IntegrationTests.Helpers;

    using Moq;

    using Ploeh.AutoFixture.Xunit2;

    using Xunit;

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