namespace AutoResponse.Sample.WebApi2.Controllers
{
    using System.Web.Http;

    using AutoResponse.Sample.Domain;
    using AutoResponse.Sample.Domain.Repositories;
    using AutoResponse.Sample.WebApi2.Models;

    public class ValuesController : ApiController
    {
        private readonly IValuesRepository valuesRepository;

        public ValuesController(IValuesRepository valuesRepository)
        {
            this.valuesRepository = valuesRepository;
        }

        [HttpGet]
        [Route("api/values/{valueId}")]
        public IHttpActionResult GetValue(int valueId)
        {
            var value = this.valuesRepository.GetValue(valueId);
            return this.Ok(new ValueApiModel { Id = value.Id });
        }
    }
}