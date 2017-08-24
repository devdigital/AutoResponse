namespace AutoResponse.Core.Mappers
{
    using System;

    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Core.Formatters;
    using AutoResponse.Core.Models;
    using AutoResponse.Core.Responses;

    public class AutoResponseApiEventHttpResponseMapper : ApiEventHttpResponseMapperBase
    {
        private readonly IContextResolver contextResolver;

        public AutoResponseApiEventHttpResponseMapper(IContextResolver contextResolver) 
            : this(contextResolver, new AutoResponseExceptionFormatter())
        {
        }

        public AutoResponseApiEventHttpResponseMapper(
            IContextResolver contextResolver,
            IAutoResponseExceptionFormatter formatter) : base(formatter)
        {
            if (contextResolver == null)
            {
                throw new ArgumentNullException(nameof(contextResolver));
            }

            this.contextResolver = contextResolver;
        }

        protected override void ConfigureMappings(ExceptionHttpResponseConfiguration configuration)
        {
            configuration.AddMapping<ServiceErrorApiEvent>(
                (c, e) =>
                    {
                        if (this.contextResolver.IncludeFullDetails(c.Context))
                        {
                            return
                                new ServiceErrorWithExceptionHttpResponse(
                                    c.Formatter.Message("A service error has occurred"),
                                    c.Formatter.Code(e.Code),
                                    c.Formatter.Message(e.Exception.Message),
                                    c.Formatter.Message(e.Exception.ToString()));                            
                        }

                        return new ServiceErrorHttpResponse(
                                c.Formatter.Message("A service error has occurred"),
                                c.Formatter.Code(e.Code));
                    });

            configuration.AddMapping<UnauthenticatedApiEvent>(
                (c, e) =>
                new UnauthenticatedHttpResponse(
                    c.Formatter.Message(e.Message ?? "The user is not authenticated"),
                    c.Formatter.Code(e.Code)));

            configuration.AddMapping<EntityValidationApiEvent>(
                (c, e) =>
                new ResourceValidationHttpResponse(
                    c.Formatter.Code(e.Code),
                    e.ErrorDetails.ToFormatted(c.Formatter)));

            configuration.AddMapping<EntityNotFoundApiEvent>(ToNotFound);

            configuration.AddMapping<EntityNotFoundQueryApiEvent>(
                (c, e) =>
                new ResourceNotFoundQueryHttpResponse(
                    $"The {c.Formatter.Resource(e.EntityType)} resource was not found with the specified parameters",
                    c.Formatter.Code(e.Code),
                    c.Formatter.Resource(e.EntityType),
                    e.Parameters));

            configuration.AddMapping<EntityCreatePermissionApiEvent>(ToCreatePermission);
            configuration.AddMapping<EntityPermissionApiEvent>(ToPermission);
            configuration.AddMapping<EntityCreatedApiEvent>(ToCreate);                
        }

        private static IHttpResponse ToPermission(
            ExceptionHttpResponseContext configuration,
            EntityPermissionApiEvent apiEvent)
        {
            var userId = configuration.Formatter.Field(apiEvent.UserId);
            var resource = configuration.Formatter.Resource(apiEvent.EntityType);
            var resourceId = configuration.Formatter.Field(apiEvent.EntityId);

            var message = $"The user with identifier '{apiEvent.UserId}', does not have permission to access the {resource} resource with identifier '{resourceId}'";

            return new ResourcePermissionHttpResponse(
                message,
                configuration.Formatter.Code(apiEvent.Code),
                userId,
                resource,
                resourceId);
        }

        private static IHttpResponse ToCreatePermission(
            ExceptionHttpResponseContext configuration, 
            EntityCreatePermissionApiEvent apiEvent)
        {            
            var resource = configuration.Formatter.Resource(apiEvent.EntityType);
            var resourceId = configuration.Formatter.Field(apiEvent.EntityId);

            var message = string.IsNullOrWhiteSpace(resourceId)
                ? $"The user with identifier '{apiEvent.UserId}', does not have permission to create a {resource} resource"
                : $"The user with identifier '{apiEvent.UserId}', does not have permission to create a {resource} resource with resource identifier '{resourceId}'";

            return new ResourceCreatePermissionHttpResponse(
                configuration.Formatter.Message(message),
                configuration.Formatter.Code(apiEvent.Code),
                configuration.Formatter.Field(apiEvent.UserId),
                resource,
                resourceId);
        }

        private static IHttpResponse ToCreate(
            ExceptionHttpResponseContext configuration, 
            EntityCreatedApiEvent apiEvent)
        {
            var resource = configuration.Formatter.Resource(apiEvent.EntityType);

            var message = string.IsNullOrWhiteSpace(apiEvent.EntityId)
                ? $"The user with identifier '{apiEvent.UserId}', created a {resource} resource"
                : $"The user with identifier '{apiEvent.UserId}', created a {resource} resource with resource identifier '{configuration.Formatter.Field(apiEvent.EntityId)}'";

            return new ResourceCreatedHttpResponse(
                configuration.Formatter.Message(message),
                configuration.Formatter.Code(apiEvent.Code),
                configuration.Formatter.Field(apiEvent.EntityId));
        }

        private static IHttpResponse ToNotFound(
            ExceptionHttpResponseContext configuration, 
            EntityNotFoundApiEvent apiEvent)
        {
            var resource = configuration.Formatter.Resource(apiEvent.EntityType);
            var resourceId = configuration.Formatter.Field(apiEvent.EntityId);

            return new ResourceNotFoundHttpResponse(
                $"The {resource} resource with identifier '{resourceId}' was not found.",
                configuration.Formatter.Code(apiEvent.Code),
                resource,
                resourceId);
        }
    }
}