using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

using AutoResponse.WebApi2.ExceptionHandling;

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

            var key = exception.GetType();
            if (!this.actionResultFactories.ContainsKey(key))
            {
                return null;
            }

            var actionResultFactory = this.actionResultFactories[key];
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

            this.actionResultFactories.Add(typeof(TException), (r, e) => actionResultFactory(r, e as TException));
        }
    }
}