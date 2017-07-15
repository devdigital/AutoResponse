namespace AutoResponse.Core.Responses
{
    using System;
    using System.Net;

    using AutoResponse.Core.Dtos;

    public class ServiceErrorWithExceptionHttpResponse : JsonHttpResponse<ErrorWithExceptionDto>
    {
        public ServiceErrorWithExceptionHttpResponse(string message, string exceptionMessage, string exceptionString)
            : base(ToErrorWithException(message, exceptionMessage, exceptionString), HttpStatusCode.InternalServerError)
        {
        }

        private static ErrorWithExceptionDto ToErrorWithException(string message, string exceptionMessage, string exceptionString)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (string.IsNullOrWhiteSpace(exceptionMessage))
            {
                throw new ArgumentNullException(nameof(exceptionMessage));
            }

            if (string.IsNullOrWhiteSpace(exceptionString))
            {
                throw new ArgumentNullException(nameof(exceptionString));
            }

            return new ErrorWithExceptionDto
            {
                Message = message,
                ExceptionMessage = exceptionMessage,
                ExceptionString = exceptionString
            };
        }
    }
}