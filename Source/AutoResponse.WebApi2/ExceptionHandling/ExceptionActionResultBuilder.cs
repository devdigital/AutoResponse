namespace AutoResponse.WebApi2.ExceptionHandling
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Http;

    public class ExceptionActionResultBuilder
    {
        private readonly IDictionary<Type, Func<HttpRequestMessage, Exception, IHttpActionResult>> mappings;

        public ExceptionActionResultBuilder(IDictionary<Type, Func<HttpRequestMessage, Exception, IHttpActionResult>> mappings)
        {
            if (mappings == null)
            {
                throw new ArgumentNullException(nameof(mappings));
            }

            this.mappings = mappings;
        }

        public void AddMapping<TException>(
            Func<HttpRequestMessage, TException, IHttpActionResult> actionResultFactory) where TException : Exception
        {
            if (actionResultFactory == null)
            {
                throw new ArgumentNullException(nameof(actionResultFactory));
            }

            if (this.mappings.ContainsKey(typeof(TException)))
            {
                throw new InvalidOperationException(
                          $"Exception type {typeof(TException).Name} action result mapping already registered");
            }

            this.mappings.Add(
                typeof(TException),
                (r, e) => actionResultFactory(r, e as TException));
        }

        public void AddGenericMapping<TExceptionInterface>(Type exceptionType, Func<HttpRequestMessage, TExceptionInterface, IHttpActionResult> actionResultFactory) where TExceptionInterface : class
        {
            if (actionResultFactory == null)
            {
                throw new ArgumentNullException(nameof(actionResultFactory));
            }

            if (this.mappings.ContainsKey(exceptionType))
            {
                throw new InvalidOperationException(
                          $"Exception type {exceptionType.Name} action result mapping already registered");
            }

            this.mappings.Add(
                exceptionType,
                (r, e) => actionResultFactory(r, e as TExceptionInterface));
        }      
    }
}