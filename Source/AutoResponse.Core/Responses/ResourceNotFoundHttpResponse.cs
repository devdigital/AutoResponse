namespace AutoResponse.Core.Responses
{
    using System.Net;

    using AutoResponse.Core.Dtos;

    public class ResourceNotFoundHttpResponse : JsonHttpResponse<ResourceNotFoundDto>
    {      
        public ResourceNotFoundHttpResponse(string message, string code, string resource, string resourceId)
            : base(new ResourceNotFoundDto { Message = message, Code = code, Resource = resource, ResourceId = resourceId }, HttpStatusCode.NotFound)
        {
        }
    }
}