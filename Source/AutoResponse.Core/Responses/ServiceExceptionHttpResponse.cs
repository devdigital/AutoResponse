namespace AutoResponse.Core.Responses
{
    using System;
    using System.Net;

    using AutoResponse.Core.Dtos;

    public class ServiceExceptionHttpResponse : JsonHttpResponse<ErrorDto>
    {
        public ServiceExceptionHttpResponse(string message, Exception exception)
            : base(ToErrorDto(message, exception), HttpStatusCode.InternalServerError)
        {
        }

        public static ErrorDto ToErrorDto(string message, Exception exception)
        {
            // TODO: stack trace etc.
            return new ErrorDto
            {
                Message = message
            };
        }
    }
}