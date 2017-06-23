namespace AutoResponse.WebApi2.ExceptionHandling
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Http;

    public abstract class ExceptionActionResultMapperBase : IExceptionActionResultMapper
    {
        private readonly Lazy<IDictionary<Type, Func<HttpRequestMessage, Exception, IHttpActionResult>>> actionResultFactories;

        protected ExceptionActionResultMapperBase()
        {
            this.actionResultFactories = new Lazy<IDictionary<Type, Func<HttpRequestMessage, Exception, IHttpActionResult>>>(() =>
            {
                var factories = new Dictionary<Type, Func<HttpRequestMessage, Exception, IHttpActionResult>>();
                this.ConfigureMappings(new ExceptionActionResultBuilder(factories));
                return factories;
            });
        }

        protected abstract void ConfigureMappings(ExceptionActionResultBuilder builder);

        public IHttpActionResult Get(HttpRequestMessage request, Exception exception)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            var exceptionType = exception.GetType();
            if (exceptionType.IsGenericType)
            {
                exceptionType = exceptionType.GetGenericTypeDefinition();
            }

            if (!this.actionResultFactories.Value.ContainsKey(exceptionType))
            {
                return null;
            }

            var actionResultFactory = this.actionResultFactories.Value[exceptionType];
            return actionResultFactory?.Invoke(request, exception);
        }
    }
}