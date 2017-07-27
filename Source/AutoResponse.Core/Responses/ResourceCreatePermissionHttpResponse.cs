namespace AutoResponse.Core.Responses
{
    using System.Net;

    public class ResourceCreatePermissionHttpResponse : ErrorHttpResponse
    {
        public ResourceCreatePermissionHttpResponse(string message, string code)
          : base(message, code, HttpStatusCode.Forbidden)
        {            
        }
    }
}