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

    public abstract class BaseAuthorizationFilter : IAutofacAuthorizationFilter
    {
        private readonly HttpStatusCode failureStatusCode;

        private readonly string failureText;

        protected BaseAuthorizationFilter(HttpStatusCode failureStatusCode, string failureText = null)
        {
            this.failureStatusCode = failureStatusCode;
            this.failureText = failureText;
        }

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

        protected abstract Task<bool> IsAuthorized(HttpActionContext actionContext);        

        protected virtual Task HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(
                this.failureStatusCode,
                this.failureText ?? "You are not authorized to view this resource");

            return Task.FromResult<object>(null);
        }

        protected bool SkipAuthorization(HttpActionContext actionContext)
        {
            return 
                actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() ||
                actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }
    }
}
