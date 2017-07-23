namespace AutoResponse.Core.Responses
{
    using System.Net;

    public class ResourcePermissionHttpResponse : ErrorHttpResponse
    {
        public ResourcePermissionHttpResponse(string message, string code)
            : base(message, code, HttpStatusCode.Forbidden)
        {
        }
    }
}