// <copyright file="DefaultValuesRepository.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Sample.Data.Repositories
{
    using AutoResponse.Sample.Domain.Models;
    using AutoResponse.Sample.Domain.Repositories;

    /// <summary>
    /// Default values repository.
    /// </summary>
    /// <seealso cref="AutoResponse.Sample.Domain.Repositories.IValuesRepository" />
    public class DefaultValuesRepository : IValuesRepository
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="valueId">The value identifier.</param>
        /// <returns>The value.</returns>
        public Value GetValue(int valueId)
        {
            return new Value(valueId);
        }
    }
}
