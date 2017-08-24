namespace AutoResponse.WebApi2.IntegrationTests.Tests
{
    using System;
    using System.Threading.Tasks;

    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Core.Mappers;
    using AutoResponse.Core.Responses;
    using AutoResponse.WebApi2.IntegrationTests.Models;

    using Ploeh.AutoFixture.Xunit2;

    using Xunit;

    public class ApiEventHttpResponseMapperTests
    {
        [Theory]
        [AutoData]
        public async Task AddMappingGetResponseReturnsExpectedResponse(
            ServiceErrorApiEvent apiEvent,
            ServiceErrorHttpResponse httpResponse)
        {
            var addMappings = new Action<ExceptionHttpResponseConfiguration>(
                configuration =>
                    {
                        configuration.AddMapping<ServiceErrorApiEvent>(
                            (e, c) => httpResponse);                        
                    });

            var mapper = new TestApiEventHttpResponseMapper(addMappings);
            var response = mapper.GetHttpResponse(null, apiEvent);
            Assert.Equal(httpResponse, response);
        }

        [Theory]
        [AutoData]
        public async Task DuplicateApiEventThrowsInvalidOperationException(
            ServiceErrorApiEvent apiEvent,
            ServiceErrorHttpResponse httpResponse)
        {
            var addMappings = new Action<ExceptionHttpResponseConfiguration>(
                configuration =>
                {
                    configuration.AddMapping<ServiceErrorApiEvent>(
                        (e, c) => httpResponse);

                    configuration.AddMapping<ServiceErrorApiEvent>(
                        (e, c) => httpResponse);
                });

            Assert.Throws<InvalidOperationException>(
                () =>
                {
                    var mapper = new TestApiEventHttpResponseMapper(addMappings);
                    mapper.GetHttpResponse(null, apiEvent);
                });
        }

        [Theory]
        [AutoData]
        public async Task UpdateMappingUpdatesTheResponse(
            ServiceErrorApiEvent apiEvent,
            ServiceErrorHttpResponse httpResponse,
            ServiceErrorHttpResponse httpResponseReplacement)
        {
            var addMappings = new Action<ExceptionHttpResponseConfiguration>(
                configuration =>
                {
                    configuration.AddMapping<ServiceErrorApiEvent>(
                        (e, c) => httpResponse);

                    configuration.UpdateMapping<ServiceErrorApiEvent>(
                        (e, c) => httpResponseReplacement);
                });

            var mapper = new TestApiEventHttpResponseMapper(addMappings);
            var response = mapper.GetHttpResponse(null, apiEvent);
            Assert.Equal(httpResponseReplacement, response);
        }

        [Theory]
        [AutoData]
        public async Task UpdateMappingThatDoesNotExistThrowsInvalidOperationException(
            ServiceErrorApiEvent apiEvent,
            ServiceErrorHttpResponse httpResponse)
        {
            var addMappings = new Action<ExceptionHttpResponseConfiguration>(
                configuration =>
                {
                    configuration.UpdateMapping<ServiceErrorApiEvent>(
                        (e, c) => httpResponse);
                });

            Assert.Throws<InvalidOperationException>(
                () =>
                {
                    var mapper = new TestApiEventHttpResponseMapper(addMappings);
                    mapper.GetHttpResponse(null, apiEvent);
                });
        }

        [Theory]
        [AutoData]
        public async Task RemoveMappingRemovesTheMapping(
            ServiceErrorApiEvent apiEvent,
            ServiceErrorHttpResponse httpResponse)
        {
            var addMappings = new Action<ExceptionHttpResponseConfiguration>(
                configuration =>
                {
                    configuration.AddMapping<ServiceErrorApiEvent>(
                        (e, c) => httpResponse);

                    configuration.RemoveMapping<ServiceErrorApiEvent>();
                });

            var mapper = new TestApiEventHttpResponseMapper(addMappings);
            var response = mapper.GetHttpResponse(null, apiEvent);
            Assert.Null(response);
        }

        [Theory]
        [AutoData]
        public async Task RemoveMappingThatDoesNotExistThrowsInvalidOperationException(
            ServiceErrorApiEvent apiEvent)
        {
            var addMappings = new Action<ExceptionHttpResponseConfiguration>(
                configuration =>
                {
                    configuration.RemoveMapping<ServiceErrorApiEvent>();
                });

            Assert.Throws<InvalidOperationException>(
                () =>
                {
                    var mapper = new TestApiEventHttpResponseMapper(addMappings);
                    mapper.GetHttpResponse(null, apiEvent);
                });
        }
    }
}
