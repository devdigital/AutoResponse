// <copyright file="AutoResponseExceptionFormatter.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Formatters
{
    using System;

    using Humanizer;

    /// <summary>
    /// AutoResponse exception formatter.
    /// </summary>
    /// <seealso cref="AutoResponse.Core.Formatters.IAutoResponseExceptionFormatter" />
    public class AutoResponseExceptionFormatter : IAutoResponseExceptionFormatter
    {
        private readonly string[] postFixes;

        private readonly IAutoResponseCodeFormatter autoResponseCodeFormatter;

        private readonly bool useCamelCase;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoResponseExceptionFormatter"/> class.
        /// </summary>
        /// <param name="useCamelCase">if set to <c>true</c> use camel case.</param>
        public AutoResponseExceptionFormatter(bool useCamelCase = true)
        {
            this.postFixes = new[] { "ApiModel", "Dto" };
            this.autoResponseCodeFormatter = new NullAutoResponseCodeFormatter();
            this.useCamelCase = useCamelCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoResponseExceptionFormatter"/> class.
        /// </summary>
        /// <param name="autoResponseCodeFormatter">The automatic response code formatter.</param>
        /// <param name="useCamelCase">if set to <c>true</c> [use camel case].</param>
        public AutoResponseExceptionFormatter(IAutoResponseCodeFormatter autoResponseCodeFormatter, bool useCamelCase = true)
        {
            this.autoResponseCodeFormatter = autoResponseCodeFormatter ?? throw new ArgumentNullException(nameof(autoResponseCodeFormatter));
            this.useCamelCase = useCamelCase;
        }

        /// <summary>
        /// Converts a message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The converted messge.</returns>
        public string Message(string message)
        {
            return string.IsNullOrWhiteSpace(message) ? null : message;
        }

        /// <summary>
        /// Converts an entity type to a resource.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <returns>The resource.</returns>
        public string Resource(string entityType)
        {
            if (string.IsNullOrWhiteSpace(entityType))
            {
                return null;
            }

            foreach (var postFix in this.postFixes)
            {
                if (!entityType.EndsWith(postFix))
                {
                    continue;
                }

                if (string.Compare(entityType, postFix, StringComparison.Ordinal) != 0)
                {
                    entityType = entityType.Substring(0, entityType.Length - postFix.Length);
                }

                break;
            }

            return string.IsNullOrWhiteSpace(entityType) ? null : this.ConvertCase(entityType);
        }

        /// <summary>
        /// Converts an entity property to a field.
        /// </summary>
        /// <param name="entityProperty">The entity property.</param>
        /// <returns>The field.</returns>
        public string Field(string entityProperty)
        {
            return string.IsNullOrWhiteSpace(entityProperty)
                ? null
                : this.ConvertCase(entityProperty);
        }

        /// <summary>
        /// Converts a code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>The converted code.</returns>
        public string Code(string code)
        {
            return this.autoResponseCodeFormatter.Format(code);
        }

        private string ConvertCase(string value)
        {
            return this.useCamelCase
                ? value?.Camelize()
                : value?.Kebaberize();
        }
    }
}