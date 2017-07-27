﻿namespace AutoResponse.Core.Mappers
{
    using System;

    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Core.Extensions;
    using AutoResponse.Core.Formatters;
    using AutoResponse.Core.Models;
    using AutoResponse.Core.Responses;

    public class AutoResponseApiEventHttpResponseMapper : ApiEventHttpResponseMapperBase
    {
        private readonly IContextResolver contextResolver;

        public AutoResponseApiEventHttpResponseMapper(IContextResolver contextResolver) 
            : this(contextResolver, new AutoResponseExceptionHttpResponseFormatter())
        {
        }

        public AutoResponseApiEventHttpResponseMapper(
            IContextResolver contextResolver,
            IHttpResponseExceptionFormatter formatter) : base(formatter)
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
                            return new ServiceErrorHttpResponse(
                                c.Formatter.Message("A service error has occurred"),
                                c.Formatter.ApiEventToCode(e));
                        }

                        return
                            new ServiceErrorWithExceptionHttpResponse(
                                c.Formatter.Message("A service error has occurred"),
                                c.Formatter.ApiEventToCode(e),
                                c.Formatter.Message(e.Exception.Message),
                                c.Formatter.Message(e.Exception.ToString()));
                    });

            configuration.AddMapping<UnauthenticatedApiEvent>(
                (c, e) =>
                new UnauthenticatedHttpResponse(
                    c.Formatter.Message(e.Message ?? "The user is not authenticated"),
                    c.Formatter.ApiEventToCode(e)));

            configuration.AddMapping<EntityValidationApiEvent>(
                (c, e) =>
                new ResourceValidationHttpResponse(
                    c.Formatter.ApiEventToCode(e),
                    e.ErrorDetails.ToFormatted(c.Formatter)));

            configuration.AddMapping<EntityNotFoundApiEvent>(this.ToNotFound);

            configuration.AddMapping<EntityNotFoundQueryApiEvent>(
                (c, e) =>
                new ResourceNotFoundQueryHttpResponse(
                    $"The {c.Formatter.Resource(e.EntityType)} resource was not found with the specified parameters",
                    c.Formatter.ApiEventToCode(e),
                    c.Formatter.Resource(e.EntityType),
                    e.Parameters));

            configuration.AddMapping<EntityCreatePermissionApiEvent>(this.ToCreatePermission);
            configuration.AddMapping<EntityPermissionApiEvent>(this.ToPermission);
            configuration.AddMapping<EntityCreatedApiEvent>(this.ToCreate);                
        }

        private IHttpResponse ToPermission(
            ExceptionHttpResponseContext configuration,
            EntityPermissionApiEvent apiEvent)
        {
            var resourceType = configuration.Formatter.Resource(apiEvent.EntityType);
            var resourceId = configuration.Formatter.Field(apiEvent.EntityId);

            var message = apiEvent.Message ??
                $"The user with identifier '{apiEvent.UserId}', does not have permission to access the {resourceType} resource with identifier '{resourceId}'";

            return new ResourcePermissionHttpResponse(
                message,
                configuration.Formatter.ApiEventToCode(apiEvent));
        }

        private IHttpResponse ToCreatePermission(
            ExceptionHttpResponseContext configuration, 
            EntityCreatePermissionApiEvent apiEvent)
        {            
            var resource = configuration.Formatter.Resource(apiEvent.EntityType);            

            var message = string.IsNullOrWhiteSpace(apiEvent.EntityId)
                ? $"The user with identifier '{apiEvent.UserId}', does not have permission to create a {resource} resource"
                : $"The user with identifier '{apiEvent.UserId}', does not have permission to create a {resource} resource with resource identifier '{configuration.Formatter.Field(apiEvent.EntityId)}'";

            return new ResourceCreatePermissionHttpResponse(
                configuration.Formatter.Message(message),
                configuration.Formatter.ApiEventToCode(apiEvent));
        }

        private IHttpResponse ToCreate(
            ExceptionHttpResponseContext configuration, 
            EntityCreatedApiEvent apiEvent)
        {
            var resource = configuration.Formatter.Resource(apiEvent.EntityType);

            var message = string.IsNullOrWhiteSpace(apiEvent.EntityId)
                ? $"The user with identifier '{apiEvent.UserId}', created a {resource} resource"
                : $"The user with identifier '{apiEvent.UserId}', created a {resource} resource with resource identifier '{configuration.Formatter.Field(apiEvent.EntityId)}'";

            // TODO: entityId is optional in the api event, but the response will always include id
            return new ResourceCreatedHttpResponse(
                configuration.Formatter.Message(message),
                configuration.Formatter.ApiEventToCode(apiEvent),
                configuration.Formatter.Field(apiEvent.EntityId));
        }

        private IHttpResponse ToNotFound(
            ExceptionHttpResponseContext configuration, 
            EntityNotFoundApiEvent apiEvent)
        {
            var resource = configuration.Formatter.Resource(apiEvent.EntityType);
            var resourceId = configuration.Formatter.Field(apiEvent.EntityId);

            return new ResourceNotFoundHttpResponse(
                $"The {resource} resource with identifier '{resourceId}' was not found.",
                configuration.Formatter.ApiEventToCode(apiEvent),
                configuration.Formatter.Resource(apiEvent.EntityType));
        }        
    }
}