namespace AutoResponse.Core.Responses
{
    using System.Net;

    using AutoResponse.Core.Dtos;

    public class ResourceNotFoundHttpResponse : JsonHttpResponse<ResourceNotFoundApiModel>
    {      
        public ResourceNotFoundHttpResponse(string message, string code, string resource, string resourceId)
            : base(new ResourceNotFoundApiModel { Message = message, Code = code, Resource = resource, ResourceId = resourceId }, HttpStatusCode.NotFound)
        {
        }
    }
}