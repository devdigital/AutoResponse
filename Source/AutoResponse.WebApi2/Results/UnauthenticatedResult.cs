namespace AutoResponse.WebApi2.Results
{
    using System.Net.Http;

    using AutoResponse.Core.Responses;

    public class UnauthenticatedResult : HttpResponseResult
    {
        public UnauthenticatedResult(HttpRequestMessage request)
            : base(request, new UnauthenticatedHttpResponse())
        {
        }
    }
}