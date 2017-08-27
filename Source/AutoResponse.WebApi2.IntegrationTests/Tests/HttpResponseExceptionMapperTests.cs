namespace AutoResponse.WebApi2.IntegrationTests.Tests
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    using AutoResponse.Client;
    using AutoResponse.Core.Dtos;
    using AutoResponse.Core.Exceptions;
    using AutoResponse.WebApi2.IntegrationTests.Builders;
    using AutoResponse.WebApi2.IntegrationTests.Models;

    using Ploeh.AutoFixture.Xunit2;

    using Xunit;

    public class HttpResponseExceptionMapperTests
    {
        [Theory]
        [AutoData]
        public async Task AddMappingGetExceptionReturnsExpectedException(
            EntityNotFoundException expectedException,
            HttpResponseMessageBuilder builder,
            ResourceNotFoundApiModel apiModel)
        {
            var addMappings = new Action<HttpResponseExceptionConfiguration>(
                configuration =>
                {
                    configuration.AddMapping(
                        HttpStatusCode.NotFound,
                        (r, c) => expectedException);
                });

            var mapper = new TestHttpResponseExceptionMapper(addMappings);
  
            var httpResponseMessage = builder
                .WithStatusCode(HttpStatusCode.NotFound)
                .WithJson(apiModel)
                .Build();

            var exception = await mapper.GetException(httpResponseMessage);
            Assert.Equal(expectedException, exception);
        }

        [Theory]
        [AutoData]
        public async Task AddMappingDuplicateThrowsInvalidOperationException(
            EntityNotFoundException expectedException,
            HttpResponseMessageBuilder builder,
            ResourceNotFoundApiModel apiModel)
        {
            var addMappings = new Action<HttpResponseExceptionConfiguration>(
                configuration =>
                {
                    configuration.AddMapping(
                        HttpStatusCode.NotFound,
                        (r, c) => expectedException);

                    configuration.AddMapping(
                        HttpStatusCode.NotFound,
                        (r, c) => expectedException);
                });

            var mapper = new TestHttpResponseExceptionMapper(addMappings);

            var httpResponseMessage = builder
                .WithStatusCode(HttpStatusCode.NotFound)
                .WithJson(apiModel)
                .Build();

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => mapper.GetException(httpResponseMessage));
        }

        [Theory]
        [AutoData]
        public async Task UpdateMappingUpdatesException(
            EntityNotFoundException expectedException,
            EntityNotFoundException exceptionReplacement,
            HttpResponseMessageBuilder builder,
            ResourceNotFoundApiModel apiModel)
        {
            var addMappings = new Action<HttpResponseExceptionConfiguration>(
                configuration =>
                {
                    configuration.AddMapping(
                        HttpStatusCode.NotFound,
                        (r, c) => expectedException);

                    configuration.UpdateMapping(
                        HttpStatusCode.NotFound,
                        (r, c) => exceptionReplacement);
                });

            var mapper = new TestHttpResponseExceptionMapper(addMappings);

            var httpResponseMessage = builder
                .WithStatusCode(HttpStatusCode.NotFound)
                .WithJson(apiModel)
                .Build();

            var exception = await mapper.GetException(httpResponseMessage);
            Assert.Equal(exceptionReplacement, exception);
        }

        [Theory]
        [AutoData]
        public async Task UpdateMappingDoesNotExistThrowsNotImplementedException(
            EntityNotFoundException exception,
            HttpResponseMessageBuilder builder,
            ResourceNotFoundApiModel apiModel)
        {
            var addMappings = new Action<HttpResponseExceptionConfiguration>(
                configuration =>
                {                    
                    configuration.UpdateMapping(
                        HttpStatusCode.NotFound,
                        (r, c) => exception);
                });

            var mapper = new TestHttpResponseExceptionMapper(addMappings);

            var httpResponseMessage = builder
                .WithStatusCode(HttpStatusCode.NotFound)
                .WithJson(apiModel)
                .Build();

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => mapper.GetException(httpResponseMessage));
        }

        [Theory]
        [AutoData]
        public async Task RemoveMappingRemovesTheExceptionAndReturnsDefaultException(
            EntityNotFoundException expectedException,
            HttpResponseMessageBuilder builder,
            ResourceNotFoundApiModel apiModel)
        {
            var addMappings = new Action<HttpResponseExceptionConfiguration>(
                configuration =>
                {
                    configuration.AddMapping(
                        HttpStatusCode.NotFound,
                        (r, c) => expectedException);

                    configuration.RemoveMapping(HttpStatusCode.NotFound);
                });

            var mapper = new TestHttpResponseExceptionMapper(addMappings);

            var httpResponseMessage = builder
                .WithStatusCode(HttpStatusCode.NotFound)
                .WithJson(apiModel)
                .Build();

            var exception = await mapper.GetException(httpResponseMessage);
            Assert.Equal(mapper.DefaultException, exception);
        }

        [Theory]
        [AutoData]
        public async Task RemoveMappingThatDoesNotExistThrowsNotImplementedException(
           EntityNotFoundException expectedException,
           HttpResponseMessageBuilder builder,
           ResourceNotFoundApiModel apiModel)
        {
            var addMappings = new Action<HttpResponseExceptionConfiguration>(
                configuration =>
                {
                    configuration.RemoveMapping(HttpStatusCode.NotFound);
                });

            var mapper = new TestHttpResponseExceptionMapper(addMappings);

            var httpResponseMessage = builder
                .WithStatusCode(HttpStatusCode.NotFound)
                .WithJson(apiModel)
                .Build();

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => mapper.GetException(httpResponseMessage));
        }
    }
}