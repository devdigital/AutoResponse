// <copyright file="AutoResponseHttpResponseFormatter.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Client
{
    using Humanizer;

    /// <summary>
    /// AutoResponse HTTP response formatter.
    /// </summary>
    /// <seealso cref="AutoResponse.Client.IAutoResponseHttpResponseFormatter" />
    public class AutoResponseHttpResponseFormatter : IAutoResponseHttpResponseFormatter
    {
        private readonly bool useCamelCase;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoResponseHttpResponseFormatter"/> class.
        /// </summary>
        /// <param name="useCamelCase">if set to <c>true</c> use camel case.</param>
        public AutoResponseHttpResponseFormatter(bool useCamelCase = true)
        {
            this.useCamelCase = useCamelCase;
        }

        /// <inheritdoc />
        public string Message(string message)
        {
            return string.IsNullOrWhiteSpace(message) ? null : message;
        }

        /// <inheritdoc />
        public string EntityType(string resource)
        {
            return string.IsNullOrWhiteSpace(resource)
                ? null
                : this.ConvertCase(resource);
        }

        /// <inheritdoc />
        public string EntityProperty(string field)
        {
            return string.IsNullOrWhiteSpace(field)
                ? null
                : this.ConvertCase(field);
        }

        /// <inheritdoc />
        public string Code(string code)
        {
            return string.IsNullOrWhiteSpace(code) ? null : code;
        }

        private string ConvertCase(string value)
        {
            // If not camel case, assume kebab case
            return this.useCamelCase
                ? value?.Pascalize()
                : value?.Underscore()?.Pascalize();
        }
    }
}