// <copyright file="OkActionResultFactory.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Sample.WebApi2.Factories
{
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Results;

    /// <summary>
    /// OK action result factory.
    /// </summary>
    /// <seealso cref="AutoResponse.Sample.WebApi2.Factories.IHttpActionResultFactory" />
    public class OkActionResultFactory : IHttpActionResultFactory
    {
        /// <inheritdoc />
        public IHttpActionResult Create(HttpRequestMessage request)
        {
            return new OkResult(request);
        }
    }
}