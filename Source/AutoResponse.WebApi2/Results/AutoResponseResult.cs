// <copyright file="AutoResponseResult.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.Results
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoResponse.Core.Mappers;

    /// <summary>
    /// AutoResponse result.
    /// </summary>
    public class AutoResponseResult : IHttpActionResult
    {
        private readonly HttpRequestMessage request;

        private readonly object apiEvent;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoResponseResult"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="apiEvent">The API event.</param>
        public AutoResponseResult(HttpRequestMessage request, object apiEvent)
        {
            this.request = request ?? throw new ArgumentNullException(nameof(request));
            this.apiEvent = apiEvent ?? throw new ArgumentNullException(nameof(apiEvent));
        }

        /// <inheritdoc />
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var mapper = this.GetMapper();
            if (mapper == null)
            {
                throw new InvalidOperationException($"No {typeof(IApiEventHttpResponseMapper).Name} registered in dependency resolver");
            }

            var httpResponse = mapper.GetHttpResponse(this.request, this.apiEvent);
            if (httpResponse == null)
            {
                return null;
            }

            var response = this.request.CreateResponse(httpResponse.StatusCode);

            foreach (var keyValuePair in httpResponse.Headers)
            {
                response.Headers.Add(keyValuePair.Key, keyValuePair.Value);
            }

            response.Content = new StringContent(
                httpResponse.Content,
                httpResponse.Encoding,
                httpResponse.ContentType);

            return Task.FromResult(response);
        }

        /// <summary>
        /// Gets the mapper.
        /// </summary>
        /// <returns>The mapper.</returns>
        protected virtual IApiEventHttpResponseMapper GetMapper()
        {
            var dependencyScope = this.request.GetDependencyScope();

            var mapper = dependencyScope?.GetService(typeof(IApiEventHttpResponseMapper)) as
                IApiEventHttpResponseMapper;

            return mapper;
        }
    }
}