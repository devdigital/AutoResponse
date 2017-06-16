namespace AutoResponse.WebApi2.Autofac.Filters
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;

    public class RequireAuthenticationFilter : BaseAuthorizationFilter
    {
        private readonly Func<bool> isAuthenticated;

        public RequireAuthenticationFilter(Func<bool> isAuthenticated)
            : base(HttpStatusCode.Unauthorized, "You need to be authenticated to access this API.")
        {
            if (isAuthenticated == null)
            {
                throw new ArgumentNullException(nameof(isAuthenticated));
            }

            this.isAuthenticated = isAuthenticated;
        }

        protected override Task<bool> IsAuthorized(HttpActionContext actionContext)
        {
            return Task.FromResult(this.isAuthenticated());
        }
    }
}
