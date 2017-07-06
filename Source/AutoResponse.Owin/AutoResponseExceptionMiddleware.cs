namespace AutoResponse.Owin
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using AutoResponse.Core.Mappers;

    using Microsoft.Owin;

    public class AutoResponseExceptionMiddleware : OwinMiddleware
    {
        private readonly IExceptionHttpResponseMapper mapper;

        public AutoResponseExceptionMiddleware(OwinMiddleware next, IExceptionHttpResponseMapper mapper)
            : base(next)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            this.mapper = mapper;
        }

        public override async Task Invoke(IOwinContext context)
        {
            var response = context.Response;

            response.OnSendingHeaders(state =>
            {
                var resp = (OwinResponse)state;
                if (!resp.Environment.ContainsKey("autoresponse.exception"))
                {
                    return;
                }

                var exception = resp.Environment["autoresponse.exception"] as Exception;
                if (exception == null)
                {
                    return;
                }

                var httpContent = this.mapper.GetHttpResponse(exception);
                if (httpContent == null)
                {
                    return;
                }

                resp.StatusCode = httpContent.StatusCode;
                resp.ReasonPhrase = this.GetReasonPhrase(httpContent.StatusCode);

                foreach (var header in httpContent.Headers)
                {
                    resp.Headers.Add(header.Key, header.Value);
                }
                
            }, response);

            try
            {
                await this.Next.Invoke(context);
            }
            catch (Exception exception)
            {                
                context.Environment.Add("autoresponse.exception", exception);

                var httpContent = this.mapper.GetHttpResponse(exception);
                if (httpContent == null)
                {
                    return;
                }

                context.Response.ContentType = httpContent.ContentType;
                context.Response.ContentLength = Encoding.UTF8.GetByteCount(httpContent.Content);

                using (var writer = new StreamWriter(context.Response.Body))
                {
                    await writer.WriteAsync(httpContent.Content);
                }
            }
        }

        private string GetReasonPhrase(int statusCode)
        {
            switch (statusCode)
            {
                case 400: return "Bad Request";
                case 401: return "Unauthorized";
                case 402: return "Payment Required";
                case 403: return "Forbidden";
                case 404: return "Not Found";
                case 405: return "Method Not Allowed";
                case 406: return "Not Acceptable";
                case 407: return "Proxy Authentication Required";
                case 408: return "Request Time-out";
                case 409: return "Conflict";
                case 410: return "Gone";
                case 411: return "Length Required";
                case 412: return "Precondition Failed";
                case 413: return "Request Entity Too Large";
                case 414: return "Request - URI Too Large";
                case 415: return "Unsupported Media Type";
                case 416: return "Requested range not satisfiable";
                case 417: return "Expectation Failed";
                case 500: return "Internal Server Error";
                case 501: return "Not Implemented";
                case 502: return "Bad Gateway";
                case 503: return "Service Unavailable";
                case 504: return "Gateway Time-out";
                default:
                    return string.Empty;
            }
        }
    }
}
