// <copyright file="IContextResolver.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Mappers
{
    /// <summary>
    /// Context resolver.
    /// </summary>
    public interface IContextResolver
    {
        /// <summary>
        /// Includes the full error details in the response.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>True if include full error details; false otherwise.</returns>
        bool IncludeFullDetails(object context);
    }
}