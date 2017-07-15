//namespace AutoResponse.WebApi2.Results
//{
//    using System;
//    using System.Net;
//    using System.Net.Http;
//    using System.Threading;
//    using System.Threading.Tasks;
//    using System.Web.Http;

//    using AutoResponse.Core.Dtos;
//    using AutoResponse.Core.Models;

//    public class ServiceErrorResult : IHttpActionResult
//    {
//        private readonly HttpRequestMessage request;
        
//        private readonly string message;

//        private readonly Exception exception;

//        public ServiceErrorResult(HttpRequestMessage request, string message)
//        {
//            if (request == null)
//            {
//                throw new ArgumentNullException(nameof(request));
//            }

//            if (string.IsNullOrWhiteSpace(message))
//            {
//                throw new ArgumentNullException(nameof(message));
//            }

//            this.request = request;
//            this.message = message;
//        }

//        public ServiceErrorResult(HttpRequestMessage request, Exception exception)
//        {
//            if (request == null)
//            {
//                throw new ArgumentNullException(nameof(request));
//            }

//            if (exception == null)
//            {
//                throw new ArgumentNullException(nameof(exception));
//            }

//            this.request = request;
//            this.exception = exception;
//        }

//        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
//        {
//            var includeError = this.request.ShouldIncludeErrorDetail();
//            if (!includeError || this.exception == null)
//            {
//                return this.request.CreateResponse(
//                    HttpStatusCode.InternalServerError,
//                    new ErrorDto { Message = this.message });            
//            }

//            return this.request.CreateResponse(
//                HttpStatusCode.InternalServerError,
//                new ErrorWithExceptionDto
//                    {
//                        Message = this.exception.ToString(),
//                        StackTrace = this.exception.StackTrace        
//                    });
//        }
//    }
//}