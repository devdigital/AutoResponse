namespace AutoResponse.WebApi2.Results
{
    using System.Net.Http;

    using AutoResponse.Core.Responses;

    public class ResourceCreatedResult : HttpResponseResult
    {
        public ResourceCreatedResult(
              HttpRequestMessage request,
              string resourceId)       
            : base(request, new ResourceCreatedHttpResponse(resourceId))
        {
        }
    }
}