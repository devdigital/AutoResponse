namespace AutoResponse.WebApi2.IntegrationTests.Builders
{
    using System.Net;
    using System.Net.Http;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class HttpResponseMessageBuilder
    {
        private HttpStatusCode currentStatusCode;

        private string content;

        public HttpResponseMessageBuilder()
        {
            this.currentStatusCode = HttpStatusCode.OK;
        }

        public HttpResponseMessageBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            this.currentStatusCode = statusCode;
            return this;
        }

        public HttpResponseMessageBuilder WithJson<TData>(TData data)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            this.content = JsonConvert.SerializeObject(data, settings);
            return this;
        }


        public HttpResponseMessage Build()
        {
            return new HttpResponseMessage(this.currentStatusCode)
            {
                Content = new StringContent(
                    this.content, 
                    System.Text.Encoding.UTF8,
                    "application/json")
            };
        }
    }
}