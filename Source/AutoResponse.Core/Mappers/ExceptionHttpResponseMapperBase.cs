namespace AutoResponse.Core.Mappers
{
    using System;
    using System.Collections.Generic;

    using AutoResponse.Core.Responses;

    public abstract class ExceptionHttpResponseMapperBase : IExceptionHttpResponseMapper
    {
        private readonly Lazy<IDictionary<Type, Func<Exception, IHttpResponse>>> mappers;

        protected ExceptionHttpResponseMapperBase()
        {
            this.mappers = new Lazy<IDictionary<Type, Func<Exception, IHttpResponse>>>(() =>
            {
                var mappersInstance = new Dictionary<Type, Func<Exception, IHttpResponse>>();
                this.ConfigureMappings(new ExceptionHttpResponseBuilder(mappersInstance));
                return mappersInstance;
            });
        }

        protected abstract void ConfigureMappings(ExceptionHttpResponseBuilder builder);
       
        public IHttpResponse GetHttpResponse(Exception exception)
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
                return this.GetUnhandledResponse(exception);
            }

            var mapper = this.mappers.Value[exceptionType];
            return mapper == null ? this.GetUnhandledResponse(exception) : mapper.Invoke(exception);
        }

        public abstract IHttpResponse GetUnhandledResponse(Exception exception);
    }
}
