// <copyright file="IAutoResponseCodeFormatter.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Formatters
{
    /// <summary>
    /// AutoResponse code formatter.
    /// </summary>
    public interface IAutoResponseCodeFormatter
    {
        /// <summary>
        /// Formats the specified code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>The formatted code.</returns>
        string Format(string code);
    }
}