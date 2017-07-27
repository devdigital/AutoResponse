namespace AutoResponse.Sample.WebApi2
{
    using AutoResponse.Core.Mappers;
    using AutoResponse.Core.Models;
    using AutoResponse.Core.Responses;
    using AutoResponse.Owin;
    using AutoResponse.Sample.Domain.Exceptions;

    public class SampleHttpResponseMapper : AutoResponseApiEventHttpResponseMapper
    {
        public SampleHttpResponseMapper() : base(new OwinContextResolver())
        {            
        }

        protected override void ConfigureMappings(ExceptionHttpResponseConfiguration configuration)
        {
            base.ConfigureMappings(configuration);

            configuration.AddMapping<DomainValidationApiEvent>(
                (c, e) => new ResourceValidationHttpResponse(
                    c.Formatter.ApiEventToCode(e),
                    new ValidationErrorDetails(e.Message)));
        }
    }
}