using System.Threading.Tasks;

namespace AutoResponse.Client
{
    using System;
    using System.Net.Http;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class ResponseContent
    {
        private readonly Lazy<string> responseContent;

        public ResponseContent(HttpResponseMessage response)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            this.Response = response;
            this.responseContent = new Lazy<string>(
                () => response.Content.ReadAsStringAsync().Result);
        }

        private readonly Lazy<JObject> json;

        public ResponseContent(HttpResponseMessage response, string responseContent)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            this.Response = response;

            this.responseContent = new Lazy<string>(
                () => responseContent);

            this.json = new Lazy<JObject>(
                () => string.IsNullOrWhiteSpace(responseContent) 
                    ? null 
                    : JObject.Parse(this.responseContent.Value));
        }

        public HttpResponseMessage Response { get; }

        public TData As<TData>() where TData : class
        {
            var content = this.responseContent.Value;
            if (string.IsNullOrWhiteSpace(content))
            {
                return default(TData);
            }

            try
            {
                return JsonConvert.DeserializeObject<TData>(content);
            }
            catch (Exception)
            {
                return default(TData);
            }
        }

        public string Property(string propertyName)
        {
            return this.Property<string>(propertyName);
        }

        public TProperty Property<TProperty>(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(propertyName);
            }

            var content = this.responseContent.Value;
            if (string.IsNullOrWhiteSpace(content))
            {
                return default(TProperty);
            }

            try
            {
                var token = this.json?.Value?.Property(propertyName)?.Value;
                return token == null
                    ? default(TProperty)
                    : token.ToObject<TProperty>();
            }
            catch (Exception)
            {
                return default(TProperty);                
            }
        }

        public string AsString()
        {
            return this.responseContent.Value;
        }
    }
}