namespace AutoResponse.Core.Mappers
{
    using System;
    using System.Collections.Generic;

    using AutoResponse.Core.Responses;

    public abstract class ExceptionHttpResponseMapperBase : IExceptionHttpResponseMapper
    {
        private readonly Lazy<IDictionary<Type, Func<object, Exception, IHttpResponse>>> mappers;

        protected ExceptionHttpResponseMapperBase()
        {
            this.mappers = new Lazy<IDictionary<Type, Func<object, Exception, IHttpResponse>>>(() =>
            {
                var mappersInstance = new Dictionary<Type, Func<object, Exception, IHttpResponse>>();
                this.ConfigureMappings(new ExceptionHttpResponseBuilder(mappersInstance));
                return mappersInstance;
            });
        }

        protected abstract void ConfigureMappings(ExceptionHttpResponseBuilder builder);
       
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
                return this.GetUnhandledResponse(context, exception);
            }

            var mapper = this.mappers.Value[exceptionType];
            return mapper == null 
                ? this.GetUnhandledResponse(context, exception) 
                : mapper.Invoke(context, exception);
        }

        public abstract IHttpResponse GetUnhandledResponse(object context, Exception exception);
    }
}
