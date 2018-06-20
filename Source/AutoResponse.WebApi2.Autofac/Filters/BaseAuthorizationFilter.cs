// <copyright file="BaseAuthorizationFilter.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.Autofac.Filters
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Controllers;

    using global::Autofac.Integration.WebApi;

    /// <summary>
    /// Base authorization filter.
    /// </summary>
    public abstract class BaseAuthorizationFilter : IAutofacAuthorizationFilter
    {
        private readonly HttpStatusCode failureStatusCode;

        private readonly string failureText;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAuthorizationFilter"/> class.
        /// </summary>
        /// <param name="failureStatusCode">The failure status code.</param>
        /// <param name="failureText">The failure text.</param>
        protected BaseAuthorizationFilter(HttpStatusCode failureStatusCode, string failureText = null)
        {
            this.failureStatusCode = failureStatusCode;
            this.failureText = failureText;
        }

        /// <inheritdoc />
        public async Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (this.SkipAuthorization(actionContext))
            {
                return;
            }

            if (!await this.IsAuthorized(actionContext))
            {
                await this.HandleUnauthorizedRequest(actionContext);
            }
        }

        /// <summary>
        /// Determines whether the specified action context is authorized.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        /// <returns>True if the request is authorized; false otherwise.</returns>
        protected abstract Task<bool> IsAuthorized(HttpActionContext actionContext);

        /// <summary>
        /// Handles an unauthorized request.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        /// <returns>The task.</returns>
        protected virtual Task HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(
                this.failureStatusCode,
                this.failureText ?? "You are not authorized to view this resource");

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Skips the authorization.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        /// <returns>True if the authorization should be skipped; false otherwise.</returns>
        protected bool SkipAuthorization(HttpActionContext actionContext)
        {
            return
                actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() ||
                actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }
    }
}
