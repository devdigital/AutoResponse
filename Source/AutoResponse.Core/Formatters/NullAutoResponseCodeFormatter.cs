// <copyright file="NullAutoResponseCodeFormatter.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Formatters
{
    /// <summary>
    /// Null AutoResponse code formatter.
    /// </summary>
    /// <seealso cref="AutoResponse.Core.Formatters.IAutoResponseCodeFormatter" />
    public class NullAutoResponseCodeFormatter : IAutoResponseCodeFormatter
    {
        /// <inheritdoc />
        public string Format(string code)
        {
            return string.IsNullOrWhiteSpace(code) ? null : code;
        }
    }
}