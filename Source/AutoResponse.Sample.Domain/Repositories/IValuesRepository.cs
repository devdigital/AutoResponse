// <copyright file="IValuesRepository.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Sample.Domain.Repositories
{
    using AutoResponse.Sample.Domain.Models;

    /// <summary>
    /// Values repository.
    /// </summary>
    public interface IValuesRepository
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="valueId">The value identifier.</param>
        /// <returns>The value.</returns>
        Value GetValue(int valueId);
    }
}