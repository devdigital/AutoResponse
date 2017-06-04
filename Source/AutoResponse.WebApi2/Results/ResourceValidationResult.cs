namespace AutoResponse.WebApi2.Results
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    
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
            
            var errorDetailsApiModel = new ErrorDetailsApiModel
            {
                Message = this.errorDetails.Message,
                Errors = this.errorDetails.Errors.Select(e =>
                    new ErrorApiModel
                        {
                            Resource = e.Resource,
                            Field = e.Field,
                            Code = e.Code.Kebaberize(),
                            Message = e.Message
                        }).ToList()
            };

            response.Content = new ObjectContent<ErrorDetailsApiModel>(
                errorDetailsApiModel,
                this.request.GetConfiguration().Formatters.JsonFormatter,
                "application/json");

            return Task.FromResult(response);
        }
    }

    public class ErrorApiModel
    {
        public string Resource { get; set; }

        public string Field { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }
    }

    public class ErrorDetailsApiModel
    {
        public string Message { get; set; }

        public IEnumerable<ErrorApiModel> Errors { get; set; }
    }

    public class ValidationErrorDetails
    {
        public ValidationErrorDetails(string message, IEnumerable<ValidationError> errors = null)
        {
            this.Message = message;
            this.Errors = errors;
        }

        public string Message { get; }

        public IEnumerable<ValidationError> Errors { get; set; }
    }

    public class ValidationError
    {
        public ValidationError(string resource, string field, ValidationErrorCode errorCode, string errorMessage = null)
        {
            throw new NotImplementedException();
        }

        public string Message { get; set; }

        public string Resource { get; }

        public string Field { get; set; }

        public string Code { get; set; }        
    }
}