using System.Collections.Generic;
using AutoResponse.AspNetCore.Results;
using AutoResponse.Core.Exceptions;
using AutoResponse.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoResponse.Sample.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            throw new EntityNotFoundException("value", "valueId");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return new ResourceValidationResult(
                new ValidationErrorDetails("This is a validation error."));
        }
    }
}
