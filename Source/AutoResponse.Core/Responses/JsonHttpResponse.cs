namespace AutoResponse.Core.Responses
{
    using System.Collections.Generic;
    using System.Net;
    using System.Text;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public abstract class JsonHttpResponse<TData> : IHttpResponse
    {
        protected JsonHttpResponse(TData data, HttpStatusCode statusCode, IDictionary<string, string[]> headers = null)
        {
            this.StatusCode = statusCode;
            this.Headers = headers ?? new Dictionary<string, string[]>();
            this.ContentType = "application/json";
            
            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            this.Content = JsonConvert.SerializeObject(data, jsonSettings);
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