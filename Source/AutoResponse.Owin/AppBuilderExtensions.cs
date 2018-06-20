// <copyright file="AppBuilderExtensions.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Owin
{
    using AutoResponse.Core.Logging;
    using AutoResponse.Core.Mappers;

    using global::Owin;

    /// <summary>
    /// App builder extensions.
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Uses AutoResponse.
        /// </summary>
        /// <param name="appBuilder">The application builder.</param>
        public static void UseAutoResponse(this IAppBuilder appBuilder)
        {
            UseAutoResponse(appBuilder, new AutoResponseOptions());
        }

        /// <summary>
        /// Uses AutoResponse with options.
        /// </summary>
        /// <param name="appBuilder">The application builder.</param>
        /// <param name="options">The options.</param>
        public static void UseAutoResponse(this IAppBuilder appBuilder, AutoResponseOptions options)
        {
            var defaultMapper = new AutoResponseApiEventHttpResponseMapper(new OwinContextResolver());
            var defaultLogger = new NullAutoResponseLogger();
            var defaultDomainResultPropertyName = "Event";

            var mapper = options.EventHttpResponseMapper ?? defaultMapper;
            var logger = options.Logger ?? defaultLogger;
            var domainResultPropertyName = string.IsNullOrWhiteSpace(options.DomainResultPropertyName)
                ? defaultDomainResultPropertyName
                : options.DomainResultPropertyName;

            appBuilder.Use<AutoResponseExceptionMiddleware>(mapper, logger, domainResultPropertyName);
        }
    }
}