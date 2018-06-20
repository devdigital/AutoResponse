// <copyright file="SampleTestStartup.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.IntegrationTests.Helpers
{
    using AutoResponse.Sample.WebApi2;

    using global::Owin;

    using WebApiTestServer;

    using Registrations = AutoResponse.Sample.WebApi2.Registrations;

    /// <summary>
    /// Sample test startup.
    /// </summary>
    /// <seealso cref="WebApiTestServer.ITestStartup" />
    public class SampleTestStartup : ITestStartup
    {
        /// <summary>
        /// Bootstraps the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="registrations">The registrations.</param>
        public void Bootstrap(IAppBuilder app, WebApiTestServer.Registrations registrations)
        {
            var domainRegistrations = new Registrations(
                registrations.TypeRegistrations,
                registrations.InstanceRegistrations);

            new Bootstrapper(app, domainRegistrations).Run();
        }
    }
}