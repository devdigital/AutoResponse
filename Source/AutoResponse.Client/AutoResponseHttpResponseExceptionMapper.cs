using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoResponse.Core.Dtos;
using AutoResponse.Core.Enums;

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
            : base(new AutoResponseHttpResponseFormatter())
        {            
        }

        public AutoResponseHttpResponseExceptionMapper(IHttpResponseFormatter formatter)
            : base(formatter)
        {
        }

        protected override void ConfigureMappings(HttpResponseExceptionConfiguration configuration)
        {            
            configuration.AddMapping(
                HttpStatusCode.Unauthorized,
                (r, c) => new UnauthenticatedException(
                    c.Formatter.Code(r.Property("code")),
                    c.Formatter.Message(r.Property("message"))));

            configuration.AddMapping(
                HttpStatusCode.Forbidden, "AR403",
                (r, c) => new EntityPermissionException(
                    c.Formatter.Code(r.Property("code")),
                    userId: c.Formatter.EntityProperty(r.Property("userId")),
                    entityType: c.Formatter.EntityType(r.Property("resource")),
                    entityId: c.Formatter.EntityProperty(r.Property("resourceId"))));

            configuration.AddMapping(
                HttpStatusCode.Forbidden, "AR403C",
                (r, c) => new EntityCreatePermissionException(
                    c.Formatter.Code(r.Property("code")),
                    c.Formatter.EntityProperty(r.Property("userId")),
                    c.Formatter.EntityType(r.Property("resource")),
                    c.Formatter.EntityType(r.Property("resourceId"))));

            configuration.AddMapping(
                (HttpStatusCode)422,
                (r, c) => new EntityValidationException(
                    c.Formatter.Code(r.Property("code")),
                    new ValidationErrorDetails(c.Formatter.Message(r.Property("message")),
                    r.Property<IEnumerable<ValidationErrorDto>>("errors").Select(e => new ValidationError(
                        c.Formatter.EntityType(e.Resource),
                        c.Formatter.EntityProperty(e.Field),
                        ToValidationErrorCode(e.Code),
                        c.Formatter.Message(e.Message))))));

            configuration.AddMapping(
                HttpStatusCode.NotFound, "AR404",
                (r, c) => new EntityNotFoundException(
                    c.Formatter.Code(r.Property("code")),
                    entityType: c.Formatter.EntityType(r.Property("resource")),
                    entityId: c.Formatter.EntityProperty(r.Property("resourceId"))));
            
            configuration.AddMapping(
                HttpStatusCode.NotFound, "AR404Q",
                (r, c) => new EntityNotFoundQueryException(
                    c.Formatter.Code(r.Property("code")),
                    c.Formatter.EntityType(r.Property("resource")),
                    r.Property<IEnumerable<QueryParameterDto>>("queryParameters").Select(qp => new QueryParameter(qp.Key, qp.Value))));                        

            configuration.AddMapping(
                HttpStatusCode.InternalServerError,
                (r, c) => new ServiceErrorException(
                    c.Formatter.Code(r.Property("code")),
                    new Exception(c.Formatter.Message(r.Property("message")))));
        }

        private static ValidationErrorCode ToValidationErrorCode(string code)
        {
            return Enum.TryParse(code, ignoreCase: true, result: out ValidationErrorCode result) ? result : ValidationErrorCode.None;
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