namespace AutoResponse.WebApi2.Extensions
{
    using System;
    using System.Collections.Generic;

    using Humanizer;

    internal static class StringExtensions
    {
        public static TEnum ToEnum<TEnum>(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (TEnum)Enum.Parse(typeof(TEnum), value.Pascalize());            
        }

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