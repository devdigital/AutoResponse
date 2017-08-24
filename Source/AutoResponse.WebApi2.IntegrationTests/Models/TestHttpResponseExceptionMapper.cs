namespace AutoResponse.WebApi2.IntegrationTests.Models
{
    using System;
    using System.Threading.Tasks;

    using AutoResponse.Client;

    internal class TestHttpResponseExceptionMapper : AutoResponseHttpResponseExceptionMapper
    {
        private readonly Action<HttpResponseExceptionConfiguration> configure;

        public TestHttpResponseExceptionMapper(Action<HttpResponseExceptionConfiguration> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            this.configure = configure;
            this.DefaultException = new Exception("Default exception");
        }

        public Exception DefaultException { get; }

        protected override void ConfigureMappings(HttpResponseExceptionConfiguration configuration)
        {
            this.configure(configuration);
        }

        protected override Task<Exception> GetDefaultException(ResponseContent responseContent, HttpResponseExceptionContext context)
        {
            return Task.FromResult(this.DefaultException);
        }
    }
}