﻿namespace AutoResponse.Owin
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    using AutoResponse.Core.Exceptions;
    using AutoResponse.Core.Logging;
    using AutoResponse.Core.Mappers;
    using AutoResponse.Core.Responses;

    using Microsoft.Owin;

    public class AutoResponseExceptionMiddleware : OwinMiddleware
    {
        private readonly IApiEventHttpResponseMapper mapper;

        private readonly IAutoResponseLogger logger;

        private readonly string domainResultPropertyName;

        public AutoResponseExceptionMiddleware(
            OwinMiddleware next, 
            IApiEventHttpResponseMapper mapper,
            IAutoResponseLogger logger,
            string domainResultPropertyName)
            : base(next)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            if (string.IsNullOrWhiteSpace(domainResultPropertyName))
            {
                throw new ArgumentNullException(nameof(domainResultPropertyName));
            }

            this.mapper = mapper;
            this.logger = logger;
            this.domainResultPropertyName = domainResultPropertyName;
        }

        public override async Task Invoke(IOwinContext context)
        {
            try
            {
                await this.Next.Invoke(context);
            }
            catch (AutoResponseException exception)
            {
                await this.logger.LogException(exception);
                this.ConvertExceptionToHttpResponse(exception, exception.EventObject, context);
            }
            catch (Exception exception)
            {
                await this.logger.LogException(exception);

                var exceptionEvent = 
                    exception.GetType().GetProperty(this.domainResultPropertyName)?.GetValue(exception, null);

                if (exceptionEvent != null)
                {
                    this.ConvertExceptionToHttpResponse(exception, exceptionEvent, context);
                    return;
                }

                throw;
            }
        }

        private void ConvertExceptionToHttpResponse(Exception exception, object apiEvent, IOwinContext context)
        {
            var httpResponse = this.mapper.GetHttpResponse(context: null, apiEvent: apiEvent); 
            if (httpResponse == null)
            {
                throw exception;
            }

            this.ConvertHttpResponseToResponse(httpResponse, context);
        }

        private void ConvertHttpResponseToResponse(IHttpResponse httpResponse, IOwinContext context)
        {
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
