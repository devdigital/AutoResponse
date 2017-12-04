namespace AutoResponse.Sample.WebApi2.Controllers
{
    using System;
    using System.Web.Http;

    using AutoResponse.Core.Models;
    using AutoResponse.Sample.Domain.Repositories;
    using AutoResponse.Sample.WebApi2.Factories;
    using AutoResponse.Sample.WebApi2.Models;
    using AutoResponse.WebApi2.Results;

    public class ValuesController : ApiController
    {
        private readonly IValuesRepository valuesRepository;

        private readonly IHttpActionResultFactory actionResultFactory;

        public ValuesController(
            IValuesRepository valuesRepository,
            IHttpActionResultFactory actionResultFactory)
        {
            this.valuesRepository = valuesRepository ?? throw new ArgumentNullException(nameof(valuesRepository));
            this.actionResultFactory = actionResultFactory ?? throw new ArgumentNullException(nameof(actionResultFactory));
        }

        [HttpGet]
        [Route("api/values/{valueId}")]
        public IHttpActionResult GetValue(int valueId)
        {
            var value = this.valuesRepository.GetValue(valueId);
            return this.Ok(new ValueApiModel { Id = value.Id });
        }

        [HttpGet]
        [Route("api/result")]
        public IHttpActionResult GetResult()
        {
            return this.actionResultFactory.Create(this.Request);
        }

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

        [HttpGet]
        [Route("api/validation-result")]
        public IHttpActionResult GetValidationResult()
        {
            return new ResourceValidationResult(
                this.Request, new ValidationErrorDetails("There was a validation error"));
        }
    }
}