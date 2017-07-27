namespace AutoResponse.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    public class HttpResponseExceptionConfiguration
    {
        private readonly IDictionary<ErrorRegistration, 
            Func<ResponseContent, HttpResponseExceptionContext, Exception>> mappers;

        public HttpResponseExceptionConfiguration(IDictionary<ErrorRegistration, 
            Func<ResponseContent, HttpResponseExceptionContext, Exception>> mappers)
        {
            if (mappers == null)
            {
                throw new ArgumentNullException(nameof(mappers));
            }

            this.mappers = mappers;
        }

        public void AddMapping(
            HttpStatusCode statusCode, 
            Func<ResponseContent, HttpResponseExceptionContext, Exception> mapper)
        {
            this.AddMapping(statusCode, null, mapper);
        }

        public void AddMapping(
            HttpStatusCode statusCode, 
            string errorCode,
            Func<ResponseContent, HttpResponseExceptionContext, Exception> mapper)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            var errorRegistration = new ErrorRegistration(statusCode, errorCode);
            if (this.mappers.ContainsKey(errorRegistration))
            {
                throw new InvalidOperationException(
                    $"HTTP response to exception mapper is already registered for response status code {errorRegistration.StatusCode} and error code {errorRegistration.ErrorCode}");
            }

            this.mappers.Add(errorRegistration, mapper);
        }
    }
}