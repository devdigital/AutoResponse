// <copyright file="SampleHttpResponseMapper.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Sample.WebApi2
{
    using AutoResponse.Core.Formatters;
    using AutoResponse.Core.Mappers;
    using AutoResponse.Core.Models;
    using AutoResponse.Core.Responses;
    using AutoResponse.Sample.Domain.Exceptions;

    /// <summary>
    /// Sample HTTP response mapper.
    /// </summary>
    /// <seealso cref="AutoResponse.Core.Mappers.AutoResponseApiEventHttpResponseMapper" />
    public class SampleHttpResponseMapper : AutoResponseApiEventHttpResponseMapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleHttpResponseMapper"/> class.
        /// </summary>
        /// <param name="contextResolver">The context resolver.</param>
        /// <param name="formatter">The formatter.</param>
        public SampleHttpResponseMapper(IContextResolver contextResolver, IAutoResponseExceptionFormatter formatter)
            : base(contextResolver, formatter)
        {
        }

        /// <inheritdoc />
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