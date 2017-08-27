namespace AutoResponse.Core.Responses
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using AutoResponse.Core.Dtos;
    using AutoResponse.Core.Models;

    public class ResourceNotFoundQueryHttpResponse : JsonHttpResponse<ResourceNotFoundQueryApiModel>
    {
        public ResourceNotFoundQueryHttpResponse(string message, string code, string resource, IEnumerable<QueryParameter> parameters)
            : base(new ResourceNotFoundQueryApiModel { Message = message, Code = code, Resource = resource, QueryParameters = parameters.Select(p => new QueryParameterApiModel { Key = p.Key, Value = p.Value })}, HttpStatusCode.NotFound)
        {
        }        
    }
}