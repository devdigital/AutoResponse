// <copyright file="ApplicationBuilderExtensions.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.AspNetCore
{
    using AutoResponse.Core.Logging;
    using AutoResponse.Core.Mappers;
    using Microsoft.AspNetCore.Builder;

    /// <summary>
    /// Application builder extensions.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Use AutoResponse.
        /// </summary>
        /// <param name="applicationBuilder">The application builder.</param>
        public static void UseAutoResponse(this IApplicationBuilder applicationBuilder)
        {
            UseAutoResponse(applicationBuilder, new AutoResponseOptions());
        }

        /// <summary>
        /// Use AutoResponse with options.
        /// </summary>
        /// <param name="applicationBuilder">The application builder.</param>
        /// <param name="options">The options.</param>
        public static void UseAutoResponse(this IApplicationBuilder applicationBuilder, AutoResponseOptions options)
        {
            var defaultMapper = new AutoResponseApiEventHttpResponseMapper(
                new AspNetCoreContextResolver());

            var defaultLogger = new NullAutoResponseLogger();
            const string defaultDomainResultPropertyName = "Event";

            var mapper = options.EventHttpResponseMapper ?? defaultMapper;
            var logger = options.Logger ?? defaultLogger;
            var domainResultPropertyName = string.IsNullOrWhiteSpace(options.DomainResultPropertyName)
                ? defaultDomainResultPropertyName
                : options.DomainResultPropertyName;

            applicationBuilder.UseMiddleware<AutoResponseExceptionMiddleware>(
                mapper,
                logger,
                domainResultPropertyName);
        }
    }
}