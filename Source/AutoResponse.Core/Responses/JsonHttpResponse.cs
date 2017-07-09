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
            this.StatusCode = statusCode;
            this.Headers = headers ?? new Dictionary<string, string[]>();
            this.ContentType = "application/json";

            this.Content = JsonConvert.SerializeObject(data);
            this.ContentLength = Encoding.UTF8.GetByteCount(this.Content);
            this.Encoding = Encoding.UTF8;            
        }
        
        public HttpStatusCode StatusCode { get; }

        public IDictionary<string, string[]> Headers { get; }

        public string ContentType { get; }

        public long? ContentLength { get; set; }

        public string Content { get; }

        public Encoding Encoding { get; }
    }
}