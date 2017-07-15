namespace AutoResponse.Core.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;

    using AutoResponse.Core.Dtos;

    public class ServiceErrorHttpResponse : JsonHttpResponse<ErrorDto>
    {
        public ServiceErrorHttpResponse(string message)
            : base(ToErrorDto(message), HttpStatusCode.InternalServerError)
        {
        }

        public static ErrorDto ToErrorDto(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            return new ErrorDto
            {
                Message = message
            };
        }
    }
}