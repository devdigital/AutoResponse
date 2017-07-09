namespace AutoResponse.Core.Responses
{
    using System.Collections.Generic;
    using System.Net;
    using System.Text;

    public interface IHttpResponse
    {
        HttpStatusCode StatusCode { get; }

        IDictionary<string, string[]> Headers { get; }

        string ContentType { get; }

        long? ContentLength { get; set; }

        string Content { get; }

        Encoding Encoding { get; }
    }
}