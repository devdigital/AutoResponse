namespace AutoResponse.Client
{
    using System;
    using System.Net.Http;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class ResponseContent
    {
        private readonly string content;

        public ResponseContent(HttpResponseMessage response, string responseContent)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            this.Response = response;
            this.content = responseContent;
        }

        public HttpResponseMessage Response { get; }

        public TData As<TData>() where TData : class
        {
            if (string.IsNullOrWhiteSpace(this.content))
            {
                return default(TData);
            }

            try
            {
                return JsonConvert.DeserializeObject<TData>(this.content);
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

            if (string.IsNullOrWhiteSpace(this.content))
            {
                return default(TProperty);
            }

            try
            {
                var jobject = JObject.Parse(this.content);
                var token = jobject?.Property(propertyName)?.Value;
                return token == null 
                    ? default(TProperty) 
                    : token.Value<TProperty>();
            }
            catch (Exception)
            {
                return default(TProperty);                
            }
        }

        public string AsString()
        {
            return this.content;
        }
    }
}