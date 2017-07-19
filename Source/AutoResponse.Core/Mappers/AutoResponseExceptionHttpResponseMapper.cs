namespace AutoResponse.Core.Mappers
{
    using System;

    using AutoResponse.Core.Exceptions;
    using AutoResponse.Core.Extensions;
    using AutoResponse.Core.Formatters;
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
            IHttpResponseFormatter formatter) : base(formatter)
        {
            if (contextResolver == null)
            {
                throw new ArgumentNullException(nameof(contextResolver));
            }

            this.contextResolver = contextResolver;
        }

        protected override void ConfigureMappings(ExceptionHttpResponseConfiguration configuration)
        {
            configuration.AddMapping<ServiceErrorException>(
                (c, e) => this.contextResolver.IncludeFullDetails(c.Context)
                    ? new ServiceErrorHttpResponse(c.Formatter.EntityMessageToResourceMessage("A service error has occurred")) as IHttpResponse
                    : new ServiceErrorWithExceptionHttpResponse(
                        c.Formatter.EntityMessageToResourceMessage("A service error has occurred"),
                        c.Formatter.EntityMessageToResourceMessage(e.Message),
                        c.Formatter.EntityMessageToResourceMessage(e.ToString())));

            configuration.AddMapping<UnauthenticatedException>(
                (c, e) => new UnauthenticatedHttpResponse());
            
            configuration.AddMapping<EntityValidationException>(
                (c, e) => new ResourceValidationHttpResponse(
                    e.ErrorDetails.ToValidationErrorDetails(c.Formatter)));

            configuration.AddMapping<EntityNotFoundException>(
                (c, e) => new ResourceNotFoundHttpResponse(
                    c.Formatter.EntityTypeToResource(e.EntityType), e.EntityId));

            configuration.AddGenericMapping<IEntityNotFoundException>(
                typeof(EntityNotFoundException<>),
                (c, e) => new ResourceNotFoundHttpResponse(
                    c.Formatter.EntityTypeToResource(e.EntityType), e.EntityId));

            configuration.AddMapping<EntityNotFoundQueryException>(
                (c, e) => new ResourceNotFoundQueryHttpResponse(
                    c.Formatter.EntityTypeToResource(e.EntityType), e.Parameters));

            configuration.AddMapping<EntityCreatePermissionException>(
                (c, e) => string.IsNullOrWhiteSpace(e.EntityId) 
                    ? new ResourceCreatePermissionHttpResponse(e.UserId, c.Formatter.EntityTypeToResource(e.EntityType))
                    : new ResourceCreatePermissionHttpResponse(e.UserId, c.Formatter.EntityTypeToResource(e.EntityType), e.EntityId));

            configuration.AddGenericMapping<IEntityCreatePermissionException>(
                typeof(EntityCreatePermissionException<>),
                (c, e) => new ResourceCreatePermissionHttpResponse(e.UserId, c.Formatter.EntityTypeToResource(e.EntityType), e.EntityId));

            configuration.AddMapping<EntityPermissionException>(
                (c, e) => string.IsNullOrWhiteSpace(e.Message) 
                    ? new ResourcePermissionHttpResponse(e.UserId, c.Formatter.EntityTypeToResource(e.EntityType), e.EntityId)
                    : new ResourcePermissionHttpResponse(e.Message));

            configuration.AddGenericMapping<IEntityPermissionException>(
                typeof(EntityPermissionException<>),
                (c, e) => new ResourcePermissionHttpResponse(e.UserId, c.Formatter.EntityTypeToResource(e.EntityType), e.EntityId));
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