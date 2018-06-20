// <copyright file="RequireAuthenticationFilter.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.Autofac.Filters
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;

    /// <summary>
    /// Require authentication filter.
    /// </summary>
    /// <seealso cref="AutoResponse.WebApi2.Autofac.Filters.BaseAuthorizationFilter" />
    public class RequireAuthenticationFilter : BaseAuthorizationFilter
    {
        private readonly Func<bool> isAuthenticated;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequireAuthenticationFilter"/> class.
        /// </summary>
        /// <param name="isAuthenticated">The is authenticated.</param>
        public RequireAuthenticationFilter(Func<bool> isAuthenticated)
            : base(HttpStatusCode.Unauthorized, "You need to be authenticated to access this API.")
        {
            this.isAuthenticated = isAuthenticated ?? throw new ArgumentNullException(nameof(isAuthenticated));
        }

        /// <inheritdoc />
        protected override Task<bool> IsAuthorized(HttpActionContext actionContext)
        {
            return Task.FromResult(this.isAuthenticated());
        }
    }
}
