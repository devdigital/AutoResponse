namespace AutoResponse.WebApi2.Results
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Core.Mappers;

    public class AutoResponseResult : IHttpActionResult 
    {
        private readonly HttpRequestMessage request;

        private readonly AutoResponseApiEvent apiEvent;

        public AutoResponseResult(HttpRequestMessage request, AutoResponseApiEvent apiEvent)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (apiEvent == null)
            {
                throw new ArgumentNullException(nameof(apiEvent));
            }

            this.request = request;
            this.apiEvent = apiEvent;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var mapper = this.GetMapper();
            if (mapper == null)
            {
                throw new InvalidOperationException($"No {typeof(IApiEventHttpResponseMapper).Name} registered in dependency resolver");
            }

            var httpResponse = mapper.GetHttpResponse(this.request, this.apiEvent);
            if (httpResponse == null)
            {
                throw new ArgumentNullException($"No http response found for api event type {this.apiEvent.GetType().Name}");
            }

            var response = this.request.CreateResponse(httpResponse.StatusCode);

            foreach (var keyValuePair in httpResponse.Headers)
            {
                response.Headers.Add(keyValuePair.Key, keyValuePair.Value);
            }

            response.Content = new StringContent(
                httpResponse.Content,
                httpResponse.Encoding,
                httpResponse.ContentType);

            return Task.FromResult(response);
        }

        protected virtual IApiEventHttpResponseMapper GetMapper()
        {
            var dependencyScope = this.request.GetDependencyScope();

            var mapper = dependencyScope?.GetService(typeof(IApiEventHttpResponseMapper)) as
                IApiEventHttpResponseMapper;

            return mapper;
        }
    }
}