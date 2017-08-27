using AutoResponse.Core.Dtos;

namespace AutoResponse.Core.Responses
{
    using System.Net;

    public class ServiceErrorHttpResponse : JsonHttpResponse<ErrorApiModel>
    {
        public ServiceErrorHttpResponse(string message, string code)
            : base(new ErrorApiModel { Message = message, Code = code }, HttpStatusCode.InternalServerError)
        {
        }        
    }
}