namespace AutoResponse.Core.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;

    using Newtonsoft.Json;

    public abstract class JsonHttpResponse<TData> : IHttpResponse
    {
        public JsonHttpResponse(TData data, HttpStatusCode statusCode, IDictionary<string, string[]> headers = null)
        {
            this.StatusCode = (int)statusCode;
            this.Headers = headers ?? new Dictionary<string, string[]>();
            this.ContentType = "application/json";

            this.Content = JsonConvert.SerializeObject(data);
            this.Encoding = Encoding.UTF8;            
        }

        public int StatusCode { get; }

        public IDictionary<string, string[]> Headers { get; }

        public string ContentType { get; }

        public string Content { get; }

        public Encoding Encoding { get; }
    }
}