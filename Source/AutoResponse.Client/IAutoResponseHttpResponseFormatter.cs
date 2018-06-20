// <copyright file="IAutoResponseHttpResponseFormatter.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Client
{
    /// <summary>
    /// AutoResponse HTTP response formatter.
    /// </summary>
    public interface IAutoResponseHttpResponseFormatter
    {
        /// <summary>
        /// Formats the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The formatted message.</returns>
        string Message(string message);

        /// <summary>
        /// Formats the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns>The entity type.</returns>
        string EntityType(string resource);

        /// <summary>
        /// Formats the field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>The entity property.</returns>
        string EntityProperty(string field);

        /// <summary>
        /// Formats the code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>The formatted code.</returns>
        string Code(string code);
    }
}