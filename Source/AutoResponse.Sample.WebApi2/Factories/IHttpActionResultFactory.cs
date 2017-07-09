namespace AutoResponse.Sample.WebApi2.Factories
{
    using System.Net.Http;
    using System.Web.Http;

    public interface IHttpActionResultFactory
    {
        IHttpActionResult Create(HttpRequestMessage request);
    }
}