// <copyright file="SampleServerFactory.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.IntegrationTests.Helpers
{
    using WebApiTestServer;

    /// <summary>
    /// Sample server factory.
    /// </summary>
    public class SampleServerFactory : TestServerFactory<SampleServerFactory>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleServerFactory"/> class.
        /// </summary>
        public SampleServerFactory()
            : base(new SampleTestStartup())
        {
        }
    }
}