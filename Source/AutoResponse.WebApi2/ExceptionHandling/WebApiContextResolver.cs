// <copyright file="WebApiContextResolver.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.ExceptionHandling
{
    using System;
    using System.Net.Http;

    using AutoResponse.Core.Mappers;

    /// <summary>
    /// Web API context resolver.
    /// </summary>
    /// <seealso cref="AutoResponse.Core.Mappers.IContextResolver" />
    public class WebApiContextResolver : IContextResolver
    {
        /// <inheritdoc />
        public bool IncludeFullDetails(object context)
        {
            if (!(context is HttpRequestMessage request))
            {
                throw new InvalidOperationException("Unexpected null request");
            }

            return request.ShouldIncludeErrorDetail();
        }
    }
}