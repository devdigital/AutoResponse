using AutoResponse.Core.Dtos;

namespace AutoResponse.Core.Responses
{
    using System.Net;

    public class UnauthenticatedHttpResponse : JsonHttpResponse<ErrorDto>
    {
        public UnauthenticatedHttpResponse(string message, string code)
            : base(new ErrorDto { Message = message, Code =  code }, HttpStatusCode.Unauthorized)
        {
        }
    }
}