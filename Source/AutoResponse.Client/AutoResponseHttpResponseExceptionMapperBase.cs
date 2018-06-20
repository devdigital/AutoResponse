// <copyright file="AutoResponseHttpResponseExceptionMapperBase.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// AutoResponse HTTP response exception mapper base.
    /// </summary>
    /// <seealso cref="AutoResponse.Client.IHttpResponseExceptionMapper" />
    public abstract class AutoResponseHttpResponseExceptionMapperBase
        : IHttpResponseExceptionMapper
    {
        private readonly IAutoResponseHttpResponseFormatter formatter;

        private readonly Lazy<IDictionary<ErrorRegistration, Func<ResponseContent, HttpResponseExceptionContext, Exception>>> mappers;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoResponseHttpResponseExceptionMapperBase"/> class.
        /// </summary>
        /// <param name="formatter">The formatter.</param>
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

        /// <inheritdoc />
        public Task<bool> IsErrorResponse(HttpResponseMessage response)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            return Task.FromResult(response.StatusCode >= (HttpStatusCode)400);
        }

        /// <inheritdoc />
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
                        var requestBody = await this.GetContent(response.RequestMessage?.Content);
                        return await this.GetDefaultException(
                            new RequestContent(response.RequestMessage, requestBody),
                            new ResponseContent(response, responseContent),
                            new HttpResponseExceptionContext(this.formatter));
                    }

                    return mapper.Invoke(
                        new ResponseContent(response, responseContent),
                        new HttpResponseExceptionContext(this.formatter));
                }
            }

            // If no status code, error code match, then look for status code, null error code.
            var statusCodeMatchRegistration = new ErrorRegistration(statusCode, null);
            if (this.mappers.Value.ContainsKey(statusCodeMatchRegistration))
            {
                var mapper = this.mappers.Value[statusCodeMatchRegistration];
                if (mapper == null)
                {
                    var requestBody = await this.GetContent(response.RequestMessage?.Content);
                    return await this.GetDefaultException(
                        new RequestContent(response.RequestMessage, requestBody),
                        new ResponseContent(response, responseContent),
                        new HttpResponseExceptionContext(this.formatter));
                }

                return mapper.Invoke(
                    new ResponseContent(response, responseContent),
                    new HttpResponseExceptionContext(this.formatter));
            }

            var body = await this.GetContent(response.RequestMessage?.Content);
            return await this.GetDefaultException(
                new RequestContent(response.RequestMessage, body),
                new ResponseContent(response, responseContent),
                new HttpResponseExceptionContext(this.formatter));
        }

        /// <summary>
        /// Configures the mappings.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        protected abstract void ConfigureMappings(
            HttpResponseExceptionConfiguration configuration);

        /// <summary>
        /// Gets the default exception.
        /// </summary>
        /// <param name="requestContent">Content of the request.</param>
        /// <param name="responseContent">Content of the response.</param>
        /// <param name="context">The context.</param>
        /// <returns>The default exception.</returns>
        protected abstract Task<Exception> GetDefaultException(
            RequestContent requestContent,
            ResponseContent responseContent,
            HttpResponseExceptionContext context);

        private async Task<string> GetContent(HttpContent content)
        {
            if (content == null)
            {
                return null;
            }

            try
            {
                return await content.ReadAsStringAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}