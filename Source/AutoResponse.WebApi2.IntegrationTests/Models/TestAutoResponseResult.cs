// <copyright file="TestAutoResponseResult.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.IntegrationTests.Models
{
    using System.Net.Http;

    using AutoResponse.Core.Mappers;
    using AutoResponse.WebApi2.ExceptionHandling;
    using AutoResponse.WebApi2.Results;

    /// <summary>
    /// Test AutoResponse result.
    /// </summary>
    /// <seealso cref="AutoResponse.WebApi2.Results.AutoResponseResult" />
    public class TestAutoResponseResult : AutoResponseResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestAutoResponseResult"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="apiEvent">The API event.</param>
        public TestAutoResponseResult(HttpRequestMessage request, object apiEvent)
            : base(request, apiEvent)
        {
        }

        /// <inheritdoc />
        protected override IApiEventHttpResponseMapper GetMapper()
        {
            return new AutoResponseApiEventHttpResponseMapper(
                       new WebApiContextResolver());
        }
    }
}