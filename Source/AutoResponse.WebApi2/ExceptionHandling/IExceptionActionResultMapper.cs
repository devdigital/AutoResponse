namespace AutoResponse.WebApi2.ExceptionHandling
{
    using System;
    using System.Net.Http;
    using System.Web.Http;

    public interface IExceptionActionResultMapper
    {
        IHttpActionResult Get(HttpRequestMessage request, Exception exception);
    }
}