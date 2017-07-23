namespace AutoResponse.Core.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;

    using AutoResponse.Core.Dtos;

    public class ServiceErrorHttpResponse : ErrorHttpResponse
    {
        public ServiceErrorHttpResponse(string message, string code)
            : base(message, code, HttpStatusCode.InternalServerError)
        {
        }        
    }
}