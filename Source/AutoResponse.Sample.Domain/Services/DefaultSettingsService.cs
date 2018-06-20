// <copyright file="DefaultSettingsService.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Sample.Domain.Services
{
    /// <summary>
    /// Default settings service.
    /// </summary>
    /// <seealso cref="AutoResponse.Sample.Domain.Services.ISettingsService" />
    public class DefaultSettingsService : ISettingsService
    {
        /// <inheritdoc />
        public bool GetIncludeFullDetails()
        {
            return false;
        }
    }
}