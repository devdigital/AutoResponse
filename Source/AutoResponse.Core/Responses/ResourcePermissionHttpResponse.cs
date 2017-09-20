namespace AutoResponse.Core.Responses
{
    using System.Net;

    using AutoResponse.Core.Dtos;

    public class ResourcePermissionHttpResponse : JsonHttpResponse<ErrorApiModel>
    {
        public ResourcePermissionHttpResponse(string message, string code)
            : base(new ErrorApiModel { Message = message, Code = code }, HttpStatusCode.Forbidden)
        {
        }
    }
}