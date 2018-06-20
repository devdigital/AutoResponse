// <copyright file="ValuesController.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Sample.Api.Controllers
{
    using System.Collections.Generic;
    using AutoResponse.AspNetCore.Results;
    using AutoResponse.Core.Exceptions;
    using AutoResponse.Core.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Values controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>The values.</returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            throw new EntityNotFoundException("value", "valueId");
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The value.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return new ResourceValidationResult(
                new ValidationErrorDetails("This is a validation error."));
        }
    }
}
