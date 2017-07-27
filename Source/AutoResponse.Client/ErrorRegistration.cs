namespace AutoResponse.Client
{
    using System.Net;

    public class ErrorRegistration
    {
        public ErrorRegistration(HttpStatusCode statusCode, string errorCode)
        {
            this.StatusCode = statusCode;
            this.ErrorCode = errorCode;
        }

        public ErrorRegistration(HttpStatusCode statusCode) : this(statusCode, null)
        {            
        }

        public HttpStatusCode StatusCode { get; }

        public string ErrorCode { get; }
    }
}