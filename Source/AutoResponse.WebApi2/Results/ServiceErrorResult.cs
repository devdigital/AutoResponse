namespace AutoResponse.WebApi2.Results
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class ServiceErrorResult : IHttpActionResult
    {
        private readonly HttpRequestMessage request;

        private readonly object errorDetails;

        private readonly Exception exception;

        public ServiceErrorResult(HttpRequestMessage request, ErrorDetailsApiModel errorDetails)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (errorDetails == null)
            {
                throw new ArgumentNullException(nameof(errorDetails));
            }

            this.request = request;
            this.errorDetails = errorDetails;
        }

        public ServiceErrorResult(HttpRequestMessage request, Exception exception)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            this.request = request;
            this.exception = exception;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            // TODO: CreateErrorResponse checks if error details should be included in the response
            // However, it doesn't support a custom model, but uses HttpError
            // If this result is hooked up to unhandled exceptions (which I believe it should)
            // then the Message property should include exception to string, only if error details included
            return await Task.FromResult(
                       this.request.CreateResponse(
                           HttpStatusCode.InternalServerError,
                           this.errorDetails));
        }
    }
}