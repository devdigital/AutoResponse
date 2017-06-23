namespace AutoResponse.WebApi2.Results
{
    using System.Net;
    using System.Net.Http;

    public class NotAuthenticatedResult : ErrorActionResult
    {
        public NotAuthenticatedResult(HttpRequestMessage request)
            : base(request, HttpStatusCode.Unauthorized)
        {
        }

        protected override ValidationErrorDetails GetErrorDetails()
        {
            return new ValidationErrorDetails("The user is not authenticated");
        }
    }
}