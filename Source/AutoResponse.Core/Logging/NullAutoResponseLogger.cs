// <copyright file="NullAutoResponseLogger.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Logging
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Null AutoResponse logger.
    /// </summary>
    /// <seealso cref="AutoResponse.Core.Logging.IAutoResponseLogger" />
    public class NullAutoResponseLogger : IAutoResponseLogger
    {
        /// <inheritdoc />
        public Task LogException(Exception exception)
        {
            return Task.CompletedTask;
        }
    }
}