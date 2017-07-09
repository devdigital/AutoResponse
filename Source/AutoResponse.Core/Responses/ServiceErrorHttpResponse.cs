namespace AutoResponse.Core.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;

    using AutoResponse.Core.Dtos;

    public class ServiceErrorHttpResponse : JsonHttpResponse<ErrorDto>
    {
        public ServiceErrorHttpResponse(string message, Exception exception)
            : base(ToErrorDto(message, exception), HttpStatusCode.InternalServerError)
        {
        }

        public static ErrorDto ToErrorDto(string message, Exception exception)
        {
            return new ErrorDto
            {
                Message = message
            };
        }
    }

    public class ServiceFullErrorHttpResponse : JsonHttpResponse<FullErrorDto>
    {
        public ServiceFullErrorHttpResponse(string message, Exception exception)
            : base(ToFullErrorDto(message, exception), HttpStatusCode.InternalServerError)
        {
        }

        private static FullErrorDto ToFullErrorDto(string message, Exception exception)
        {
            return new FullErrorDto
            {
                Message = message,
                StackTrace = exception.StackTrace
            };
        }
    }
}