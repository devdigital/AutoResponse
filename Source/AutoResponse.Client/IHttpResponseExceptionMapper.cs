namespace AutoResponse.Client
{
    using System;
    using System.Net.Http;

    public interface IHttpResponseExceptionMapper
    {
        bool IsErrorResponse(HttpResponseMessage response);

        Exception GetException(HttpResponseMessage response);

        Exception GetDefaultException(HttpResponseMessage response);
    }
}