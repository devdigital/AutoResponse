// <copyright file="IHttpActionResultFactory.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Sample.WebApi2.Factories
{
    using System.Net.Http;
    using System.Web.Http;

    /// <summary>
    /// HTTP action result factory.
    /// </summary>
    public interface IHttpActionResultFactory
    {
        /// <summary>
        /// Creates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The HTTP action result.</returns>
        IHttpActionResult Create(HttpRequestMessage request);
    }
}