namespace AutoResponse.WebApi2.ExceptionHandling
{
    using System;

    using AutoResponse.Core.Mappers;
    using AutoResponse.Core.Responses;
    using AutoResponse.Data.Exceptions;
    using AutoResponse.WebApi2.Extensions;

    using Humanizer;

    public class AutoResponseExceptionHttpResponseMapper : ExceptionHttpResponseMapperBase
    {
        protected override void ConfigureMappings(ExceptionHttpResponseBuilder builder)
        {
            builder.AddMapping<ServiceErrorException>(
                e => new ServiceErrorHttpResponse(e.Message, e.InnerException));

            builder.AddMapping<UnauthenticatedException>(
                e => new UnauthenticatedHttpResponse());

            builder.AddMapping<EntityValidationException>(
                e => new ResourceValidationHttpResponse(e.ErrorDetails.ToValidationErrorDetails()));

            builder.AddMapping<EntityNotFoundException>(
                e => new ResourceNotFoundHttpResponse(e.EntityType.Kebaberize(), e.EntityId));

            builder.AddGenericMapping<IEntityNotFoundException>(
                typeof(EntityNotFoundException<>),
                e => new ResourceNotFoundHttpResponse(e.EntityType.Kebaberize(), e.EntityId));

            builder.AddMapping<EntityCreatePermissionException>(
                e => string.IsNullOrWhiteSpace(e.EntityId) 
                    ? new ResourceCreatePermissionHttpResponse(e.UserId, e.EntityType.Kebaberize())
                    : new ResourceCreatePermissionHttpResponse(e.UserId, e.EntityType.Kebaberize(), e.EntityId));

            builder.AddGenericMapping<IEntityCreatePermissionException>(
                typeof(EntityCreatePermissionException<>),
                e => new ResourceCreatePermissionHttpResponse(e.UserId, e.EntityType.Kebaberize(), e.EntityId));

            builder.AddMapping<EntityPermissionException>(
                e => new ResourcePermissionHttpResponse(e.UserId, e.EntityType.Kebaberize(), e.EntityId));

            builder.AddGenericMapping<IEntityPermissionException>(
                typeof(EntityPermissionException<>),
                e => new ResourcePermissionHttpResponse(e.UserId, e.EntityType.Kebaberize(), e.EntityId));
        }

        public override IHttpResponse GetUnhandledResponse(Exception exception)
        {
            return new ServiceErrorHttpResponse(
                "There was an unhandled exception", exception);
        }
    }
}