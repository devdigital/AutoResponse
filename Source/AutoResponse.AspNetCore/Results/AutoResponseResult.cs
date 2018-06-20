// <copyright file="AutoResponseResult.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.AspNetCore.Results
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using AutoResponse.Core.Mappers;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// AutoResponse result.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.IActionResult" />
    public class AutoResponseResult : IActionResult
    {
        private readonly object apiEvent;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoResponseResult"/> class.
        /// </summary>
        /// <param name="apiEvent">The API event.</param>
        public AutoResponseResult(object apiEvent)
        {
            this.apiEvent = apiEvent ?? throw new ArgumentNullException(nameof(apiEvent));
        }

        /// <inheritdoc />
        public async Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var mapper = this.GetMapper(context);
            if (mapper == null)
            {
                throw new InvalidOperationException($"No {typeof(IApiEventHttpResponseMapper).Name} registered in dependency resolver");
            }

            var httpResponse = mapper.GetHttpResponse(
                context: null,
                apiEvent: this.apiEvent);

            if (httpResponse == null)
            {
                throw new InvalidOperationException($"No mapping registered for API event type {this.apiEvent.GetType().Name}");
            }

            var response = context.HttpContext.Response;
            response.StatusCode = (int)httpResponse.StatusCode;

            foreach (var keyValuePair in httpResponse.Headers)
            {
                response.Headers.Add(keyValuePair.Key, keyValuePair.Value);
            }

            response.ContentType = httpResponse.ContentType;
            response.ContentLength = httpResponse.ContentLength;

            var data = Encoding.UTF8.GetBytes(httpResponse.Content);
            await response.Body.WriteAsync(data, 0, data.Length);
        }

        /// <summary>
        /// Gets the mapper.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The mapper.</returns>
        protected virtual IApiEventHttpResponseMapper GetMapper(ActionContext context)
        {
            var services = context?.HttpContext?.RequestServices;
            return services?.GetService(
                typeof(IApiEventHttpResponseMapper)) as IApiEventHttpResponseMapper;
        }
    }
}