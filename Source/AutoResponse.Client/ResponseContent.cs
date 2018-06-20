// <copyright file="ResponseContent.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Client
{
    using System;
    using System.Net.Http;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Response content.
    /// </summary>
    public class ResponseContent
    {
        private readonly string content;

        private readonly Lazy<JObject> json;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseContent"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="responseContent">Content of the response.</param>
        public ResponseContent(HttpResponseMessage response, string responseContent)
        {
            this.Response = response ?? throw new ArgumentNullException(nameof(response));
            this.content = responseContent;
            this.json = new Lazy<JObject>(() => JObject.Parse(this.content));
        }

        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        public HttpResponseMessage Response { get; }

        /// <summary>
        /// Converts to the specified type.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <returns>Instance of the type.</returns>
        public TData As<TData>()
            where TData : class
        {
            if (string.IsNullOrWhiteSpace(this.content))
            {
                return default(TData);
            }

            try
            {
                return JsonConvert.DeserializeObject<TData>(this.content);
            }
            catch (Exception)
            {
                return default(TData);
            }
        }

        /// <summary>
        /// Gets a JSON property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>The property value.</returns>
        public string Property(string propertyName)
        {
            return this.Property<string>(propertyName);
        }

        /// <summary>
        /// Gets a JSON property as a type.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Instance of the type.</returns>
        public TProperty Property<TProperty>(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(propertyName);
            }

            if (string.IsNullOrWhiteSpace(this.content))
            {
                return default(TProperty);
            }

            try
            {
                var token = this.json?.Value?.Property(propertyName)?.Value;
                return token == null
                    ? default(TProperty)
                    : token.ToObject<TProperty>();
            }
            catch (Exception)
            {
                return default(TProperty);
            }
        }

        /// <summary>
        /// Gets the response as a string.
        /// </summary>
        /// <returns>The response string.</returns>
        public string AsString()
        {
            return this.content;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Status Code: {this.Response.StatusCode}");
            stringBuilder.AppendLine($"Body: {this.AsString() ?? "<unknown>"}");
            return stringBuilder.ToString();
        }
    }
}