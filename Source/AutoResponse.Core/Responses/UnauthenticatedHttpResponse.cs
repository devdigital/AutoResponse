using AutoResponse.Core.Dtos;

namespace AutoResponse.Core.Responses
{
    using System.Net;

    public class UnauthenticatedHttpResponse : JsonHttpResponse<ErrorApiModel>
    {
        public UnauthenticatedHttpResponse(string message, string code)
            : base(new ErrorApiModel { Message = message, Code =  code }, HttpStatusCode.Unauthorized)
        {
        }
    }
}