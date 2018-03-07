using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AutoResponse.Client
{
    public class RequestContent
    {
        public RequestContent(HttpRequestMessage request, string requestBody)
        {
            this.Request = request;
            this.Body = requestBody;
        }
        
        public HttpRequestMessage Request { get; }

        public string Body { get; }

        public override string ToString()
        {
            if (this.Request == null)
            {
                return "Unknown request.";
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Uri: {this.Request.RequestUri}");
            stringBuilder.AppendLine($"Method: {this.Request.Method.Method}");
            stringBuilder.AppendLine($"Body: {this.Body}");
            return stringBuilder.ToString();
        }
    }
}