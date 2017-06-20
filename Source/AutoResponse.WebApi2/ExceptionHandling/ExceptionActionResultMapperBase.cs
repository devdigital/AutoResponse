namespace AutoResponse.WebApi2.ExceptionHandling
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Http;

    public abstract class ExceptionActionResultMapperBase : IExceptionActionResultMapper
    {
        private readonly IDictionary<Type, Func<HttpRequestMessage, Exception, IHttpActionResult>> actionResultFactories;

        protected ExceptionActionResultMapperBase()
        {
            this.actionResultFactories = new Dictionary<Type, Func<HttpRequestMessage, Exception, IHttpActionResult>>();
        }

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

            if (!this.actionResultFactories.ContainsKey(exceptionType))
            {
                return null;
            }

            var actionResultFactory = this.actionResultFactories[exceptionType];
            return actionResultFactory?.Invoke(request, exception);
        }
        
        protected void AddMapping<TException>(            
            Func<HttpRequestMessage, TException, IHttpActionResult> actionResultFactory) where TException : Exception
        {
            if (actionResultFactory == null)
            {
                throw new ArgumentNullException(nameof(actionResultFactory));
            }

            if (this.actionResultFactories.ContainsKey(typeof(TException)))
            {
                throw new InvalidOperationException(
                    $"Exception type {typeof(TException).Name} action result mapping already registered");
            }

            this.actionResultFactories.Add(
                typeof(TException), 
                (r, e) => actionResultFactory(r, e as TException));
        }

        public void AddGenericMapping<TExceptionInterface>(Type exceptionType, Func<HttpRequestMessage, TExceptionInterface, IHttpActionResult> actionResultFactory) where TExceptionInterface : class
        {
            if (actionResultFactory == null)
            {
                throw new ArgumentNullException(nameof(actionResultFactory));
            }

            if (this.actionResultFactories.ContainsKey(exceptionType))
            {
                throw new InvalidOperationException(
                    $"Exception type {exceptionType.Name} action result mapping already registered");
            }

            this.actionResultFactories.Add(
                exceptionType, 
                (r, e) => actionResultFactory(r, e as TExceptionInterface));
        }
    }
}