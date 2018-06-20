// <copyright file="UnauthenticatedResult.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.AspNetCore.Results
{
    using AutoResponse.Core.ApiEvents;

    /// <summary>
    /// Unauthenticated result.
    /// </summary>
    /// <seealso cref="AutoResponse.AspNetCore.Results.AutoResponseResult" />
    public class UnauthenticatedResult : AutoResponseResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthenticatedResult"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public UnauthenticatedResult(string message)
            : base(new UnauthenticatedApiEvent(message))
        {
        }
    }
}