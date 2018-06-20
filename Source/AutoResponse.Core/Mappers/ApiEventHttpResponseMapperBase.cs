// <copyright file="ApiEventHttpResponseMapperBase.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Mappers
{
    using System;
    using System.Collections.Generic;

    using AutoResponse.Core.Formatters;
    using AutoResponse.Core.Responses;

    /// <summary>
    /// API event HTTP response mapper base.
    /// </summary>
    /// <seealso cref="AutoResponse.Core.Mappers.IApiEventHttpResponseMapper" />
    public abstract class ApiEventHttpResponseMapperBase : IApiEventHttpResponseMapper
    {
        private readonly IAutoResponseExceptionFormatter formatter;

        private readonly Lazy<IDictionary<Type, Func<ExceptionHttpResponseContext, object, IHttpResponse>>> mappers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiEventHttpResponseMapperBase"/> class.
        /// </summary>
        /// <param name="formatter">The formatter.</param>
        protected ApiEventHttpResponseMapperBase(IAutoResponseExceptionFormatter formatter)
        {
            this.formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));

            this.mappers = new Lazy<IDictionary<Type, Func<ExceptionHttpResponseContext, object, IHttpResponse>>>(() =>
            {
                var mappersInstance = new Dictionary<Type, Func<ExceptionHttpResponseContext, object, IHttpResponse>>();
                this.ConfigureMappings(new ExceptionHttpResponseConfiguration(mappersInstance));
                return mappersInstance;
            });
        }

        /// <summary>
        /// Gets the HTTP response.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="apiEvent">The API event.</param>
        /// <returns>The HTTP response.</returns>
        public IHttpResponse GetHttpResponse(object context, object apiEvent)
        {
            if (apiEvent == null)
            {
                throw new ArgumentNullException(nameof(apiEvent));
            }

            var apiEventType = apiEvent.GetType();
            if (apiEventType.IsGenericType)
            {
                apiEventType = apiEventType.GetGenericTypeDefinition();
            }

            if (!this.mappers.Value.ContainsKey(apiEventType))
            {
                return null;
            }

            var mapper = this.mappers.Value[apiEventType];

            return mapper?.Invoke(
                new ExceptionHttpResponseContext(context, this.formatter),
                apiEvent);
        }

        /// <summary>
        /// Configures the mappings.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        protected abstract void ConfigureMappings(ExceptionHttpResponseConfiguration configuration);
    }
}
