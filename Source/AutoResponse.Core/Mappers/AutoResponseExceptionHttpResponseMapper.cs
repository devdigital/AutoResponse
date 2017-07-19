namespace AutoResponse.Core.Mappers
{
    using System;

    using AutoResponse.Core.Exceptions;
    using AutoResponse.Core.Extensions;
    using AutoResponse.Core.Responses;

    public class AutoResponseExceptionHttpResponseMapper : ExceptionHttpResponseMapperBase
    {
        private readonly IContextResolver contextResolver;

        public AutoResponseExceptionHttpResponseMapper(IContextResolver contextResolver) 
            : this(contextResolver, new DefaultHttpResponseFormatter())
        {
        }

        public AutoResponseExceptionHttpResponseMapper(
            IContextResolver contextResolver,
            IHttpResponseFormatter httpResponseFormatter) : base(httpResponseFormatter)
        {
            if (contextResolver == null)
            {
                throw new ArgumentNullException(nameof(contextResolver));
            }

            this.contextResolver = contextResolver;
        }

        protected override void ConfigureMappings(ExceptionHttpResponseConfiguration configuration)
        {
            var formatter = configuration.HttpResponseFormatter;

            configuration.AddMapping<ServiceErrorException>(
                (c, e) => this.contextResolver.IncludeFullDetails(c)
                    ? (IHttpResponse)new ServiceErrorHttpResponse("A service error has occurred")
                    : (IHttpResponse)new ServiceErrorWithExceptionHttpResponse("A service error has occurred", e.Message, e.ToString()));

            configuration.AddMapping<UnauthenticatedException>(
                (c, e) => new UnauthenticatedHttpResponse());
            
            configuration.AddMapping<EntityValidationException>(
                (c, e) => new ResourceValidationHttpResponse(
                    e.ErrorDetails.ToValidationErrorDetails(formatter)));

            configuration.AddMapping<EntityNotFoundException>(
                (c, e) => new ResourceNotFoundHttpResponse(
                    formatter.EntityTypeToResource(e.EntityType), e.EntityId));

            configuration.AddGenericMapping<IEntityNotFoundException>(
                typeof(EntityNotFoundException<>),
                (c, e) => new ResourceNotFoundHttpResponse(
                    formatter.EntityTypeToResource(e.EntityType), e.EntityId));

            configuration.AddMapping<EntityCreatePermissionException>(
                (c, e) => string.IsNullOrWhiteSpace(e.EntityId) 
                    ? new ResourceCreatePermissionHttpResponse(e.UserId, formatter.EntityTypeToResource(e.EntityType))
                    : new ResourceCreatePermissionHttpResponse(e.UserId, formatter.EntityTypeToResource(e.EntityType), e.EntityId));

            configuration.AddGenericMapping<IEntityCreatePermissionException>(
                typeof(EntityCreatePermissionException<>),
                (c, e) => new ResourceCreatePermissionHttpResponse(e.UserId, formatter.EntityTypeToResource(e.EntityType), e.EntityId));

            configuration.AddMapping<EntityPermissionException>(
                (c, e) => new ResourcePermissionHttpResponse(e.UserId, formatter.EntityTypeToResource(e.EntityType), e.EntityId));

            configuration.AddGenericMapping<IEntityPermissionException>(
                typeof(EntityPermissionException<>),
                (c, e) => new ResourcePermissionHttpResponse(e.UserId, formatter.EntityTypeToResource(e.EntityType), e.EntityId));
        }

        public override IHttpResponse GetUnhandledResponse(
            object context,
            Exception exception,
            IHttpResponseFormatter formatter)
        {
            var includeFullDetails = this.contextResolver.IncludeFullDetails(context);

            if (includeFullDetails)
            {
                return new ServiceErrorWithExceptionHttpResponse(
                    formatter.EntityMessageToResourceMessage("An error has occurred"),
                    formatter.EntityMessageToResourceMessage(exception.Message),
                    formatter.EntityMessageToResourceMessage(exception.ToString()));
            }

            return new ServiceErrorHttpResponse(
                formatter.EntityMessageToResourceMessage("An error has occurred"));
        }
    }
}