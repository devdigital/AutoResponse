// <copyright file="OwinTestHttpClientFactory.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.IntegrationTests.Helpers
{
    using System.Net.Http;
    using Flurl.Http.Configuration;
    using Microsoft.Owin.Testing;

    /// <summary>
    /// OWIN test HTTP client factory.
    /// Adapted from https://stackoverflow.com/a/33128757/248164.
    /// </summary>
    /// <seealso cref="Flurl.Http.Configuration.DefaultHttpClientFactory" />
    public class OwinTestHttpClientFactory : DefaultHttpClientFactory
    {
        private readonly TestServer testServer;

        /// <summary>
        /// Initializes a new instance of the <see cref="OwinTestHttpClientFactory"/> class.
        /// </summary>
        /// <param name="server">The server.</param>
        public OwinTestHttpClientFactory(TestServer server)
        {
            this.testServer = server;
        }

        /// <inheritdoc />
        public override HttpMessageHandler CreateMessageHandler()
        {
            return this.testServer.Handler;
        }
    }
}