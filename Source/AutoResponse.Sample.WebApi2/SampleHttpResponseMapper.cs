namespace AutoResponse.Sample.WebApi2
{
    using AutoResponse.Core.Formatters;
    using AutoResponse.Core.Mappers;
    using AutoResponse.Core.Models;
    using AutoResponse.Core.Responses;
    using AutoResponse.Sample.Domain.Exceptions;

    public class SampleHttpResponseMapper : AutoResponseApiEventHttpResponseMapper
    {
        public SampleHttpResponseMapper(IContextResolver contextResolver, IAutoResponseExceptionFormatter formatter) : base(contextResolver, formatter)
        {            
        }

        protected override void ConfigureMappings(ExceptionHttpResponseConfiguration configuration)
        {
            base.ConfigureMappings(configuration);

            configuration.AddMapping<DomainValidationApiEvent>(
                (c, e) => new ResourceValidationHttpResponse(
                    code: null,
                    validationErrorDetails: new ValidationErrorDetails(e.Message)));
        }
    }
}