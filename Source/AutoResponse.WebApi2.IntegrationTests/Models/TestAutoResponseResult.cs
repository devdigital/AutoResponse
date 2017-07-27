namespace AutoResponse.WebApi2.IntegrationTests.Models
{
    using System.Net.Http;

    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Core.Mappers;
    using AutoResponse.WebApi2.ExceptionHandling;
    using AutoResponse.WebApi2.Results;

    public class TestAutoResponseResult : AutoResponseResult
    {
        public TestAutoResponseResult(HttpRequestMessage request, AutoResponseApiEvent apiEvent)
            : base(request, apiEvent)
        {
        }

        protected override IApiEventHttpResponseMapper GetMapper()
        {
            return new AutoResponseApiEventHttpResponseMapper(
                       new WebApiContextResolver());
        }
    }
}