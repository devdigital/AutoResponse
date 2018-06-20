// <copyright file="ISettingsService.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Sample.Domain.Services
{
    /// <summary>
    /// Settings service.
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Gets the include full details setting.
        /// </summary>
        /// <returns>True if include full details; false otherwise.</returns>
        bool GetIncludeFullDetails();
    }
}