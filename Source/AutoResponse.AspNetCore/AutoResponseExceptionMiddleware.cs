// <copyright file="AutoResponseExceptionMiddleware.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.AspNetCore
{
    using System;
    using System.Threading.Tasks;
    using AutoResponse.Core.Exceptions;
    using AutoResponse.Core.Logging;
    using AutoResponse.Core.Mappers;
    using AutoResponse.Core.Responses;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// AutoResponse exception middleware.
    /// </summary>
    public class AutoResponseExceptionMiddleware
    {
        private readonly RequestDelegate next;

        private readonly IApiEventHttpResponseMapper mapper;

        private readonly IAutoResponseLogger logger;

        private readonly string domainResultPropertyName;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoResponseExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="domainResultPropertyName">Name of the domain result property.</param>
        public AutoResponseExceptionMiddleware(
            RequestDelegate next,
            IApiEventHttpResponseMapper mapper,
            IAutoResponseLogger logger,
            string domainResultPropertyName)
        {
            if (string.IsNullOrWhiteSpace(domainResultPropertyName))
            {
                throw new ArgumentNullException(nameof(domainResultPropertyName));
            }

            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.domainResultPropertyName = domainResultPropertyName;
        }

        /// <summary>
        /// Invokes the middleware.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The task.</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);
            }
            catch (AutoResponseException exception)
            {
                await this.logger.LogException(exception);
                await this.ConvertExceptionToHttpResponse(
                    exception,
                    exception.EventObject,
                    context);
            }
            catch (Exception exception)
            {
                await this.logger.LogException(exception);

                var exceptionEvent =
                    exception.GetType().GetProperty(this.domainResultPropertyName)?.GetValue(exception, null);

                if (exceptionEvent != null)
                {
                    await this.ConvertExceptionToHttpResponse(
                        exception,
                        exceptionEvent,
                        context);

                    return;
                }

                throw;
            }
        }

        private static async Task ConvertHttpResponseToResponse(IHttpResponse httpResponse, HttpContext context)
        {
            context.Response.StatusCode = (int)httpResponse.StatusCode;

            foreach (var header in httpResponse.Headers)
            {
                context.Response.Headers.Add(header.Key, header.Value);
            }

            context.Response.ContentType = httpResponse.ContentType;
            context.Response.ContentLength = httpResponse.ContentLength;

            await context.Response.WriteAsync(httpResponse.Content);
        }

        private async Task ConvertExceptionToHttpResponse(Exception exception, object apiEvent, HttpContext context)
        {
            var httpResponse = this.mapper.GetHttpResponse(
                context: null,
                apiEvent: apiEvent);

            if (httpResponse == null)
            {
                throw exception;
            }

            await ConvertHttpResponseToResponse(httpResponse, context);
        }
    }
}
