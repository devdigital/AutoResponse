namespace AutoResponse.WebApi2.ExceptionHandling
{
    using System;
    using System.Net.Http;

    using AutoResponse.Core.Mappers;

    public class WebApiContextResolver : IContextResolver
    {
        public bool IncludeFullDetails(object context)
        {
            var request = context as HttpRequestMessage;
            if (request == null)
            {
                throw new InvalidOperationException("Unexpected null request");
            }

            return request.ShouldIncludeErrorDetail();
        }
    }
}