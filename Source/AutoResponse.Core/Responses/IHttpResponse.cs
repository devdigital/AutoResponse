namespace AutoResponse.Core.Responses
{
    using System;
    using System.Collections.Generic;

    public interface IHttpResponse
    {
        int StatusCode { get; }

        IDictionary<string, string> Headers { get; }

        string ContentType { get; }

        Type DataType { get; }

        object Data { get; }
    }
}