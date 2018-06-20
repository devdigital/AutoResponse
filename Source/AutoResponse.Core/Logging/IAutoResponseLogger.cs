// <copyright file="IAutoResponseLogger.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Logging
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// AutoResponse logger.
    /// </summary>
    public interface IAutoResponseLogger
    {
        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>The task.</returns>
        Task LogException(Exception exception);
    }
}
