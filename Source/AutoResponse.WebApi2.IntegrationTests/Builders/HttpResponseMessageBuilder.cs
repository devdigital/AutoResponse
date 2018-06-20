// <copyright file="HttpResponseMessageBuilder.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.IntegrationTests.Builders
{
    using System.Net;
    using System.Net.Http;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// HTTP response message builder.
    /// </summary>
    public class HttpResponseMessageBuilder
    {
        private HttpStatusCode currentStatusCode;

        private string content;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseMessageBuilder"/> class.
        /// </summary>
        public HttpResponseMessageBuilder()
        {
            this.currentStatusCode = HttpStatusCode.OK;
        }

        /// <summary>
        /// Adds status code.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <returns>The message builder.</returns>
        public HttpResponseMessageBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            this.currentStatusCode = statusCode;
            return this;
        }

        /// <summary>
        /// Adds JSON.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <param name="data">The data.</param>
        /// <returns>The message builder.</returns>
        public HttpResponseMessageBuilder WithJson<TData>(TData data)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };

            this.content = JsonConvert.SerializeObject(data, settings);
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>The HTTP response message.</returns>
        public HttpResponseMessage Build()
        {
            return new HttpResponseMessage(this.currentStatusCode)
            {
                Content = new StringContent(
                    this.content,
                    System.Text.Encoding.UTF8,
                    "application/json"),
            };
        }
    }
}