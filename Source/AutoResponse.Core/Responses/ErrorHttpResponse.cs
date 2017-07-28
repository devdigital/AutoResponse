namespace AutoResponse.Core.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using AutoResponse.Core.Dtos;

    public class ErrorHttpResponse : JsonHttpResponse<ResponseDto>
    {
        public ErrorHttpResponse(string message, string code, HttpStatusCode statusCode, IDictionary<string, string[]> headers = null) 
            : base(new ResponseDto { Message = message, Code = code }, statusCode, headers)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }     
        }
    }
}