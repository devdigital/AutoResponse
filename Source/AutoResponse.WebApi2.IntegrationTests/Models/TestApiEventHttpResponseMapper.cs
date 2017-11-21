namespace AutoResponse.WebApi2.IntegrationTests.Models
{
    using System;

    using AutoResponse.Core.Mappers;
    using AutoResponse.WebApi2.ExceptionHandling;

    internal class TestApiEventHttpResponseMapper : AutoResponseApiEventHttpResponseMapper
    {
        private readonly Action<ExceptionHttpResponseConfiguration> configure;

        public TestApiEventHttpResponseMapper(Action<ExceptionHttpResponseConfiguration> configure)
            : base(new WebApiContextResolver())
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            this.configure = configure;
        }

        protected override void ConfigureMappings(ExceptionHttpResponseConfiguration configuration)
        {
            this.configure(configuration);
        }
    }
}