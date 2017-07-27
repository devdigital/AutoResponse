namespace AutoResponse.Core.Responses
{
    using System.Net;

    using AutoResponse.Core.Dtos;

    public class ResourceCreatedHttpResponse : JsonHttpResponse<ResourceCreatedDto>
    {
        // TODO: location header
        public ResourceCreatedHttpResponse(string message, string code, string resourceId)
            : base(new ResourceCreatedDto { Message = message, Code = code, Id = resourceId }, HttpStatusCode.Created)
        {
        }
    }
}