// <copyright file="IAutoResponseExceptionFormatter.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Formatters
{
    /// <summary>
    /// AutoResponse exception formatter.
    /// </summary>
    public interface IAutoResponseExceptionFormatter
    {
        /// <summary>
        /// Formats the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The formatted message.</returns>
        string Message(string message);

        /// <summary>
        /// Formats the entity type.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <returns>The resource.</returns>
        string Resource(string entityType);

        /// <summary>
        /// Formats the entity property.
        /// </summary>
        /// <param name="entityProperty">The entity property.</param>
        /// <returns>The field.</returns>
        string Field(string entityProperty);

        /// <summary>
        /// Formats the code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>The formatted code.</returns>
        string Code(string code);
    }
}