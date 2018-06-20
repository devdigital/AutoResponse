// <copyright file="StringExtensions.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.Extensions
{
    using System;
    using System.Collections.Generic;

    using Humanizer;

    /// <summary>
    /// String extensions.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Converts a string to an enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The enum value.</returns>
        public static TEnum ToEnum<TEnum>(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (TEnum)Enum.Parse(typeof(TEnum), value.Pascalize());
        }

        /// <summary>
        /// Converts a string to an enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="mappings">The mappings.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The enum.</returns>
        public static TEnum ToEnum<TEnum>(
            this string value,
            IDictionary<string, TEnum> mappings,
            TEnum defaultValue)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (mappings == null)
            {
                throw new ArgumentNullException(nameof(mappings));
            }

            if (defaultValue == null)
            {
                throw new ArgumentNullException(nameof(defaultValue));
            }

            return mappings.ContainsKey(value) ? mappings[value] : defaultValue;
        }
    }
}