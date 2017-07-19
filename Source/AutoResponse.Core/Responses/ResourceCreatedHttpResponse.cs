namespace AutoResponse.Core.Responses
{
    using System.Net;

    using AutoResponse.Core.Dtos;

    public class ResourceCreatedHttpResponse : JsonHttpResponse<ResourceCreatedDto>
    {
        // TODO: location header
        public ResourceCreatedHttpResponse(string resourceId)
            : base(new ResourceCreatedDto { Id = resourceId }, HttpStatusCode.Created)
        {
        }
    }
}