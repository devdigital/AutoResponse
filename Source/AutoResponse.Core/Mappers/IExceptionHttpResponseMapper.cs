namespace AutoResponse.Core.Mappers
{
    using System;

    using AutoResponse.Core.Responses;

    public interface IExceptionHttpResponseMapper
    {
        IHttpResponse GetHttpResponse(object context, Exception exception);

        IHttpResponse GetUnhandledResponse(object context, Exception exception);
    }
}