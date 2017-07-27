﻿namespace AutoResponse.Core.Mappers
{
    using System;
    using System.Collections.Generic;

    using AutoResponse.Core.Formatters;
    using AutoResponse.Core.Responses;

    public abstract class ApiEventHttpResponseMapperBase : IApiEventHttpResponseMapper
    {
        private readonly IHttpResponseExceptionFormatter formatter;

        private readonly Lazy<IDictionary<Type, Func<ExceptionHttpResponseContext, object, IHttpResponse>>> mappers;        

        protected ApiEventHttpResponseMapperBase(IHttpResponseExceptionFormatter formatter)
        {
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            this.formatter = formatter;

            this.mappers = new Lazy<IDictionary<Type, Func<ExceptionHttpResponseContext, object, IHttpResponse>>>(() =>
            {
                var mappersInstance = new Dictionary<Type, Func<ExceptionHttpResponseContext, object, IHttpResponse>>();
                this.ConfigureMappings(new ExceptionHttpResponseConfiguration(mappersInstance));
                return mappersInstance;
            });
        }

        protected abstract void ConfigureMappings(ExceptionHttpResponseConfiguration configuration);

        public IHttpResponse GetHttpResponse(object context, object apiEvent)
        {
            if (apiEvent == null)
            {
                throw new ArgumentNullException(nameof(apiEvent));
            }

            var apiEventType = apiEvent.GetType();
            if (apiEventType.IsGenericType)
            {
                apiEventType = apiEventType.GetGenericTypeDefinition();
            }

            if (!this.mappers.Value.ContainsKey(apiEventType))
            {
                return null;
            }

            var mapper = this.mappers.Value[apiEventType];

            return mapper?.Invoke(
                new ExceptionHttpResponseContext(context, this.formatter), 
                apiEvent);
        }
    }
}
