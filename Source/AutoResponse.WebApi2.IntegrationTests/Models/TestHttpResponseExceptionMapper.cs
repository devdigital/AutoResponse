// <copyright file="TestHttpResponseExceptionMapper.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.IntegrationTests.Models
{
    using System;
    using System.Threading.Tasks;

    using AutoResponse.Client;

    /// <summary>
    /// Test HTTP response exception mapper.
    /// </summary>
    /// <seealso cref="AutoResponse.Client.AutoResponseHttpResponseExceptionMapper" />
    internal class TestHttpResponseExceptionMapper : AutoResponseHttpResponseExceptionMapper
    {
        private readonly Action<HttpResponseExceptionConfiguration> configure;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestHttpResponseExceptionMapper"/> class.
        /// </summary>
        /// <param name="configure">The configure.</param>
        public TestHttpResponseExceptionMapper(Action<HttpResponseExceptionConfiguration> configure)
        {
            this.configure = configure ?? throw new ArgumentNullException(nameof(configure));
            this.DefaultException = new Exception("Default exception");
        }

        /// <summary>
        /// Gets the default exception.
        /// </summary>
        /// <value>
        /// The default exception.
        /// </value>
        public Exception DefaultException { get; }

        /// <inheritdoc />
        protected override void ConfigureMappings(HttpResponseExceptionConfiguration configuration)
        {
            this.configure(configuration);
        }

        /// <inheritdoc />
        protected override Task<Exception> GetDefaultException(
            RequestContent requestContent,
            ResponseContent responseContent,
            HttpResponseExceptionContext context)
        {
            return Task.FromResult(this.DefaultException);
        }
    }
}