namespace AutoResponse.WebApi2.Extensions
{
    using System.Web.Http;

    using AutoResponse.WebApi2.Results;

    public static class ApiControllerExtensions
    {
        public static IHttpActionResult ServiceErrorResult(
            this ApiController controller, 
            ErrorDetailsApiModel errorDetails)
        {
            return new ServiceErrorResult(controller.Request, errorDetails);
        }

        public static IHttpActionResult ResourceCreatedResult(this ApiController controller, string id)
        {
            return new ResourceCreatedResult(controller.Request, id);
        }   
    }
}