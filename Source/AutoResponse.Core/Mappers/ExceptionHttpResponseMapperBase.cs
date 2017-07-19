namespace AutoResponse.Core.Mappers
{
    using System;
    using System.Collections.Generic;

    using AutoResponse.Core.Responses;

    public abstract class ExceptionHttpResponseMapperBase : IExceptionHttpResponseMapper
    {
        private readonly IHttpResponseFormatter formatter;

        private readonly Lazy<IDictionary<Type, Func<MappingConfiguration, Exception, IHttpResponse>>> mappers;        

        protected ExceptionHttpResponseMapperBase(IHttpResponseFormatter formatter)
        {
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            this.formatter = formatter;

            this.mappers = new Lazy<IDictionary<Type, Func<MappingConfiguration, Exception, IHttpResponse>>>(() =>
            {
                var mappersInstance = new Dictionary<Type, Func<MappingConfiguration, Exception, IHttpResponse>>();
                this.ConfigureMappings(new ExceptionHttpResponseConfiguration(mappersInstance));
                return mappersInstance;
            });
        }

        protected abstract void ConfigureMappings(ExceptionHttpResponseConfiguration configuration);
       
        public IHttpResponse GetHttpResponse(object context, Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            var exceptionType = exception.GetType();
            if (exceptionType.IsGenericType)
            {
                exceptionType = exceptionType.GetGenericTypeDefinition();
            }

            if (!this.mappers.Value.ContainsKey(exceptionType))
            {
                return this.GetUnhandledResponse(context, exception, this.formatter);
            }

            var mapper = this.mappers.Value[exceptionType];
            return mapper == null 
                ? this.GetUnhandledResponse(context, exception, this.formatter) 
                : mapper.Invoke(new MappingConfiguration(context, this.formatter), exception);
        }

        public abstract IHttpResponse GetUnhandledResponse(
            object context,
            Exception exception,
            IHttpResponseFormatter formatter);
    }
}
