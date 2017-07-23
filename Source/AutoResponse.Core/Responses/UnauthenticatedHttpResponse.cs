namespace AutoResponse.Core.Responses
{
    using System.Net;

    public class UnauthenticatedHttpResponse : ErrorHttpResponse
    {
        public UnauthenticatedHttpResponse(string message, string code)
            : base(message, code, HttpStatusCode.Unauthorized)
        {
        }
    }
}