using AutoResponse.Core.Dtos;

namespace AutoResponse.Core.Responses
{
    using System.Net;

    public class ServiceErrorHttpResponse : JsonHttpResponse<ErrorDto>
    {
        public ServiceErrorHttpResponse(string message, string code)
            : base(new ErrorDto { Message = message, Code = code }, HttpStatusCode.InternalServerError)
        {
        }        
    }
}