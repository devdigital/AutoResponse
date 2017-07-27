namespace AutoResponse.Core.Mappers
{
    using System;
    using System.Collections.Generic;

    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Core.Responses;

    public class ExceptionHttpResponseConfiguration
    {
        private readonly IDictionary<Type, Func<ExceptionHttpResponseContext, AutoResponseApiEvent, IHttpResponse>> mappers;
        
        public ExceptionHttpResponseConfiguration(IDictionary<Type, Func<ExceptionHttpResponseContext, AutoResponseApiEvent, IHttpResponse>> mappers)
        {   
            if (mappers == null)
            {
                throw new ArgumentNullException(nameof(mappers));
            }

            this.mappers = mappers;
        }

        public void AddMapping<TApiEvent>(
            Func<ExceptionHttpResponseContext, TApiEvent, IHttpResponse> mapper) where TApiEvent : AutoResponseApiEvent
        {
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            if (this.mappers.ContainsKey(typeof(TApiEvent)))
            {
                throw new InvalidOperationException(
                    $"API event type {typeof(TApiEvent).Name} mapper already registered");
            }

            this.mappers.Add(
                typeof(TApiEvent),
                (c, e) => mapper(c, e as TApiEvent));
        }
    }
}