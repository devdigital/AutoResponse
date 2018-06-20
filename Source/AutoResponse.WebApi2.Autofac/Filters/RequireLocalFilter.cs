// <copyright file="RequireLocalFilter.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.Autofac.Filters
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;

    /// <summary>
    /// Require local filter.
    /// </summary>
    /// <seealso cref="AutoResponse.WebApi2.Autofac.Filters.BaseAuthorizationFilter" />
    public class RequireLocalFilter : BaseAuthorizationFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequireLocalFilter"/> class.
        /// </summary>
        public RequireLocalFilter()
            : base(HttpStatusCode.NotFound, string.Empty)
        {
        }

        /// <inheritdoc />
        protected override Task<bool> IsAuthorized(HttpActionContext actionContext)
        {
            var request = actionContext?.Request;
            return Task.FromResult(request != null && request.IsLocal());
        }
    }
}