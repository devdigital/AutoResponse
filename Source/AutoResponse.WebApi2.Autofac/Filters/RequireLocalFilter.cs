namespace AutoResponse.WebApi2.Autofac.Filters
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;

    public class RequireLocalFilter : BaseAuthorizationFilter
    {
        public RequireLocalFilter()
            : base(HttpStatusCode.NotFound, string.Empty)
        {
        }

        protected override Task<bool> IsAuthorized(HttpActionContext actionContext)
        {            
            var request = actionContext?.Request;
            return Task.FromResult(request != null && request.IsLocal());
        }
    }
}