namespace AutoResponse.Core.Responses
{
    using System.Net;

    using AutoResponse.Core.Dtos;

    public class UnauthenticatedHttpResponse : JsonHttpResponse<ErrorDto>
    {
        public UnauthenticatedHttpResponse()
            : base(new ErrorDto { Message = "The user is not authenticated" }, HttpStatusCode.Unauthorized)
        {
        }
    }
}