namespace AutoResponse.Sample.WebApi2.Controllers
{
    using System;
    using System.Web.Http;

    using AutoResponse.Sample.Domain.Repositories;
    using AutoResponse.Sample.WebApi2.Factories;
    using AutoResponse.Sample.WebApi2.Models;

    public class ValuesController : ApiController
    {
        private readonly IValuesRepository valuesRepository;

        private readonly IHttpActionResultFactory actionResultFactory;

        public ValuesController(
            IValuesRepository valuesRepository,
            IHttpActionResultFactory actionResultFactory)
        {
            if (valuesRepository == null)
            {
                throw new ArgumentNullException(nameof(valuesRepository));
            }

            if (actionResultFactory == null)
            {
                throw new ArgumentNullException(nameof(actionResultFactory));
            }

            this.valuesRepository = valuesRepository;
            this.actionResultFactory = actionResultFactory;
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
            var result = this.actionResultFactory.Create(this.Request);
            return result;
        }
    }
}