namespace AutoResponse.Owin
{
    using System;

    using AutoResponse.Core.Mappers;
    using AutoResponse.Core.Responses;
    using AutoResponse.Data.Exceptions;

    using Humanizer;

    public class AutoResponseExceptionHttpResponseMapper : ExceptionHttpResponseMapperBase
    {
        private readonly IContextResolver contextResolver;

        public AutoResponseExceptionHttpResponseMapper(IContextResolver contextResolver)
        {
            if (contextResolver == null)
            {
                throw new ArgumentNullException(nameof(contextResolver));
            }

            this.contextResolver = contextResolver;
        }

        protected override void ConfigureMappings(ExceptionHttpResponseBuilder builder)
        {
            builder.AddMapping<ServiceErrorException>(
                (c, e) => this.contextResolver.IncludeFullDetails(c)
                    ? (IHttpResponse)new ServiceErrorHttpResponse("A service error has occurred")
                    : (IHttpResponse)new ServiceErrorWithExceptionHttpResponse("A service error has occurred", e.Message, e.ToString()));

            builder.AddMapping<UnauthenticatedException>(
                (c, e) => new UnauthenticatedHttpResponse());
            
            builder.AddMapping<EntityValidationException>(
                (c, e) => new ResourceValidationHttpResponse(e.ErrorDetails.ToValidationErrorDetails()));

            builder.AddMapping<EntityNotFoundException>(
                (c, e) => new ResourceNotFoundHttpResponse(e.EntityType.Kebaberize(), e.EntityId));

            builder.AddGenericMapping<IEntityNotFoundException>(
                typeof(EntityNotFoundException<>),
                (c, e) => new ResourceNotFoundHttpResponse(e.EntityType.Kebaberize(), e.EntityId));

            builder.AddMapping<EntityCreatePermissionException>(
                (c, e) => string.IsNullOrWhiteSpace(e.EntityId) 
                    ? new ResourceCreatePermissionHttpResponse(e.UserId, e.EntityType.Kebaberize())
                    : new ResourceCreatePermissionHttpResponse(e.UserId, e.EntityType.Kebaberize(), e.EntityId));

            builder.AddGenericMapping<IEntityCreatePermissionException>(
                typeof(EntityCreatePermissionException<>),
                (c, e) => new ResourceCreatePermissionHttpResponse(e.UserId, e.EntityType.Kebaberize(), e.EntityId));

            builder.AddMapping<EntityPermissionException>(
                (c, e) => new ResourcePermissionHttpResponse(e.UserId, e.EntityType.Kebaberize(), e.EntityId));

            builder.AddGenericMapping<IEntityPermissionException>(
                typeof(EntityPermissionException<>),
                (c, e) => new ResourcePermissionHttpResponse(e.UserId, e.EntityType.Kebaberize(), e.EntityId));
        }

        public override IHttpResponse GetUnhandledResponse(object context, Exception exception)
        {
            var includeFullDetails = this.contextResolver.IncludeFullDetails(context);

            if (includeFullDetails)
            {
                return new ServiceErrorWithExceptionHttpResponse(
                    "An error has occurred",
                    exception.Message,
                    exception.ToString());
            }

            return new ServiceErrorHttpResponse(
                "An error has occurred");            
        }
    }
}