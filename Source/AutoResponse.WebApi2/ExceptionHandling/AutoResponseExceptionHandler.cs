// <copyright file="AutoResponseExceptionHandler.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.ExceptionHandling
{
    using System;
    using System.Web.Http.ExceptionHandling;

    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Core.Exceptions;
    using AutoResponse.WebApi2.Results;

    /// <summary>
    /// AutoResponse exception handler.
    /// </summary>
    /// <seealso cref="System.Web.Http.ExceptionHandling.ExceptionHandler" />
    public class AutoResponseExceptionHandler : ExceptionHandler
    {
        private readonly string domainResultPropertyName;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoResponseExceptionHandler"/> class.
        /// </summary>
        public AutoResponseExceptionHandler()
        {
            this.domainResultPropertyName = "Event";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoResponseExceptionHandler"/> class.
        /// </summary>
        /// <param name="domainResultPropertyName">Name of the domain result property.</param>
        public AutoResponseExceptionHandler(string domainResultPropertyName)
        {
            if (string.IsNullOrWhiteSpace(domainResultPropertyName))
            {
                throw new ArgumentNullException(nameof(domainResultPropertyName));
            }

            this.domainResultPropertyName = domainResultPropertyName;
        }

        /// <summary>
        /// Determines whether the exception should be handled.
        /// Fix for exception handler not being invoked because of CORs package handling exceptions
        /// See http://stackoverflow.com/a/24634485/248164.
        /// </summary>
        /// <param name="context">The exception handler context.</param>
        /// <returns>
        /// true if the exception should be handled; otherwise, false.
        /// </returns>
        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }

        /// <inheritdoc />
        public override void Handle(ExceptionHandlerContext context)
        {
            if (context?.Exception == null)
            {
                base.Handle(context);
                return;
            }

            object apiEvent = null;
            if (context.Exception is AutoResponseException autoResponseException)
            {
                apiEvent = autoResponseException.EventObject;
            }

            if (apiEvent == null)
            {
                apiEvent = context.Exception?.GetType().GetProperty(this.domainResultPropertyName)
                    ?.GetValue(context.Exception, null);
            }

            if (apiEvent == null)
            {
                apiEvent = new ServiceErrorApiEvent(context.Exception);
            }

            context.Result = new AutoResponseResult(context.Request, apiEvent);
            base.Handle(context);
        }
    }
}