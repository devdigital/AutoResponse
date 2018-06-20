// <copyright file="JsonHttpResponse.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Responses
{
    using System.Collections.Generic;
    using System.Net;
    using System.Text;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// JSON HTTP response.
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    public abstract class JsonHttpResponse<TData> : IHttpResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonHttpResponse{TData}"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="headers">The headers.</param>
        protected JsonHttpResponse(TData data, HttpStatusCode statusCode, IDictionary<string, string[]> headers = null)
        {
            this.StatusCode = statusCode;
            this.Headers = headers ?? new Dictionary<string, string[]>();
            this.ContentType = "application/json";

            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };

            this.Content = JsonConvert.SerializeObject(data, jsonSettings);
            this.ContentLength = Encoding.UTF8.GetByteCount(this.Content);
            this.Encoding = Encoding.UTF8;
        }

        /// <summary>
        /// Gets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets the headers.
        /// </summary>
        /// <value>
        /// The headers.
        /// </value>
        public IDictionary<string, string[]> Headers { get; }

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; }

        /// <summary>
        /// Gets or sets the length of the content.
        /// </summary>
        /// <value>
        /// The length of the content.
        /// </value>
        public long? ContentLength { get; set; }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; }

        /// <summary>
        /// Gets the encoding.
        /// </summary>
        /// <value>
        /// The encoding.
        /// </value>
        public Encoding Encoding { get; }
    }
}