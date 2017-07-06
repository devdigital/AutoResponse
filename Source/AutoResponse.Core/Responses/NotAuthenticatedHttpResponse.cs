namespace AutoResponse.Core.Responses
{
    using System.Net;

    using AutoResponse.Core.Dtos;

    public class NotAuthenticatedHttpResponse : JsonHttpResponse<ErrorDto>
    {
        public NotAuthenticatedHttpResponse()
            : base(new ErrorDto { Message = "The user is not authenticated" }, HttpStatusCode.Unauthorized)
        {
        }
    }
}