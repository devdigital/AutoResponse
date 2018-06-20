// <copyright file="ExceptionHttpResponseContext.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Mappers
{
    using System;

    using AutoResponse.Core.Formatters;

    /// <summary>
    /// Exception HTTP response context.
    /// </summary>
    public class ExceptionHttpResponseContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHttpResponseContext"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="formatter">The formatter.</param>
        public ExceptionHttpResponseContext(object context, IAutoResponseExceptionFormatter formatter)
        {
            this.Context = context;
            this.Formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        public object Context { get; }

        /// <summary>
        /// Gets the formatter.
        /// </summary>
        /// <value>
        /// The formatter.
        /// </value>
        public IAutoResponseExceptionFormatter Formatter { get; }
    }
}