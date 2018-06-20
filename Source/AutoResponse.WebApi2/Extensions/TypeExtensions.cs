// <copyright file="TypeExtensions.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.Extensions
{
    using System;

    using Newtonsoft.Json;

    /// <summary>
    /// Type extensions.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Converts a type to a description.
        /// TODO: currently uses a crude approach where an instance is serialised
        /// so property values are null. Should use stubbing framework instead to generate instance with real looking stub data
        /// Should also show (optional) for non required fields.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>The description.</returns>
        public static string ToDescription(this Type type, JsonSerializerSettings settings)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            try
            {
                var instance = Activator.CreateInstance(type);
                return JsonConvert.SerializeObject(instance, Formatting.Indented, settings);
            }
            catch
            {
                return null;
            }
        }
    }
}