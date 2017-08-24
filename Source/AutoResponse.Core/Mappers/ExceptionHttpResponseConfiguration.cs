namespace AutoResponse.Core.Mappers
{
    using System;
    using System.Collections.Generic;

    using AutoResponse.Core.Responses;

    public class ExceptionHttpResponseConfiguration
    {
        private readonly IDictionary<Type, Func<ExceptionHttpResponseContext, object, IHttpResponse>> mappings;
        
        public ExceptionHttpResponseConfiguration(IDictionary<Type, Func<ExceptionHttpResponseContext, object, IHttpResponse>> mappings)
        {   
            if (mappings == null)
            {
                throw new ArgumentNullException(nameof(mappings));
            }

            this.mappings = mappings;
        }

        public void AddMapping<TApiEvent>(
            Func<ExceptionHttpResponseContext, TApiEvent, IHttpResponse> mapping) where TApiEvent : class
        {
            if (mapping == null)
            {
                throw new ArgumentNullException(nameof(mapping));
            }

            if (this.mappings.ContainsKey(typeof(TApiEvent)))
            {
                throw new InvalidOperationException(
                    $"API event type {typeof(TApiEvent).Name} mapping already registered");
            }

            this.mappings.Add(
                typeof(TApiEvent),
                (c, e) => mapping(c, e as TApiEvent));
        }

        public void UpdateMapping<TApiEvent>(Func<ExceptionHttpResponseContext, TApiEvent, IHttpResponse> mapping)
            where TApiEvent : class
        {
            if (mapping == null)
            {
                throw new ArgumentNullException(nameof(mapping));
            }

            if (!this.mappings.ContainsKey(typeof(TApiEvent)))
            {
                throw new InvalidOperationException(
                    $"API event type {typeof(TApiEvent).Name} mapping is not registered");
            }

            this.mappings[typeof(TApiEvent)] = (c, e) => mapping(c, e as TApiEvent);
        }

        public void RemoveMapping<TApiEvent>()
        {
            if (!this.mappings.ContainsKey(typeof(TApiEvent)))
            {
                throw new InvalidOperationException(
                    $"API event type {typeof(TApiEvent).Name} mapping is not registered");
            }

            this.mappings.Remove(typeof(TApiEvent));
        }
    }
}