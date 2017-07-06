namespace AutoResponse.Core.Responses
{
    using System.Collections.Generic;
    using System.Text;

    public interface IHttpResponse
    {
        int StatusCode { get; }

        IDictionary<string, string[]> Headers { get; }

        string ContentType { get; }

        string Content { get; }

        Encoding Encoding { get; }
    }
}