// <copyright file="AspNetCoreContextResolver.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.AspNetCore
{
    using AutoResponse.Core.Mappers;

    /// <summary>
    /// ASP.NET Core context resolver.
    /// </summary>
    /// <seealso cref="AutoResponse.Core.Mappers.IContextResolver" />
    public class AspNetCoreContextResolver : IContextResolver
    {
        /// <inheritdoc />
        public bool IncludeFullDetails(object context)
        {
            return false;
        }
    }
}