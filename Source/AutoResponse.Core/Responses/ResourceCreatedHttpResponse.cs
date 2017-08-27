namespace AutoResponse.Core.Responses
{
    using System.Net;

    using AutoResponse.Core.Dtos;

    public class ResourceCreatedHttpResponse : JsonHttpResponse<ResourceCreatedApiModel>
    {
        // TODO: location header
        public ResourceCreatedHttpResponse(string message, string code, string resourceId)
            : base(new ResourceCreatedApiModel { Message = message, Code = code, Id = resourceId }, HttpStatusCode.Created)
        {
        }
    }
}