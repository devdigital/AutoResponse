// <copyright file="IAutoResponseApiEvent.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.ApiEvents
{
    /// <summary>
    /// AutoResponse API event.
    /// </summary>
    public interface IAutoResponseApiEvent
    {
        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        string Code { get; }
    }
}