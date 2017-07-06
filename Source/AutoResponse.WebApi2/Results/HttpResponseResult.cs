namespace AutoResponse.WebApi2.Results
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoResponse.Core;
    using AutoResponse.Core.Responses;

    public class HttpResponseResult : IHttpActionResult
    {
        private readonly HttpRequestMessage request;

        private readonly IHttpResponse httpResponse;

        public HttpResponseResult(HttpRequestMessage request, IHttpResponse httpResponse)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (httpResponse == null)
            {
                throw new ArgumentNullException(nameof(httpResponse));
            }

            this.request = request;
            this.httpResponse = httpResponse;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = this.request.CreateResponse(this.httpResponse.StatusCode);

            // TODO: review header collection value
            foreach (var keyValuePair in this.httpResponse.Headers)
            {
                response.Headers.Add(keyValuePair.Key, keyValuePair.Value);
            }

            // TODO: formatter based on content type
            response.Content = new ObjectContent(
                this.httpResponse.DataType, 
                this.httpResponse.Data,                                
                this.request.GetConfiguration().Formatters.JsonFormatter,
                "application/json");

            return Task.FromResult(response);
        }
    }
}