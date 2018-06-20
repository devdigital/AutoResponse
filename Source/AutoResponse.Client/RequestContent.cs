// <copyright file="RequestContent.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Client
{
    using System.Net.Http;
    using System.Text;

    /// <summary>
    /// Request context.
    /// </summary>
    public class RequestContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestContent"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="requestBody">The request body.</param>
        public RequestContent(HttpRequestMessage request, string requestBody)
        {
            this.Request = request;
            this.Body = requestBody;
        }

        /// <summary>
        /// Gets the request.
        /// </summary>
        /// <value>
        /// The request.
        /// </value>
        public HttpRequestMessage Request { get; }

        /// <summary>
        /// Gets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public string Body { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            if (this.Request == null)
            {
                return "Unknown request.";
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Uri: {this.Request.RequestUri}");
            stringBuilder.AppendLine($"Method: {this.Request.Method.Method}");
            stringBuilder.AppendLine($"Body: {this.Body ?? "<unknown>"}");
            return stringBuilder.ToString();
        }
    }
}