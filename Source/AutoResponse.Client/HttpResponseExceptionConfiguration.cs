namespace AutoResponse.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    public class HttpResponseExceptionConfiguration
    {
        private readonly IDictionary<ErrorRegistration, 
            Func<ResponseContent, HttpResponseExceptionContext, Exception>> mappings;

        public HttpResponseExceptionConfiguration(IDictionary<ErrorRegistration, 
            Func<ResponseContent, HttpResponseExceptionContext, Exception>> mappings)
        {
            if (mappings == null)
            {
                throw new ArgumentNullException(nameof(mappings));
            }

            this.mappings = mappings;
        }

        public void AddMapping(
            HttpStatusCode statusCode, 
            Func<ResponseContent, HttpResponseExceptionContext, Exception> mapping)
        {
            this.AddMapping(statusCode, null, mapping);
        }

        public void AddMapping(
            HttpStatusCode statusCode, 
            string errorCode,
            Func<ResponseContent, HttpResponseExceptionContext, Exception> mapping)
        {
            if (mapping == null)
            {
                throw new ArgumentNullException(nameof(mapping));
            }

            var errorRegistration = new ErrorRegistration(statusCode, errorCode);
            if (this.mappings.ContainsKey(errorRegistration))
            {
                throw new InvalidOperationException(
                    $"HTTP response to exception mapping is already registered for response status code {errorRegistration.StatusCode} and error code {errorRegistration.ErrorCode}");
            }

            this.mappings.Add(errorRegistration, mapping);
        }

        public void UpdateMapping(
            HttpStatusCode statusCode,
            Func<ResponseContent, HttpResponseExceptionContext, Exception> mapping)
        {
            this.UpdateMapping(statusCode, null, mapping);
        }

        public void UpdateMapping(
            HttpStatusCode statusCode,
            string errorCode,
            Func<ResponseContent, HttpResponseExceptionContext, Exception> mapping)
        {
            var registration = new ErrorRegistration(statusCode, errorCode);

            if (!this.mappings.ContainsKey(registration))
            {
                throw new InvalidOperationException(
                    $"HTTP response to exception mapping is not registered for response status code {statusCode} and error code {errorCode}");
            }

            this.mappings[registration] = mapping;
        }

        public void RemoveMapping(HttpStatusCode statusCode)
        {
            this.RemoveMapping(statusCode, null);
        }

        public void RemoveMapping(HttpStatusCode statusCode, string errorCode)
        {
            var registration = new ErrorRegistration(statusCode, errorCode);

            if (!this.mappings.ContainsKey(registration))
            {
                throw new InvalidOperationException(
                    $"HTTP response to exception mapping is not registered for response status code {statusCode} and error code {errorCode}");
            }

            this.mappings.Remove(registration);
        }
    }
}