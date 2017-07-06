namespace AutoResponse.Core.Mappers
{
    using System;

    using AutoResponse.Core.Responses;

    public interface IExceptionHttpResponseMapper
    {
        IHttpResponse GetHttpResponse(Exception exception);
    }
}