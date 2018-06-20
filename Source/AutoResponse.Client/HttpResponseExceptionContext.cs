// <copyright file="HttpResponseExceptionContext.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Client
{
    using System;

    /// <summary>
    /// HTTP response exception context.
    /// </summary>
    public class HttpResponseExceptionContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseExceptionContext"/> class.
        /// </summary>
        /// <param name="formatter">The formatter.</param>
        public HttpResponseExceptionContext(IAutoResponseHttpResponseFormatter formatter)
        {
            this.Formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }

        /// <summary>
        /// Gets the formatter.
        /// </summary>
        /// <value>
        /// The formatter.
        /// </value>
        public IAutoResponseHttpResponseFormatter Formatter { get; }
    }
}