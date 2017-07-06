namespace AutoResponse.Core.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    public abstract class JsonHttpResponse<TData> : IHttpResponse
    {
        public JsonHttpResponse(TData data, HttpStatusCode statusCode, IDictionary<string, string> headers = null)
        {
            this.StatusCode = (int)statusCode;
            this.Headers = headers ?? new Dictionary<string, string>();
            this.ContentType = "application/json";
            this.DataType = typeof(TData);
            this.Data = data;
        }

        public int StatusCode { get; }

        public IDictionary<string, string> Headers { get; }

        public string ContentType { get; }

        public Type DataType { get; }

        public object Data { get; }
    }
}