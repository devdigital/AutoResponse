namespace AutoResponse.Client
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface IHttpResponseExceptionMapper
    {
        Task<bool> IsErrorResponse(HttpResponseMessage response);

        Task<Exception> GetException(HttpResponseMessage response);
    }
}