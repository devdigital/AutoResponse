namespace AutoResponse.Core.Responses
{
    using System.Net;

    public class ServiceErrorHttpResponse : ErrorHttpResponse
    {
        public ServiceErrorHttpResponse(string message, string code)
            : base(message, code, HttpStatusCode.InternalServerError)
        {
        }        
    }
}