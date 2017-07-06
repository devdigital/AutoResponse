namespace AutoResponse.WebApi2.Results
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoResponse.Core.Models;

    using Humanizer;

    public class ResourceValidationResult : IHttpActionResult
    {
        private readonly HttpRequestMessage request;

        private readonly ValidationErrorDetails errorDetails;        

        public ResourceValidationResult(HttpRequestMessage request, ValidationErrorDetails errorDetails)
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

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = this.request.CreateResponse((HttpStatusCode)422);
            
            var errorDetailsApiModel = new ErrorDetailsApiModel<ValidationErrorApiModel>
            {
                Message = this.errorDetails.Message,
                Errors = this.errorDetails.Errors.Select(e =>
                    new ValidationErrorApiModel
                        {
                            Resource = e.Resource,
                            Field = e.Field,
                            Code = e.Code.ToString().Kebaberize(),
                            Message = e.Message
                        }).ToList()
            };

            response.Content = new ObjectContent<ErrorDetailsApiModel<ValidationErrorApiModel>>(
                errorDetailsApiModel,
                this.request.GetConfiguration().Formatters.JsonFormatter,
                "application/json");

            return Task.FromResult(response);
        }
    }
}