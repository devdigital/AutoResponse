namespace AutoResponse.Core.Mappers
{
    using System;
    using System.Collections.Generic;

    using AutoResponse.Core.Responses;

    public class ExceptionHttpResponseConfiguration
    {
        private readonly IDictionary<Type, Func<MappingConfiguration, Exception, IHttpResponse>> mappers;
        
        public ExceptionHttpResponseConfiguration(IDictionary<Type, Func<MappingConfiguration, Exception, IHttpResponse>> mappers)
        {
            if (mappers == null)
            {
                throw new ArgumentNullException(nameof(mappers));
            }

            this.mappers = mappers;
        }

        public void AddMapping<TException>(
            Func<MappingConfiguration, TException, IHttpResponse> mapper) where TException : Exception
        {
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            if (this.mappers.ContainsKey(typeof(TException)))
            {
                throw new InvalidOperationException(
                    $"Exception type {typeof(TException).Name} mapper already registered");
            }

            this.mappers.Add(
                typeof(TException),
                (c, e) => mapper(c, e as TException));
        }

        public void AddGenericMapping<TExceptionInterface>(
            Type exceptionType,
            Func<MappingConfiguration, TExceptionInterface, IHttpResponse> mapper) where TExceptionInterface : class
        {
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            if (this.mappers.ContainsKey(exceptionType))
            {
                throw new InvalidOperationException(
                    $"Exception type {exceptionType.Name} mapper already registered");
            }

            this.mappers.Add(
                exceptionType,
                (c, e) => mapper(c, e as TExceptionInterface));
        }
    }
}