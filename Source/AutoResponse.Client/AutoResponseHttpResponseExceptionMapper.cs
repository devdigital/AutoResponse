namespace AutoResponse.Client
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    using AutoResponse.Core.Exceptions;
    using AutoResponse.Core.Models;

    public class AutoResponseHttpResponseExceptionMapper : AutoResponseHttpResponseExceptionMapperBase
    {
        public AutoResponseHttpResponseExceptionMapper() 
            : base(new AutoResponseExceptionHttpResponseFormatter())
        {            
        }

        public AutoResponseHttpResponseExceptionMapper(IExceptionHttpResponseFormatter formatter)
            : base(formatter)
        {
        }

        protected override void ConfigureMappings(HttpResponseExceptionConfiguration configuration)
        {   
            // TODO: more data copying over from responses
            // also difference between create permission response and permission response
            configuration.AddMapping(
                HttpStatusCode.Unauthorized,
                (r, c) => new UnauthenticatedException(
                    c.Formatter.Message(r.Property("message"))));

            configuration.AddMapping(
                HttpStatusCode.Forbidden,
                (r, c) => new EntityPermissionException(
                    c.Formatter.Message(r.Property("message"))));

            configuration.AddMapping(
                (HttpStatusCode)422,
                (r, c) => new EntityValidationException(
                    new ValidationErrorDetails(
                        c.Formatter.Message(r.Property("message")))));

            configuration.AddMapping(
                HttpStatusCode.NotFound,
                (r, c) => new EntityNotFoundException(
                    c.Formatter.Message(r.Property("message"))));
            
            configuration.AddMapping(
                HttpStatusCode.InternalServerError,
                (r, c) => new ServiceErrorException(
                    new Exception(c.Formatter.Message(r.Property("message")))));
        }

        protected override Task<Exception> GetDefaultException(
            ResponseContent responseContent, 
            HttpResponseExceptionContext context)
        {
            // TODO: exception details if include in response etc.
            var exceptionMessage = 
                context.Formatter.Message(responseContent.Property("message"));

            return Task.FromResult(new Exception(exceptionMessage));
        }
    }
}