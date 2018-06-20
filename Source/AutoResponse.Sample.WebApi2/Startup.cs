// <copyright file="Startup.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AutoResponse.Sample.WebApi2.Startup))]

namespace AutoResponse.Sample.WebApi2
{
    /// <summary>
    /// Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            new Bootstrapper(app).Run();
        }
    }
}
