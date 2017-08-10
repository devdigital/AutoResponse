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
            // TODO: build entity fields from response fields
            configuration.AddMapping(
                HttpStatusCode.Unauthorized,
                (r, c) => new UnauthenticatedException(
                    c.Formatter.Message(r.Property("code")),
                    c.Formatter.Message(r.Property("message"))));

            configuration.AddMapping(
                HttpStatusCode.Forbidden,
                (r, c) => new EntityPermissionException(
                    c.Formatter.Message(r.Property("code")),
                    userId: "user",
                    entityType: "entity",
                    entityId: "id"));

            configuration.AddMapping(
                (HttpStatusCode)422,
                (r, c) => new EntityValidationException(
                    c.Formatter.Message(r.Property("code")),
                    new ValidationErrorDetails(c.Formatter.Message(r.Property("message")))));

            configuration.AddMapping(
                HttpStatusCode.NotFound,
                (r, c) => new EntityNotFoundException(
                    c.Formatter.Message(r.Property("code")),
                    entityType: "entity",
                    entityId: "id"));
            
            configuration.AddMapping(
                HttpStatusCode.InternalServerError,
                (r, c) => new ServiceErrorException(
                    c.Formatter.Message(r.Property("code")),
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