namespace AutoResponse.Sample.WebApi2.Factories
{
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Results;

    public class OkActionResultFactory : IHttpActionResultFactory
    {
        public IHttpActionResult Create(HttpRequestMessage request)
        {
            return new OkResult(request);
        }
    }
}