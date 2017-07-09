namespace AutoResponse.Owin
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    using AutoResponse.Core.Mappers;

    using Microsoft.Owin;

    public class AutoResponseExceptionMiddleware : OwinMiddleware
    {
        private readonly IExceptionHttpResponseMapper mapper;

        public AutoResponseExceptionMiddleware(
            OwinMiddleware next, 
            IExceptionHttpResponseMapper mapper)
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
            try
            {
                await this.Next.Invoke(context);
            }
            catch (Exception exception)
            {
                this.ConvertExceptionToHttpReponse(exception, context);
            }            
        }

        private void ConvertExceptionToHttpReponse(Exception exception, IOwinContext context)
        {
            var httpResponse = this.mapper.GetHttpResponse(exception);
            if (httpResponse == null)
            {
                throw exception;
            }

            context.Response.StatusCode = (int)httpResponse.StatusCode;
            context.Response.ReasonPhrase = this.GetReasonPhrase(httpResponse.StatusCode);

            foreach (var header in httpResponse.Headers)
            {
                context.Response.Headers.Add(header.Key, header.Value);
            }

            context.Response.ContentType = httpResponse.ContentType;
            context.Response.ContentLength = httpResponse.ContentLength;
            context.Response.Write(httpResponse.Content);
        }

        private string GetReasonPhrase(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.BadRequest: return "Bad Request";
                case HttpStatusCode.Unauthorized: return "Unauthorized";
                case HttpStatusCode.PaymentRequired: return "Payment Required";
                case HttpStatusCode.Forbidden: return "Forbidden";
                case HttpStatusCode.NotFound: return "Not Found";
                case HttpStatusCode.MethodNotAllowed: return "Method Not Allowed";
                case HttpStatusCode.NotAcceptable: return "Not Acceptable";
                case HttpStatusCode.ProxyAuthenticationRequired: return "Proxy Authentication Required";
                case HttpStatusCode.RequestTimeout: return "Request Time-out";
                case HttpStatusCode.Conflict: return "Conflict";
                case HttpStatusCode.Gone: return "Gone";
                case HttpStatusCode.LengthRequired: return "Length Required";
                case HttpStatusCode.PreconditionFailed: return "Precondition Failed";
                case HttpStatusCode.RequestEntityTooLarge: return "Request Entity Too Large";
                case HttpStatusCode.RequestUriTooLong: return "Request - URI Too Large";
                case HttpStatusCode.UnsupportedMediaType: return "Unsupported Media Type";
                case HttpStatusCode.RequestedRangeNotSatisfiable: return "Requested range not satisfiable";
                case HttpStatusCode.ExpectationFailed: return "Expectation Failed";
                case HttpStatusCode.InternalServerError: return "Internal Server Error";
                case HttpStatusCode.NotImplemented: return "Not Implemented";
                case HttpStatusCode.BadGateway: return "Bad Gateway";
                case HttpStatusCode.ServiceUnavailable: return "Service Unavailable";
                case HttpStatusCode.GatewayTimeout: return "Gateway Time-out";
                case (HttpStatusCode)418: return "I'm a teapot";
                case (HttpStatusCode)421: return "Misdirected Request";
                case (HttpStatusCode)422: return "Unprocessable Entity";
                case (HttpStatusCode)423: return "Locked";
                case (HttpStatusCode)424: return "Failed Dependency";
                case HttpStatusCode.UpgradeRequired: return "Upgrade Required";
                case (HttpStatusCode)429: return "Too Many Requests";
                case (HttpStatusCode)431: return "Request Header Fields Too Large";
                case (HttpStatusCode)451: return "Unavailable For Legal Reasons";
                default:
                    throw new ArgumentOutOfRangeException(nameof(statusCode), statusCode, "Unexpected HTTP status code when generating reason phrase");
            }
        }
    }
}
