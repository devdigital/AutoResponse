namespace AutoResponse.WebApi2.Extensions
{
    using System.Web.Http;

    using AutoResponse.WebApi2.Results;

    public static class ApiControllerExtensions
    {
        //public static IHttpActionResult ServiceErrorResult(
        //    this ApiController controller, 
        //    string message)
        //{
        //    return new ServiceErrorResult(controller.Request, message);
        //}

        public static IHttpActionResult ResourceCreatedResult(this ApiController controller, string id)
        {
            return new ResourceCreatedResult(controller.Request, id);
        }   
    }
}