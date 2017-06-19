namespace AutoResponse.Sample.WebApi2.Controllers
{
    using System.Web.Http;

    using AutoResponse.Data.Exceptions;

    public class ValuesController : ApiController
    {
        [HttpGet]
        [Route("api/value/{valueId}")]
        public IHttpActionResult GetValue(int valueId)
        {
            if (valueId == 1)
            {
                return this.Ok(1);
            }

            throw new EntityNotFoundException<Value>(valueId.ToString());
        }
    }

    public class Value
    {
    }
}