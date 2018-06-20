// <copyright file="TestApiEventHttpResponseMapper.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.IntegrationTests.Models
{
    using System;

    using AutoResponse.Core.Mappers;
    using AutoResponse.WebApi2.ExceptionHandling;

    /// <summary>
    /// Test API event HTTP mapper.
    /// </summary>
    /// <seealso cref="AutoResponse.Core.Mappers.AutoResponseApiEventHttpResponseMapper" />
    internal class TestApiEventHttpResponseMapper : AutoResponseApiEventHttpResponseMapper
    {
        private readonly Action<ExceptionHttpResponseConfiguration> configure;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestApiEventHttpResponseMapper"/> class.
        /// </summary>
        /// <param name="configure">The configure.</param>
        public TestApiEventHttpResponseMapper(Action<ExceptionHttpResponseConfiguration> configure)
            : base(new WebApiContextResolver())
        {
            this.configure = configure ?? throw new ArgumentNullException(nameof(configure));
        }

        /// <inheritdoc />
        protected override void ConfigureMappings(ExceptionHttpResponseConfiguration configuration)
        {
            this.configure(configuration);
        }
    }
}