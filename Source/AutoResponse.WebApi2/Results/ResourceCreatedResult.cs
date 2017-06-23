namespace AutoResponse.WebApi2.Results
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class ResourceCreatedResult : IHttpActionResult
    {
        private readonly HttpRequestMessage request;

        private readonly string id;

        public ResourceCreatedResult(HttpRequestMessage request, string id)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.request = request;
            this.id = id;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = this.request.CreateResponse(HttpStatusCode.Created);
            response.Content = new ObjectContent(
                typeof(ResourceCreatedApiModel),
                new ResourceCreatedApiModel { Id = this.id },
                this.request.GetConfiguration().Formatters.JsonFormatter,
                "application/json");

            return Task.FromResult(response);
        }
    }
}