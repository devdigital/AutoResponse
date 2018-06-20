// <copyright file="ValuesController.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Sample.WebApi2.Controllers
{
    using System;
    using System.Web.Http;

    using AutoResponse.Core.Models;
    using AutoResponse.Sample.Domain.Repositories;
    using AutoResponse.Sample.WebApi2.Factories;
    using AutoResponse.Sample.WebApi2.Models;
    using AutoResponse.WebApi2.Results;

    /// <summary>
    /// Values controller.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ValuesController : ApiController
    {
        private readonly IValuesRepository valuesRepository;

        private readonly IHttpActionResultFactory actionResultFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValuesController"/> class.
        /// </summary>
        /// <param name="valuesRepository">The values repository.</param>
        /// <param name="actionResultFactory">The action result factory.</param>
        public ValuesController(
            IValuesRepository valuesRepository,
            IHttpActionResultFactory actionResultFactory)
        {
            this.valuesRepository = valuesRepository ?? throw new ArgumentNullException(nameof(valuesRepository));
            this.actionResultFactory = actionResultFactory ?? throw new ArgumentNullException(nameof(actionResultFactory));
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="valueId">The value identifier.</param>
        /// <returns>The value.</returns>
        [HttpGet]
        [Route("api/values/{valueId}")]
        public IHttpActionResult GetValue(int valueId)
        {
            var value = this.valuesRepository.GetValue(valueId);
            return this.Ok(new ValueApiModel { Id = value.Id });
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <returns>The result.</returns>
        [HttpGet]
        [Route("api/result")]
        public IHttpActionResult GetResult()
        {
            return this.actionResultFactory.Create(this.Request);
        }

        /// <summary>
        /// Gets a failed result.
        /// </summary>
        /// <returns>The result.</returns>
        [HttpGet]
        [Route("api/fail")]
        public IHttpActionResult GetFail()
        {
            throw new Exception(
                "There was an error",
                new Exception(
                    "I am an inner exception",
                    new Exception("I am an inner inner exception")));
        }

        /// <summary>
        /// Gets the validation result.
        /// </summary>
        /// <returns>The validation result.</returns>
        [HttpGet]
        [Route("api/validation-result")]
        public IHttpActionResult GetValidationResult()
        {
            return new ResourceValidationResult(
                this.Request, new ValidationErrorDetails("There was a validation error"));
        }
    }
}