namespace AutoResponse.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json.Linq;

    public abstract class AutoResponseHttpResponseExceptionMapperBase 
        : IHttpResponseExceptionMapper
    {
        private readonly IAutoResponseHttpResponseFormatter formatter;

        private readonly Lazy<IDictionary<ErrorRegistration, Func<ResponseContent, HttpResponseExceptionContext, Exception>>> mappers;

        protected AutoResponseHttpResponseExceptionMapperBase(IAutoResponseHttpResponseFormatter formatter)
        {
            this.formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));

            this.mappers = new Lazy<IDictionary<ErrorRegistration, Func<ResponseContent, HttpResponseExceptionContext, Exception>>>(() =>
            {
                var mappersInstance = new Dictionary<ErrorRegistration, Func<ResponseContent, HttpResponseExceptionContext, Exception>>(
                    new ErrorRegistrationEqualityComparer());
                this.ConfigureMappings(new HttpResponseExceptionConfiguration(mappersInstance));
                return mappersInstance;
            });
        }

        protected abstract void ConfigureMappings(
            HttpResponseExceptionConfiguration configuration);

        public Task<bool> IsErrorResponse(HttpResponseMessage response)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            return Task.FromResult(response.StatusCode >= (HttpStatusCode)400);
        }

        public async Task<Exception> GetException(HttpResponseMessage response)
        {
            var statusCode = response.StatusCode;
            var responseContent = await response.Content.ReadAsStringAsync();
            string errorCode;

            try
            {
                var jobject = JObject.Parse(responseContent);
                errorCode = jobject?.Property("code")?.Value?.Value<string>();
            }
            catch (Exception)
            {
                errorCode = null;
            }

            if (!string.IsNullOrWhiteSpace(errorCode))
            {
                // Search for both response code and error code match first
                var exactRegistration = new ErrorRegistration(statusCode, errorCode);
                if (this.mappers.Value.ContainsKey(exactRegistration))
                {
                    var mapper = this.mappers.Value[exactRegistration];
                    if (mapper == null)
                    {
                        return await this.GetDefaultException(
                            new ResponseContent(response, responseContent),
                            new HttpResponseExceptionContext(this.formatter));
                    }

                    return mapper.Invoke(
                        new ResponseContent(response, responseContent),
                        new HttpResponseExceptionContext(this.formatter));
                }
            }

            // If no status code, error code match, then look for status code, null error code                       
            var statusCodeMatchRegistration = new ErrorRegistration(statusCode, null);
            if (this.mappers.Value.ContainsKey(statusCodeMatchRegistration))
            {
                var mapper = this.mappers.Value[statusCodeMatchRegistration];
                if (mapper == null)
                {
                    return await this.GetDefaultException(
                        new ResponseContent(response, responseContent),
                        new HttpResponseExceptionContext(this.formatter));
                }

                return mapper.Invoke(
                    new ResponseContent(response, responseContent),
                    new HttpResponseExceptionContext(this.formatter));
            }
        
            return await this.GetDefaultException(
                new ResponseContent(response, responseContent),
                new HttpResponseExceptionContext(this.formatter));
        }

        protected abstract Task<Exception> GetDefaultException(
            ResponseContent responseContent, 
            HttpResponseExceptionContext context);
    }
}