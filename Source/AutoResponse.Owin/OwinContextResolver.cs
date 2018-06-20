// <copyright file="OwinContextResolver.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Owin
{
    using AutoResponse.Core.Mappers;

    /// <summary>
    /// OWIN context resolver.
    /// </summary>
    /// <seealso cref="AutoResponse.Core.Mappers.IContextResolver" />
    public class OwinContextResolver : IContextResolver
    {
        /// <inheritdoc />
        public bool IncludeFullDetails(object context)
        {
            return false;
        }
    }
}